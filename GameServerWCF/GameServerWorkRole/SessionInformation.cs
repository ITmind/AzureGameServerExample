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
    using System.Runtime.Serialization;
    using System;

    /// <summary>
    /// Contains information related to the session.
    /// </summary>
    [DataContract(Name = "ClientInformation")]
    internal class SessionInformation : ClientInformation
    {
        /// <summary>Gets or sets the callback channel for client notifications.</summary>
        [IgnoreDataMember]
        public IClientNotification Callback { get; set; }

        [IgnoreDataMember]
        public DateTime LastSyncTime { get; set; }

    }
}
