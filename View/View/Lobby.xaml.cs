using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{

    public partial class Lobby : Window, ServiceReference.IGameServiceCallback, ServiceReference.IChatServiceCallback
    {
        private InstanceContext context;
        private ServiceReference.ChatServiceClient chatClient;
        private ServiceReference.JoinGameServiceClient joinGameServiceClient;
        private Game game = new Game();  

        public Lobby()
        {
            InitializeComponent();
            context = new InstanceContext(this);
            chatClient = new ServiceReference.ChatServiceClient(context);
            joinGameServiceClient = new ServiceReference.JoinGameServiceClient();
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
                    //joinGameServiceClient.SendNextHostGame(SingletonGameRound.GameRound.CodeGame);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ExitPlayer();
            }
            joinGameServiceClient.Close();
            chatClient.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();

        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                //joinGameServiceClient.GoToGame(SingletonGameRound.GameRound.CodeGame);
                //joinGameServiceClient.StartGame(SingletonGameRound.GameRound.CodeGame , SingletonGameRound.GameRound.SpeedGame);
            }
            catch(TimeoutException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
  
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string message = txtMessage.Text;
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    chatClient.SendMessage(message, SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                    txtMessage.Clear();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void JoinServices()
        {
            try
            {
                //joinGameServiceClient.JoinGame(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                chatClient.JoinChat(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConfigureLobby()
        {
            if (SingletonPlayer.PlayerClient.PlayerType)
            {
                lbCodeVerificationTitle.Text = "Code Verification";
                lbCodeVerification.Text = SingletonGameRound.GameRound.CodeGame;
            }
            else
            {
                btnPlay.Visibility = Visibility.Collapsed;
            }

        }

        public void ReciveWinner(string username)
        {
            throw new NotImplementedException();
        }

        private void ExitPlayer()
        {
            try
            {
                //joinGameServiceClient.ExitGame(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                chatClient.ExitChat(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SingletonPlayer.PlayerClient.PlayerType = false;
        }

        public void ReciveMessage(string player, string message)
        {
            //Se suma y se va a caer
            txtChat.Text += player + ":  " + message + "\r\n";
        }

        public void ResponseTotalPlayers(int totalPlayers)
        {
            SingletonGameRound.GameRound.TotalPlayers = totalPlayers;
        }

        public void SendNextHostGameResponse(bool status)
        {
            SingletonPlayer.PlayerClient.PlayerType = status;
            lbCodeVerificationTitle.Text = "Code Verification";
            lbCodeVerification.Text = SingletonGameRound.GameRound.CodeGame;
            btnPlay.Visibility = Visibility.Visible;
        }

        public void GoToPlay(bool status)
        {
            if (status)
            {
                game.Show();
            }
        }

        public void SendCard(int idCard)
        {
            game.SendCard(idCard);
        }

       
    }
}
