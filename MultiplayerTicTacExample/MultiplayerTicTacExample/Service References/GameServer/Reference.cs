﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18010
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 11.0.50727.1
// 
namespace MultiplayerTicTacExample.GameServer {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ClientInformation", Namespace="http://schemas.datacontract.org/2004/07/GameServerWorkRole.Contract")]
    public partial class ClientInformation : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool IsActiveField;
        
        private string RoleIdField;
        
        private string SessionIdField;
        
        private string UserNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsActive {
            get {
                return this.IsActiveField;
            }
            set {
                if ((this.IsActiveField.Equals(value) != true)) {
                    this.IsActiveField = value;
                    this.RaisePropertyChanged("IsActive");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RoleId {
            get {
                return this.RoleIdField;
            }
            set {
                if ((object.ReferenceEquals(this.RoleIdField, value) != true)) {
                    this.RoleIdField = value;
                    this.RaisePropertyChanged("RoleId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SessionId {
            get {
                return this.SessionIdField;
            }
            set {
                if ((object.ReferenceEquals(this.SessionIdField, value) != true)) {
                    this.SessionIdField = value;
                    this.RaisePropertyChanged("SessionId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameInformation", Namespace="http://schemas.datacontract.org/2004/07/GameServerWorkRole")]
    public partial class GameInformation : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool IsActiveField;
        
        private int NumPlayersField;
        
        private string ParametersField;
        
        private System.Collections.Generic.List<MultiplayerTicTacExample.GameServer.ClientInformation> PlayersField;
        
        private string SessionIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsActive {
            get {
                return this.IsActiveField;
            }
            set {
                if ((this.IsActiveField.Equals(value) != true)) {
                    this.IsActiveField = value;
                    this.RaisePropertyChanged("IsActive");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumPlayers {
            get {
                return this.NumPlayersField;
            }
            set {
                if ((this.NumPlayersField.Equals(value) != true)) {
                    this.NumPlayersField = value;
                    this.RaisePropertyChanged("NumPlayers");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Parameters {
            get {
                return this.ParametersField;
            }
            set {
                if ((object.ReferenceEquals(this.ParametersField, value) != true)) {
                    this.ParametersField = value;
                    this.RaisePropertyChanged("Parameters");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<MultiplayerTicTacExample.GameServer.ClientInformation> Players {
            get {
                return this.PlayersField;
            }
            set {
                if ((object.ReferenceEquals(this.PlayersField, value) != true)) {
                    this.PlayersField = value;
                    this.RaisePropertyChanged("Players");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SessionId {
            get {
                return this.SessionIdField;
            }
            set {
                if ((object.ReferenceEquals(this.SessionIdField, value) != true)) {
                    this.SessionIdField = value;
                    this.RaisePropertyChanged("SessionId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameServer.IGameService", CallbackContract=typeof(MultiplayerTicTacExample.GameServer.IGameServiceCallback))]
    public interface IGameService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/Register", ReplyAction="http://tempuri.org/IGameService/RegisterResponse")]
        System.Threading.Tasks.Task<MultiplayerTicTacExample.GameServer.ClientInformation> RegisterAsync(string uid, string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/CreateGame", ReplyAction="http://tempuri.org/IGameService/CreateGameResponse")]
        System.Threading.Tasks.Task<MultiplayerTicTacExample.GameServer.GameInformation> CreateGameAsync(string uid, string parameters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/JoinGame", ReplyAction="http://tempuri.org/IGameService/JoinGameResponse")]
        System.Threading.Tasks.Task JoinGameAsync(string uid, string gameSessionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/DeleteGame", ReplyAction="http://tempuri.org/IGameService/DeleteGameResponse")]
        System.Threading.Tasks.Task DeleteGameAsync(string uid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/MakeTurn", ReplyAction="http://tempuri.org/IGameService/MakeTurnResponse")]
        System.Threading.Tasks.Task MakeTurnAsync(string uid, string type, string data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetGames", ReplyAction="http://tempuri.org/IGameService/GetGamesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<MultiplayerTicTacExample.GameServer.GameInformation>> GetGamesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/UpdateGameList")]
        void UpdateGameList(MultiplayerTicTacExample.GameServer.GameInformation gameInfo);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/DeliverGameMessage")]
        void DeliverGameMessage(string type, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceChannel : MultiplayerTicTacExample.GameServer.IGameService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameServiceClientBase : System.ServiceModel.DuplexClientBase<MultiplayerTicTacExample.GameServer.IGameService>, MultiplayerTicTacExample.GameServer.IGameService {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public GameServiceClientBase(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance, GameServiceClientBase.GetDefaultBinding(), GameServiceClientBase.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.NetTcpBinding_IGameService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GameServiceClientBase(System.ServiceModel.InstanceContext callbackInstance, EndpointConfiguration endpointConfiguration) : 
                base(callbackInstance, GameServiceClientBase.GetBindingForEndpoint(endpointConfiguration), GameServiceClientBase.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GameServiceClientBase(System.ServiceModel.InstanceContext callbackInstance, EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(callbackInstance, GameServiceClientBase.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GameServiceClientBase(System.ServiceModel.InstanceContext callbackInstance, EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, GameServiceClientBase.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public GameServiceClientBase(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<MultiplayerTicTacExample.GameServer.ClientInformation> RegisterAsync(string uid, string userName) {
            return base.Channel.RegisterAsync(uid, userName);
        }
        
        public System.Threading.Tasks.Task<MultiplayerTicTacExample.GameServer.GameInformation> CreateGameAsync(string uid, string parameters) {
            return base.Channel.CreateGameAsync(uid, parameters);
        }
        
        public System.Threading.Tasks.Task JoinGameAsync(string uid, string gameSessionId) {
            return base.Channel.JoinGameAsync(uid, gameSessionId);
        }
        
        public System.Threading.Tasks.Task DeleteGameAsync(string uid) {
            return base.Channel.DeleteGameAsync(uid);
        }
        
        public System.Threading.Tasks.Task MakeTurnAsync(string uid, string type, string data) {
            return base.Channel.MakeTurnAsync(uid, type, data);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<MultiplayerTicTacExample.GameServer.GameInformation>> GetGamesAsync() {
            return base.Channel.GetGamesAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_IGameService)) {
                System.ServiceModel.NetTcpBinding result = new System.ServiceModel.NetTcpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.Security.Mode = System.ServiceModel.SecurityMode.None;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.NetTcpBinding_IGameService)) {
                //return new System.ServiceModel.EndpointAddress("net.tcp://127.255.0.1:3030/GameServer");
                return new System.ServiceModel.EndpointAddress("net.tcp://mytestgameserver.cloudapp.net:3030/GameServer");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return GameServiceClientBase.GetBindingForEndpoint(EndpointConfiguration.NetTcpBinding_IGameService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return GameServiceClientBase.GetEndpointAddress(EndpointConfiguration.NetTcpBinding_IGameService);
        }
        
        public enum EndpointConfiguration {
            
            NetTcpBinding_IGameService,
        }
    }
    
    public class UpdateGameListReceivedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public UpdateGameListReceivedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public MultiplayerTicTacExample.GameServer.GameInformation gameInfo {
            get {
                base.RaiseExceptionIfNecessary();
                return ((MultiplayerTicTacExample.GameServer.GameInformation)(this.results[0]));
            }
        }
    }
    
    public class DeliverGameMessageReceivedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public DeliverGameMessageReceivedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string type {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
        
        public string message {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
    }
    
    public partial class GameServiceClient : GameServiceClientBase {
        
        public GameServiceClient(EndpointConfiguration endpointConfiguration) : 
                this(new GameServiceClientCallback(), endpointConfiguration) {
        }
        
        private GameServiceClient(GameServiceClientCallback callbackImpl, EndpointConfiguration endpointConfiguration) : 
                base(new System.ServiceModel.InstanceContext(callbackImpl), endpointConfiguration) {
            callbackImpl.Initialize(this);
        }
        
        public GameServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                this(new GameServiceClientCallback(), binding, remoteAddress) {
        }
        
        private GameServiceClient(GameServiceClientCallback callbackImpl, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(new System.ServiceModel.InstanceContext(callbackImpl), binding, remoteAddress) {
            callbackImpl.Initialize(this);
        }
        
        public GameServiceClient() : 
                this(new GameServiceClientCallback()) {
        }
        
        private GameServiceClient(GameServiceClientCallback callbackImpl) : 
                base(new System.ServiceModel.InstanceContext(callbackImpl)) {
            callbackImpl.Initialize(this);
        }
        
        public event System.EventHandler<UpdateGameListReceivedEventArgs> UpdateGameListReceived;
        
        public event System.EventHandler<DeliverGameMessageReceivedEventArgs> DeliverGameMessageReceived;
        
        private void OnUpdateGameListReceived(object state) {
            if ((this.UpdateGameListReceived != null)) {
                object[] results = ((object[])(state));
                this.UpdateGameListReceived(this, new UpdateGameListReceivedEventArgs(results, null, false, null));
            }
        }
        
        private void OnDeliverGameMessageReceived(object state) {
            if ((this.DeliverGameMessageReceived != null)) {
                object[] results = ((object[])(state));
                this.DeliverGameMessageReceived(this, new DeliverGameMessageReceivedEventArgs(results, null, false, null));
            }
        }
        
        private class GameServiceClientCallback : object, IGameServiceCallback {
            
            private GameServiceClient proxy;
            
            public void Initialize(GameServiceClient proxy) {
                this.proxy = proxy;
            }
            
            public void UpdateGameList(MultiplayerTicTacExample.GameServer.GameInformation gameInfo) {
                this.proxy.OnUpdateGameListReceived(new object[] {
                            gameInfo});
            }
            
            public void DeliverGameMessage(string type, string message) {
                this.proxy.OnDeliverGameMessageReceived(new object[] {
                            type,
                            message});
            }
        }
    }
}
