﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace View.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlayerDTO", Namespace="http://schemas.datacontract.org/2004/07/Logic")]
    [System.SerializableAttribute()]
    public partial class PlayerDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime BirthdayField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CoinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsActiveField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Birthday {
            get {
                return this.BirthdayField;
            }
            set {
                if ((this.BirthdayField.Equals(value) != true)) {
                    this.BirthdayField = value;
                    this.RaisePropertyChanged("Birthday");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Coin {
            get {
                return this.CoinField;
            }
            set {
                if ((this.CoinField.Equals(value) != true)) {
                    this.CoinField = value;
                    this.RaisePropertyChanged("Coin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
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
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IAuthenticationService")]
    public interface IAuthenticationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/AuthenticationLogin", ReplyAction="http://tempuri.org/IAuthenticationService/AuthenticationLoginResponse")]
        View.ServiceReference.PlayerDTO AuthenticationLogin(string name, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/AuthenticationLogin", ReplyAction="http://tempuri.org/IAuthenticationService/AuthenticationLoginResponse")]
        System.Threading.Tasks.Task<View.ServiceReference.PlayerDTO> AuthenticationLoginAsync(string name, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthenticationServiceChannel : View.ServiceReference.IAuthenticationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthenticationServiceClient : System.ServiceModel.ClientBase<View.ServiceReference.IAuthenticationService>, View.ServiceReference.IAuthenticationService {
        
        public AuthenticationServiceClient() {
        }
        
        public AuthenticationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthenticationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public View.ServiceReference.PlayerDTO AuthenticationLogin(string name, string password) {
            return base.Channel.AuthenticationLogin(name, password);
        }
        
        public System.Threading.Tasks.Task<View.ServiceReference.PlayerDTO> AuthenticationLoginAsync(string name, string password) {
            return base.Channel.AuthenticationLoginAsync(name, password);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IChatService", CallbackContract=typeof(View.ServiceReference.IChatServiceCallback))]
    public interface IChatService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/JoinChat")]
        void JoinChat(string username, string codeVerification);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/JoinChat")]
        System.Threading.Tasks.Task JoinChatAsync(string username, string codeVerification);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendMessage")]
        void SendMessage(string message, string userChat, string codeVerification);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(string message, string userChat, string codeVerification);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ExitChat")]
        void ExitChat(string userName, string codeVerification);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ExitChat")]
        System.Threading.Tasks.Task ExitChatAsync(string userName, string codeVerification);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/CreateChat")]
        void CreateChat(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/CreateChat")]
        System.Threading.Tasks.Task CreateChatAsync(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/DeleteChat")]
        void DeleteChat(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/DeleteChat")]
        System.Threading.Tasks.Task DeleteChatAsync(string verificationCode);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChatService/ReciveMessage")]
        void ReciveMessage(string player, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatServiceChannel : View.ServiceReference.IChatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChatServiceClient : System.ServiceModel.DuplexClientBase<View.ServiceReference.IChatService>, View.ServiceReference.IChatService {
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void JoinChat(string username, string codeVerification) {
            base.Channel.JoinChat(username, codeVerification);
        }
        
        public System.Threading.Tasks.Task JoinChatAsync(string username, string codeVerification) {
            return base.Channel.JoinChatAsync(username, codeVerification);
        }
        
        public void SendMessage(string message, string userChat, string codeVerification) {
            base.Channel.SendMessage(message, userChat, codeVerification);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(string message, string userChat, string codeVerification) {
            return base.Channel.SendMessageAsync(message, userChat, codeVerification);
        }
        
        public void ExitChat(string userName, string codeVerification) {
            base.Channel.ExitChat(userName, codeVerification);
        }
        
        public System.Threading.Tasks.Task ExitChatAsync(string userName, string codeVerification) {
            return base.Channel.ExitChatAsync(userName, codeVerification);
        }
        
        public void CreateChat(string verificationCode) {
            base.Channel.CreateChat(verificationCode);
        }
        
        public System.Threading.Tasks.Task CreateChatAsync(string verificationCode) {
            return base.Channel.CreateChatAsync(verificationCode);
        }
        
        public void DeleteChat(string verificationCode) {
            base.Channel.DeleteChat(verificationCode);
        }
        
        public System.Threading.Tasks.Task DeleteChatAsync(string verificationCode) {
            return base.Channel.DeleteChatAsync(verificationCode);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IChangePasswordService")]
    public interface IChangePasswordService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChangePasswordService/ChangePassword", ReplyAction="http://tempuri.org/IChangePasswordService/ChangePasswordResponse")]
        bool ChangePassword(string email, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChangePasswordService/ChangePassword", ReplyAction="http://tempuri.org/IChangePasswordService/ChangePasswordResponse")]
        System.Threading.Tasks.Task<bool> ChangePasswordAsync(string email, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChangePasswordServiceChannel : View.ServiceReference.IChangePasswordService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChangePasswordServiceClient : System.ServiceModel.ClientBase<View.ServiceReference.IChangePasswordService>, View.ServiceReference.IChangePasswordService {
        
        public ChangePasswordServiceClient() {
        }
        
        public ChangePasswordServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ChangePasswordServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ChangePasswordServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ChangePasswordServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool ChangePassword(string email, string password) {
            return base.Channel.ChangePassword(email, password);
        }
        
        public System.Threading.Tasks.Task<bool> ChangePasswordAsync(string email, string password) {
            return base.Channel.ChangePasswordAsync(email, password);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IEmailService")]
    public interface IEmailService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmailService/ValidationEmail", ReplyAction="http://tempuri.org/IEmailService/ValidationEmailResponse")]
        bool ValidationEmail(string email, string codeVerification);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmailService/ValidationEmail", ReplyAction="http://tempuri.org/IEmailService/ValidationEmailResponse")]
        System.Threading.Tasks.Task<bool> ValidationEmailAsync(string email, string codeVerification);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEmailServiceChannel : View.ServiceReference.IEmailService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EmailServiceClient : System.ServiceModel.ClientBase<View.ServiceReference.IEmailService>, View.ServiceReference.IEmailService {
        
        public EmailServiceClient() {
        }
        
        public EmailServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EmailServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmailServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool ValidationEmail(string email, string codeVerification) {
            return base.Channel.ValidationEmail(email, codeVerification);
        }
        
        public System.Threading.Tasks.Task<bool> ValidationEmailAsync(string email, string codeVerification) {
            return base.Channel.ValidationEmailAsync(email, codeVerification);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IUserRegistrationService")]
    public interface IUserRegistrationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRegistrationService/RegistrerUserDataBase", ReplyAction="http://tempuri.org/IUserRegistrationService/RegistrerUserDataBaseResponse")]
        bool RegistrerUserDataBase(View.ServiceReference.PlayerDTO player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRegistrationService/RegistrerUserDataBase", ReplyAction="http://tempuri.org/IUserRegistrationService/RegistrerUserDataBaseResponse")]
        System.Threading.Tasks.Task<bool> RegistrerUserDataBaseAsync(View.ServiceReference.PlayerDTO player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRegistrationService/ValidationEmailDataBase", ReplyAction="http://tempuri.org/IUserRegistrationService/ValidationEmailDataBaseResponse")]
        bool ValidationEmailDataBase(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRegistrationService/ValidationEmailDataBase", ReplyAction="http://tempuri.org/IUserRegistrationService/ValidationEmailDataBaseResponse")]
        System.Threading.Tasks.Task<bool> ValidationEmailDataBaseAsync(string email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRegistrationService/ValidationUsernameDataBase", ReplyAction="http://tempuri.org/IUserRegistrationService/ValidationUsernameDataBaseResponse")]
        bool ValidationUsernameDataBase(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserRegistrationService/ValidationUsernameDataBase", ReplyAction="http://tempuri.org/IUserRegistrationService/ValidationUsernameDataBaseResponse")]
        System.Threading.Tasks.Task<bool> ValidationUsernameDataBaseAsync(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserRegistrationServiceChannel : View.ServiceReference.IUserRegistrationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserRegistrationServiceClient : System.ServiceModel.ClientBase<View.ServiceReference.IUserRegistrationService>, View.ServiceReference.IUserRegistrationService {
        
        public UserRegistrationServiceClient() {
        }
        
        public UserRegistrationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserRegistrationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserRegistrationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserRegistrationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool RegistrerUserDataBase(View.ServiceReference.PlayerDTO player) {
            return base.Channel.RegistrerUserDataBase(player);
        }
        
        public System.Threading.Tasks.Task<bool> RegistrerUserDataBaseAsync(View.ServiceReference.PlayerDTO player) {
            return base.Channel.RegistrerUserDataBaseAsync(player);
        }
        
        public bool ValidationEmailDataBase(string email) {
            return base.Channel.ValidationEmailDataBase(email);
        }
        
        public System.Threading.Tasks.Task<bool> ValidationEmailDataBaseAsync(string email) {
            return base.Channel.ValidationEmailDataBaseAsync(email);
        }
        
        public bool ValidationUsernameDataBase(string username) {
            return base.Channel.ValidationUsernameDataBase(username);
        }
        
        public System.Threading.Tasks.Task<bool> ValidationUsernameDataBaseAsync(string username) {
            return base.Channel.ValidationUsernameDataBaseAsync(username);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IJoinGameService", CallbackContract=typeof(View.ServiceReference.IJoinGameServiceCallback))]
    public interface IJoinGameService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/JoinGame")]
        void JoinGame(string username, string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/JoinGame")]
        System.Threading.Tasks.Task JoinGameAsync(string username, string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/SendWinner")]
        void SendWinner(string username, string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/SendWinner")]
        System.Threading.Tasks.Task SendWinnerAsync(string username, string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/ExitGame")]
        void ExitGame(string userName, string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/ExitGame")]
        System.Threading.Tasks.Task ExitGameAsync(string userName, string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/CreateGame")]
        void CreateGame(string verificationCode, int limitPlayers);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/CreateGame")]
        System.Threading.Tasks.Task CreateGameAsync(string verificationCode, int limitPlayers);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/EliminateGame")]
        void EliminateGame(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/EliminateGame")]
        System.Threading.Tasks.Task EliminateGameAsync(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/SendNextHostGame")]
        void SendNextHostGame(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/SendNextHostGame")]
        System.Threading.Tasks.Task SendNextHostGameAsync(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/GoToGame")]
        void GoToGame(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/GoToGame")]
        System.Threading.Tasks.Task GoToGameAsync(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJoinGameService/ResponseCodeExist", ReplyAction="http://tempuri.org/IJoinGameService/ResponseCodeExistResponse")]
        bool ResponseCodeExist(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJoinGameService/ResponseCodeExist", ReplyAction="http://tempuri.org/IJoinGameService/ResponseCodeExistResponse")]
        System.Threading.Tasks.Task<bool> ResponseCodeExistAsync(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJoinGameService/ResponseCompleteLobby", ReplyAction="http://tempuri.org/IJoinGameService/ResponseCompleteLobbyResponse")]
        bool ResponseCompleteLobby(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJoinGameService/ResponseCompleteLobby", ReplyAction="http://tempuri.org/IJoinGameService/ResponseCompleteLobbyResponse")]
        System.Threading.Tasks.Task<bool> ResponseCompleteLobbyAsync(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/StartGame")]
        void StartGame(string verificationCode);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/StartGame")]
        System.Threading.Tasks.Task StartGameAsync(string verificationCode);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJoinGameServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/ReciveWinner")]
        void ReciveWinner(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/ResponseTotalPlayers")]
        void ResponseTotalPlayers(int totalPlayers);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/SendNextHostGameResponse")]
        void SendNextHostGameResponse(bool status);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/SendCard")]
        void SendCard(int idCard);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJoinGameService/GoToPlay")]
        void GoToPlay(bool status);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJoinGameServiceChannel : View.ServiceReference.IJoinGameService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class JoinGameServiceClient : System.ServiceModel.DuplexClientBase<View.ServiceReference.IJoinGameService>, View.ServiceReference.IJoinGameService {
        
        public JoinGameServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public JoinGameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public JoinGameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JoinGameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JoinGameServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void JoinGame(string username, string verificationCode) {
            base.Channel.JoinGame(username, verificationCode);
        }
        
        public System.Threading.Tasks.Task JoinGameAsync(string username, string verificationCode) {
            return base.Channel.JoinGameAsync(username, verificationCode);
        }
        
        public void SendWinner(string username, string verificationCode) {
            base.Channel.SendWinner(username, verificationCode);
        }
        
        public System.Threading.Tasks.Task SendWinnerAsync(string username, string verificationCode) {
            return base.Channel.SendWinnerAsync(username, verificationCode);
        }
        
        public void ExitGame(string userName, string verificationCode) {
            base.Channel.ExitGame(userName, verificationCode);
        }
        
        public System.Threading.Tasks.Task ExitGameAsync(string userName, string verificationCode) {
            return base.Channel.ExitGameAsync(userName, verificationCode);
        }
        
        public void CreateGame(string verificationCode, int limitPlayers) {
            base.Channel.CreateGame(verificationCode, limitPlayers);
        }
        
        public System.Threading.Tasks.Task CreateGameAsync(string verificationCode, int limitPlayers) {
            return base.Channel.CreateGameAsync(verificationCode, limitPlayers);
        }
        
        public void EliminateGame(string verificationCode) {
            base.Channel.EliminateGame(verificationCode);
        }
        
        public System.Threading.Tasks.Task EliminateGameAsync(string verificationCode) {
            return base.Channel.EliminateGameAsync(verificationCode);
        }
        
        public void SendNextHostGame(string verificationCode) {
            base.Channel.SendNextHostGame(verificationCode);
        }
        
        public System.Threading.Tasks.Task SendNextHostGameAsync(string verificationCode) {
            return base.Channel.SendNextHostGameAsync(verificationCode);
        }
        
        public void GoToGame(string verificationCode) {
            base.Channel.GoToGame(verificationCode);
        }
        
        public System.Threading.Tasks.Task GoToGameAsync(string verificationCode) {
            return base.Channel.GoToGameAsync(verificationCode);
        }
        
        public bool ResponseCodeExist(string verificationCode) {
            return base.Channel.ResponseCodeExist(verificationCode);
        }
        
        public System.Threading.Tasks.Task<bool> ResponseCodeExistAsync(string verificationCode) {
            return base.Channel.ResponseCodeExistAsync(verificationCode);
        }
        
        public bool ResponseCompleteLobby(string verificationCode) {
            return base.Channel.ResponseCompleteLobby(verificationCode);
        }
        
        public System.Threading.Tasks.Task<bool> ResponseCompleteLobbyAsync(string verificationCode) {
            return base.Channel.ResponseCompleteLobbyAsync(verificationCode);
        }
        
        public void StartGame(string verificationCode) {
            base.Channel.StartGame(verificationCode);
        }
        
        public System.Threading.Tasks.Task StartGameAsync(string verificationCode) {
            return base.Channel.StartGameAsync(verificationCode);
        }
    }
}
