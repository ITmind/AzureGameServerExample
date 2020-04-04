// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System.Threading;

namespace GameServerWorkRole
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceModel;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Contract;
    using System.Security;

    /// <summary>
    /// Implementation of the WCF chat service.
    /// </summary>
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple,
#if DEBUG
        IncludeExceptionDetailInFaults = true,
#else
        IncludeExceptionDetailInFaults = false,
#endif
        AddressFilterMode = AddressFilterMode.Any)]
    public class GameService : IGameService
    {
        Timer timer;
        TimeSpan TimeForDelete;

        public GameService()
        {
            TimeForDelete = new TimeSpan(0, 0, 60);
            timer = new Timer(timerCallback,null,60000,60000);
        }

        private void timerCallback(object state)
        {
            var sesions = SessionManager.GetNotAcitiveSessions();
            var sesionForDelete = sesions.Where(x => DateTime.Now.Subtract(x.LastSyncTime) > TimeForDelete).Select(x=>x.SessionId);
            foreach (var sessionId in sesionForDelete)
            {
                SessionManager.RemoveSession(sessionId);
                DeleteGame(sessionId);
            }
        }

        [SecurityCritical]
        public ClientInformation Register(string uid, string userName)
        {
            // retrieve session information
            string roleId = RoleEnvironment.CurrentRoleInstance.Id;
            string sessionId = uid;
            var callback = OperationContext.Current.GetCallbackChannel<IClientNotification>();
            var game = GameManager.GetCurrentGamesForPlayer(sessionId);
            if (game != null && !game.IsActive)
            {
                game.IsActive = true;
                NotifyConnectedClientsGame(game);
            }

            SessionInformation session;
            if (SessionManager.CreateOrUpdateSession(sessionId, userName, roleId, callback, out session))
            {
                // ensure that the session is killed when channel is closed
                OperationContext.Current.Channel.Closed += (sender, e) =>
                                                               {
                                                                   session.IsActive = false;
                                                                   game = GameManager.GetCurrentGamesForPlayer(sessionId);
                                                                   if (game != null && game.IsActive)
                                                                   {
                                                                       game.IsActive = false;
                                                                       NotifyConnectedClientsGame(game);
                                                                   }

                    Trace.TraceInformation("Session '{0}' by user '{1}' has been closed in role '{2}'.", sessionId, userName, roleId);
                };

                Trace.TraceInformation("Session '{0}' by user '{1}' has been opened in role '{2}'.", sessionId, userName, roleId);
            }

            return new ClientInformation
                       {
                SessionId = sessionId,
                UserName = userName,
                RoleId = roleId
            };
        }


        /// <summary>
        /// Returns a list of connected clients.
        /// </summary>
        /// <returns>The list of active sessions.</returns>
        public IEnumerable<ClientInformation> GetConnectedClients()
        {
            return from session in SessionManager.GetActiveSessions()
                   select new ClientInformation
                              {
                       SessionId = session.SessionId,
                       UserName = session.UserName,
                       RoleId = session.RoleId
                   };
         }


        private static void NotifyConnectedClientsGame(GameInformation gameInfo)
        {
            foreach (SessionInformation client in SessionManager.GetActiveSessions())
            {
                if (client.Callback != null)
                {
                    try
                    {
                        client.Callback.UpdateGameList(gameInfo);
                    }
                    catch (TimeoutException timeoutException)
                    {
                        Trace.TraceError("Unable to notify client '{0}'. The service operation timed out. {1}", client.UserName, timeoutException.Message);
                        client.Callback = null;
                    }
                    catch (CommunicationException communicationException)
                    {
                        Trace.TraceError("Unable to notify client '{0}'. There was a communication problem. {1} - {2}", client.UserName, communicationException.Message, communicationException.StackTrace);
                        client.Callback = null;
                    }
                }
            }
        }


        public GameInformation CreateGame(string uid, string parameters)
        {
            var newGame = new GameInformation();          
            newGame.Parameters = parameters;
            newGame.NumPlayers = 2;
            newGame.SessionId = uid;
            newGame.Players = new List<ClientInformation>(2);
            newGame.Players.Add(SessionManager.GetSession(uid));
            GameManager.CreateGame(uid, newGame);
            NotifyConnectedClientsGame(newGame);
            return newGame;
        }

        public void JoinGame(string uid, string gameSessionId)
        {
            var game = GameManager.GetGame(gameSessionId);
            if (game != null)
            {
                game.Players.Add(SessionManager.GetSession(uid));
                NotifyConnectedClientsGame(game);
            }
        }


        public void MakeTurn(string uid, string type, string data)
        {
            var game = GameManager.GetCurrentGamesForPlayer(uid);
            if (game != null)
            {
                foreach (var player in game.Players)
                {
                    if (player.SessionId != uid)
                    {
                        var playerSession = SessionManager.GetSession(player.SessionId);
                        if (playerSession.Callback != null)
                        {
                            try
                            {
                                playerSession.Callback.DeliverGameMessage(type, data);
                            }
                            catch
                            {
                                
                            }
                        }
                    }
                }
            }
        }


        public IEnumerable<GameInformation> GetGames()
        {
            return GameManager.GetAviableGames();
        }



        public void DeleteGame(string uid)
        {
            var deletingGame = GameManager.GetGame(uid);
            if (deletingGame != null)
            {
                GameManager.RemoveGame(uid);
                NotifyConnectedClientsGame(deletingGame);
            }
            else
            {
                deletingGame = GameManager.GetCurrentGamesForPlayer(uid);
                if (deletingGame != null)
                {
                    try
                    {
                        deletingGame.Players.RemoveAt(1);
                        NotifyConnectedClientsGame(deletingGame);
                    }
                    catch
                    {                                                
                    }
                    
                    
                }
            }
        }
    }
}
