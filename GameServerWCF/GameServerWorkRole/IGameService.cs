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

namespace GameServerWorkRole.Contract
{
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(IClientNotification),SessionMode = SessionMode.Required)]
    public interface IGameService
    {
        /// <summary>
        /// Регистрация пользователя на сервере
        /// </summary>
        /// <param name="uid">идентификатор пользователя</param>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        [OperationContract(IsInitiating = true)]
        ClientInformation Register(string uid, string userName);

        /// <summary>
        /// Создание новой игры
        /// </summary>
        /// <param name="uid">идентификатор пользователя, создающего игру</param>
        /// <param name="parameters">парметры в виде стоки</param>
        /// <returns>Данные созданной игры</returns>
        [OperationContract(IsInitiating = false)]
        GameInformation CreateGame(string uid, string parameters);

        /// <summary>
        /// Присоединение к существующей игре
        /// </summary>
        /// <param name="uid">идентификатор пользователя, подключающегося к игре</param>
        /// <param name="gameSessionId">идентификатор игры</param>
        [OperationContract(IsInitiating = false)]
        void JoinGame(string uid, string gameSessionId);

        /// <summary>
        /// Удаление игры
        /// </summary>
        /// <param name="uid">идентификатор игры</param>
        [OperationContract(IsInitiating = false)]
        void DeleteGame(string uid);

        /// <summary>
        /// Разослать сообщения клиентам, подключеным к текущей игре
        /// </summary>
        /// <param name="uid">идентификатор пользователя</param>
        /// <param name="type">тип сообщения</param>
        /// <param name="data">сообщение</param>
        [OperationContract(IsInitiating = false)]
        void MakeTurn(string uid, string type, string data);

        /// <summary>
        /// Получение доступных игр
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsInitiating = false)]
        IEnumerable<GameInformation> GetGames();
    }
}
