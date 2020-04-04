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

namespace GameServerWorkRole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Manages sessions for users connected to the role. Maintains a thread-safe dictionary of active sessions.
    /// </summary>
    internal class GameManager
    {
        /// <summary>Controls access to the session dictionary.</summary>
        private static readonly ReaderWriterLockSlim GameLock = new ReaderWriterLockSlim();

        private static readonly Dictionary<string, GameInformation> Games = new Dictionary<string, GameInformation>();

        /// <summary>
        /// Retrieves a list of aviable game.
        /// </summary>
        /// <returns>A list of GameInformation objects.</returns>
        public static IEnumerable<GameInformation> GetAviableGames()
        {
            GameLock.EnterReadLock();
            try
            {
                return Games.Values.Where(game => game.Players.Count < game.NumPlayers).ToArray();
            }
            finally
            {
                GameLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Возвращает текущую игру для игрока
        /// </summary>
        /// <param name="sessionId">идентификатор игры</param>
        /// <returns>описание игры</returns>
        public static GameInformation GetCurrentGamesForPlayer(string sessionId)
        {
            GameLock.EnterReadLock();
            try
            {
                return Games.Values.FirstOrDefault(game => game.Players.Select(x=>x.SessionId).Contains(sessionId));
            }
            finally
            {
                GameLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Возвращет игру по индетификатору
        /// </summary>
        /// <param name="sessionId">идентификатор игры</param>
        /// <returns>описание игры</returns>
        public static GameInformation GetGame(string sessionId)
        {
            if (sessionId == null) return null;

            GameLock.EnterReadLock();
            try
            {
                if (Games != null && Games.ContainsKey(sessionId)) return Games[sessionId];
            }
            finally
            {
                GameLock.ExitReadLock();
            }

            return null;
        }

        /// <summary>
        /// Создание новой игры
        /// </summary>
        /// <param name="sessionId">Идентификатор игры (равен идентификатору пользователя)</param>
        /// <param name="game">описание игры</param>
        public static void CreateGame(string sessionId, GameInformation game)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentException("sessionId is null or empety");
            }

            if (game == null)
            {
                throw new ArgumentException("Not game");
            }            

            GameLock.EnterWriteLock();
            try
            {
                GameInformation temp;
                bool isNewGame = !Games.TryGetValue(sessionId, out temp);
                if (!isNewGame)
                {
                    Games.Remove(sessionId);
                }
                Games.Add(sessionId, game);

            }
            finally
            {
                GameLock.ExitWriteLock();
            }
        }

        public static void RemoveGame(string sessionId)
        {
            if (sessionId == null)
            {
                throw new ArgumentNullException("sessionId");
            }

            GameLock.EnterWriteLock();
            try
            {
                GameInformation game;
                if (Games.TryGetValue(sessionId, out game))
                {
                    game.Players.Clear();
                    Games.Remove(sessionId);
                }
            }
            finally
            {
                GameLock.ExitWriteLock();
            }
        }

    }
}
