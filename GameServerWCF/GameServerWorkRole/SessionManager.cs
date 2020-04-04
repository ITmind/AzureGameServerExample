namespace GameServerWorkRole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using GameServerWorkRole.Contract;

    /// <summary>
    /// Управление сессиями пользователей в текущей роли сервера. Поддерживает потоко-безпасный словарь активных сессий.
    /// </summary>
    internal class SessionManager
    {
        /// <summary>Контроль доступа к словарю сессий</summary>
        private static ReaderWriterLockSlim sessionLock = new ReaderWriterLockSlim();

        /// <summary>Содежрит информацию обо всех сессиях пользователей</summary>
        private static Dictionary<string, SessionInformation> sessions = new Dictionary<string, SessionInformation>();

        /// <summary>
        /// Получить сессию игрока
        /// </summary>
        /// <param name="sessionId">идентификатор игрока</param>
        /// <returns></returns>
        public static SessionInformation GetSession(string sessionId)
        {
            if (sessionId == null)
            {
                throw new ArgumentNullException("sessionId");
            }

            sessionLock.EnterReadLock();
            try
            {
                SessionInformation session;
                sessions.TryGetValue(sessionId, out session);
                return session;
            }
            finally
            {
                sessionLock.ExitReadLock();
            }
        }

        /// <summary>
        /// создать/обновить сессию игрока
        /// </summary>
        /// <param name="sessionId">идентификатор игрока</param>
        /// <param name="userName">имя игрока</param>
        /// <param name="roleId">идентификатор роли</param>
        /// <param name="callback">интерфейс обратного вызова</param>
        /// <param name="session">данные сессии</param>
        /// <returns></returns>
        public static bool CreateOrUpdateSession(string sessionId, string userName, string roleId, IClientNotification callback, out SessionInformation session)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentException("sessionId is null or empty");
            }

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("userName is null or empty");
            }

            if (string.IsNullOrEmpty(roleId))
            {
                throw new ArgumentException("roleId is null or empty");
            }

            sessionLock.EnterWriteLock();
            try
            {
                bool isNewSession = !sessions.TryGetValue(sessionId, out session);
                if (isNewSession)
                {
                    session = new SessionInformation();
                    sessions.Add(sessionId, session);
                }

                session.SessionId = sessionId;
                session.UserName = userName;
                session.RoleId = roleId;
                session.Callback = callback;
                session.IsActive = true;
                session.LastSyncTime = DateTime.Now;

                return isNewSession;
            }
            finally
            {
                sessionLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Удаление сессии из словаря
        /// </summary>
        /// <param name="sessionId">идентификатор игрока</param>
        public static void RemoveSession(string sessionId)
        {
            if (sessionId == null)
            {
                throw new ArgumentNullException("sessionId");
            }

            sessionLock.EnterWriteLock();
            try
            {
                SessionInformation session;
                if (sessions.TryGetValue(sessionId, out session))
                {
                    session.IsActive = false;
                    sessions.Remove(sessionId);
                }
            }
            finally
            {
                sessionLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Получение активных сессий
        /// </summary>
        /// <returns>Список сессий</returns>
        public static IEnumerable<SessionInformation> GetActiveSessions()
        {
            sessionLock.EnterReadLock();
            try
            {
                return sessions.Values.Where(session => session.IsActive).ToArray();
            }
            finally
            {
                sessionLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Получние неактивных сессий
        /// </summary>
        /// <returns>Список сессий</returns>
        public static IEnumerable<SessionInformation> GetNotAcitiveSessions()
        {
            sessionLock.EnterReadLock();
            try
            {
                return sessions.Values.Where(session => !session.IsActive).ToArray();
            }
            finally
            {
                sessionLock.ExitReadLock();
            }
        }

    }
}
