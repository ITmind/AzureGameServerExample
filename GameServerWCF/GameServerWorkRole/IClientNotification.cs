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
    using System.ServiceModel;

    [ServiceContract]
    public interface IClientNotification
    {
        /// <summary>
        /// Получение информации об изменении данных игры
        /// </summary>
        /// <param name="gameInfo">данные об игре</param>
        [OperationContract(IsOneWay = true)]
        void UpdateGameList(GameInformation gameInfo);

        /// <summary>
        /// Получение сообщения
        /// </summary>
        /// <param name="type">тип сообщения</param>
        /// <param name="message">сообщение</param>
        [OperationContract(IsOneWay = true)]
        void DeliverGameMessage(string type,string message);
    }
}
