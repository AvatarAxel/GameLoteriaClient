using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

    public partial class Lobby : Window, ServiceReference.IJoinGameServiceCallback, ServiceReference.IChatServiceCallback
    {
        private InstanceContext context;
        ServiceReference.ChatServiceClient client;
        ServiceReference.JoinGameServiceClient joinGameServiceClient;
        public Lobby()
        {
            InitializeComponent();
            context = new InstanceContext(this);
            ConfigureLobby();
            JoinServices();
            txtChat.IsEnabled = false;
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public void JoinServices()        {

            client = new ServiceReference.ChatServiceClient(context);
            joinGameServiceClient = new ServiceReference.JoinGameServiceClient(context);
            try
            {
                joinGameServiceClient.JoinGame(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                client.JoinChat(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            if (SingletonPlayer.PlayerClient.PlayerType)
            {

                ExitPlayer();
                try
                {
                    joinGameServiceClient.EliminateGame(SingletonGameRound.GameRound.CodeGame);
                    client.DeleteChat(SingletonGameRound.GameRound.CodeGame);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                ExitPlayer();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }

        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("UwU", "Verificar", MessageBoxButton.OK, MessageBoxImage.Information);
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
                btnPlay.IsEnabled = false;
            }

        }

        public void ReciveWinner(string username)
        {
            throw new NotImplementedException();
        }

        public void ResponseCodeExist(bool status)
        {

        }

        private void ExitPlayer()
        {

            try
            {
                joinGameServiceClient.ExitGame(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                client.ExitChat(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference.ChatServiceClient client = new ServiceReference.ChatServiceClient(context);
            string message = txtMessage.Text;
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    client.SendMessage(message, SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                    txtMessage.Clear();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ReciveMessage(string player, string message)
        {
            txtChat.Text += player + ":  " + message + "\r\n";
        }

        public void ResponseCompleteLobby(bool status)
        {

        }

        public void ResponseTotalPlayers(int totalPlayers)
        {

        }
    }
}
