﻿using Logic;
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
        private ServiceReference.GameServiceClient GameServiceClient;
        private Game game = new Game();
        private Encryption encryption = new Encryption();

        public Lobby()
        {
            InitializeComponent();
            context = new InstanceContext(this);
            chatClient = new ServiceReference.ChatServiceClient(context);
            GameServiceClient = new ServiceReference.GameServiceClient(context);
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
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ExitPlayer();
            }
            GameServiceClient.Close();
            chatClient.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();

        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                GameServiceClient.GoToGame(SingletonGameRound.GameRound.CodeGame);
                GameServiceClient.StartGame(SingletonGameRound.GameRound.CodeGame);
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
                    string algo = encryption.EncryptionMessage(message);

                    chatClient.SendMessage(algo, SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
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
                GameServiceClient.JoinGame(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
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
                GameServiceClient.ExitGame(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
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
            catch (CommunicationObjectAbortedException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SingletonPlayer.PlayerClient.PlayerType = false;
        }

        public void ReciveMessage(string player, string message)
        {
           string messageDescryption = encryption.DescryptionMessage(message);

            txtChat.Text += player + ":  " + messageDescryption + "\r\n";
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
            game.ReciveCard(idCard);
        }

       
    }
}
