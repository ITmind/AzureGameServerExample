namespace GameServerWorkRole
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using GameServerWorkRole.Contract;

    [DataContract]
    public class GameInformation
    {
        /// <summary>
        /// Список игроков данной игры
        /// </summary>
        [DataMember]
        public List<ClientInformation> Players { get; set; }

        /// <summary>
        /// Количество игроков
        /// </summary>
        [DataMember]
        public int NumPlayers { get; set; }

        /// <summary>
        /// Идентификатор игры
        /// </summary>
        [DataMember]
        public string SessionId { get; set; }

        /// <summary>
        /// Параметры игры
        /// </summary>
        [DataMember]
        public string Parameters { get; set; }

        /// <summary>
        /// true при активной сессии, false при неактивной
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }
    }
}
