
namespace GameServerWorkRole.Contract
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Contains information about a client session.
    /// </summary>   
    [DataContract]
    public class ClientInformation
    {
        /// <summary>Идентификатор пользователя</summary>
        [DataMember]
        public string SessionId { get; set; }

        /// <summary>Имя пользователя</summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>Идентификатор роли сервера</summary>
        [DataMember]
        public string RoleId { get; set; }

        /// <summary>true при активной сессии, false при неактивной</summary>
        [DataMember]
        public bool IsActive { get; set; }
    }
}
