﻿using Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        private Login login = new Login();

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
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    login.Show();
                    Close();
                }
                catch (CommunicationObjectFaultedException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
            }
            catch (CommunicationException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GameServiceClient.GoToGame(SingletonGameRound.GameRound.CodeGame);
            }
            catch(TimeoutException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
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
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    login.Show();
                    Close();
                }
                catch (CommunicationObjectFaultedException)
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
                GameServiceClient.UpdateBetCoins(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConfigureLobby()
        {
            context = new InstanceContext(this);
            chatClient = new ServiceReference.ChatServiceClient(context);
            GameServiceClient = new ServiceReference.GameServiceClient(context);
            if (SingletonPlayer.PlayerClient.PlayerType)
            {
                lbCodeVerificationTitle.Text = "Code Verification";
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
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
            }
            catch (CommunicationObjectAbortedException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
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
            lbCodeVerificationTitle.Text = "Code Verification";
            lbCodeVerification.Text = SingletonGameRound.GameRound.CodeGame;
            btnPlay.Visibility = Visibility.Visible;
        }

        public void GoToPlay(bool status)
        {
            if (status)
            {
                game = new Game();
                game.RecieveTotalPlayersLoteria(SingletonGameRound.GameRound.TotalPlayers);
                game.StartGame();
                game.ShowDialog();
                GameServiceClient.UpdateBetCoins(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
            }
            else 
            {
                MessageBox.Show("You don't have enough coins", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                ExitPlayer();
                Close();
            }
        }

        public void UpdateBetCoinsResponse(int coins, int bet)
        {
            SingletonPlayer.PlayerClient.Coin = coins;
            SingletonGameRound.GameRound.Bet = bet;
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
            if (ListPlayers.SelectedIndex > 0)
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
                    MessageBox.Show("You can't be your own friend", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnSignOutPlayer_Click(object sender, RoutedEventArgs e)
        {
            if(ListPlayers.SelectedIndex > 0)
            {
                string username = ListPlayers.SelectedItem.ToString();
                if (username != SingletonPlayer.PlayerClient.Username)
                {
                    GameServiceClient.BanPlayer(SingletonGameRound.GameRound.CodeGame, username);
                }
                else
                {
                    MessageBox.Show("Do not remove yourself", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }   
        }

        public void BanPlayerResponse(bool status)
        {
            if (status)
            {
                GameServiceClient.Close();
                chatClient.Close();
                MessageBox.Show("You have been expelled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            
            }
        }
    }
}
