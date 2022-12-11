using Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using View.ServiceReference;

namespace View
{

    public partial class Lobby : Window, ServiceReference.IGameServiceCallback, ServiceReference.IChatServiceCallback
    {
        private InstanceContext context;
        private ServiceReference.ChatServiceClient chatClient;
        private ServiceReference.GameServiceClient GameServiceClient;
        private Game game;
        private Encryption encryption = new Encryption();

        public Lobby()
        {
            InitializeComponent();
            ConfigureLobby();
            JoinServices();
            txtChat.IsEnabled = false;
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            if (SingletonPlayer.PlayerClient.PlayerType)
            {
                ExitPlayer();
                try
                {
                    GameServiceClient.SendNextHostGame(SingletonGameRound.GameRound.CodeGame);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
            }
            else
            {
                ExitPlayer();
            }

            try
            {
                GameServiceClient.Close();
                chatClient.Close();
            }
            catch(CommunicationObjectAbortedException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            catch (CommunicationException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (SingletonGameRound.GameRound.TotalPlayers >= 3)
            {
                try
                {
                    GameServiceClient.GoToGame(SingletonGameRound.GameRound.CodeGame);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
            }
            else
            {
                MessageBox.Show(Properties.Langs.Lang.theMinimumNumberOfPlayers, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {          
            string message = txtMessage.Text;       

            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    string messageEncryptation = encryption.EncryptionMessage(message);

                    chatClient.SendMessage(messageEncryptation, SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                    txtMessage.Clear();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
            }
        }

        public void JoinServices()
        {
            try
            {
                GameServiceClient.JoinGame(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                chatClient.JoinChat(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                GameServiceClient.UpdateBetCoins(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            catch (TimeoutException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
        }

        private void ConfigureLobby()
        {
            context = new InstanceContext(this);
            chatClient = new ServiceReference.ChatServiceClient(context);
            GameServiceClient = new ServiceReference.GameServiceClient(context);
            if (SingletonPlayer.PlayerClient.PlayerType)
            {
                lbCodeVerificationTitle.Text = Properties.Langs.Lang.codeVerification;
                lbCodeVerification.Text = SingletonGameRound.GameRound.CodeGame;
            }
            else
            {
                btnPlay.Visibility = Visibility.Collapsed;
                btnSignOutPlayer.Visibility = Visibility.Collapsed;
            }
        }

        private void ExitPlayer()
        {
            try
            {
                GameServiceClient.ExitGame(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                chatClient.ExitChat(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            catch (CommunicationObjectAbortedException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            finally 
            {
                SingletonPlayer.PlayerClient.PlayerType = false;
            }
        }

        public void ReciveMessage(string player, string message)
        {
           string messageDescryption = encryption.DescryptionMessage(message);
            txtChat.Text += player + ":  " + messageDescryption + "\r\n";
        }

        public void ResponseTotalPlayers(int totalPlayers)
        {
            SingletonGameRound.GameRound.TotalPlayers = totalPlayers;
            lbTotalPlayers.Text = SingletonGameRound.GameRound.TotalPlayers.ToString();
        }

        public void SendNextHostGameResponse(bool status)
        {
            SingletonPlayer.PlayerClient.PlayerType = status;
            lbCodeVerificationTitle.Text = Properties.Langs.Lang.codeVerification;
            lbCodeVerification.Text = SingletonGameRound.GameRound.CodeGame;
            btnPlay.Visibility = Visibility.Visible;
            btnSignOutPlayer.Visibility = Visibility.Visible;
        }

        public void GoToPlay(bool status)
        {
            if (status)
            {
                if (SingletonPlayer.PlayerClient.Coin >= SingletonGameRound.GameRound.Bet)
                {
                    EnterGame();
                }
                else
                {
                    MessageBox.Show(Properties.Langs.Lang.insufficientCoins, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    ExitPlayer();
                    Close();
                }         
            }
        }

        private void EnterGame() 
        {
            game = new Game();
            game.RecieveTotalPlayersLoteria(SingletonGameRound.GameRound.TotalPlayers);
            game.JoinServices();
            game.StartGame();
            game.ShowDialog();
            try
            {
                GameServiceClient.UpdateBetCoins(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
            }
            catch
            {
                Close();
            }
        }

        public void UpdateBetCoinsResponse(int coins, int bet)
        {
            SingletonGameRound.GameRound.Bet = bet;
            if (SingletonPlayer.PlayerClient.RegisteredUser)
            {
                SingletonPlayer.PlayerClient.Coin = coins;
            }
            lbCoins.Text = SingletonPlayer.PlayerClient.Coin.ToString();
        }

        public void GetListPlayer(string[] PlayerLobby)
        {
            ListPlayers.Items.Clear();
            for (int i = 0; i < PlayerLobby.Length; i++)
            {
                ListPlayers.Items.Add(PlayerLobby[i]);
            }
        }

        private void BtnAddFriend_Click(object sender, RoutedEventArgs e)
        {
            if (ListPlayers.SelectedIndex >= 0)
            {
                if (SingletonPlayer.PlayerClient.RegisteredUser)
                {
                    string username = ListPlayers.SelectedItem.ToString();
                    if (username != SingletonPlayer.PlayerClient.Username)
                    {
                        /* Validar cuantos amigos tiene en total
                         * Dependiendo de la cantidad realizar
                         * if <30
                         *  Enviarle la solicutud al men y decirle al otro men que ya se envio
                         *  El men debe de aceptarla o denegarla
                         *  Aceptar : Se actualiza la lista de amigos de ambos
                         *  Denegar : Pues no hace nada
                         */
                    }
                    else
                    {
                        MessageBox.Show(Properties.Langs.Lang.youCantBeYourOwnFriend, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(Properties.Langs.Lang.youAreNOTRegisteredYet, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BtnSignOutPlayer_Click(object sender, RoutedEventArgs e)
        {
            if(ListPlayers.SelectedIndex >= 0)
            {
                string username = ListPlayers.SelectedItem.ToString();
                if (username != SingletonPlayer.PlayerClient.Username)
                {
                    GameServiceClient.BanPlayer(SingletonGameRound.GameRound.CodeGame, username);
                }
                else
                {
                    MessageBox.Show(Properties.Langs.Lang.doNotRemoveYourself, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }   
        }

        public void BanPlayerResponse(bool status)
        {
            if (status)
            {
                GameServiceClient.Close();
                chatClient.Close();
                MessageBox.Show(Properties.Langs.Lang.youHaveBeenExpelled, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }

        private void GoLogin()
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    
    }
}
