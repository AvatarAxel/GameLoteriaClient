using Logic;
using System;
using System.ServiceModel;
using System.Windows;

namespace View
{
    public partial class VE_EnterGameCode : Window
    {
        private ServiceReference.JoinGameServiceClient client;
        public VE_EnterGameCode()
        {
            InitializeComponent();
            client = new ServiceReference.JoinGameServiceClient();
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            try
            {
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            catch (CommunicationObjectFaultedException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            Close();
        }

        public void BtnJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            string codeVerification = txtCode.Text;
            if (!string.IsNullOrEmpty(codeVerification) )
            {
                if (ValidationLobby(codeVerification))
                {
                    try
                    {
                        SingletonGameRound.GameRound.CodeGame = txtCode.Text;
                        Lobby lobby = new Lobby();
                        lobby.Show();
                        GoLogin();
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        GoLogin();
                    }
                    catch (CommunicationException)
                    {
                        MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        GoLogin();
                    }
                }
            }
        }

        private bool ValidationLobby(string codeVerification) 
        {
            try
            {
                if (!client.ResponseCodeExist(codeVerification))
                {
                    MessageBox.Show("This room does not exist", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (client.ResponseCompleteLobby(codeVerification))
                {
                    MessageBox.Show("Room full", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
                if (SingletonPlayer.PlayerClient.RegisteredUser)
                {
                    if (!client.ValidateCoinsRegistered(SingletonPlayer.PlayerClient.Username, codeVerification))
                    {
                        MessageBox.Show("Insufficient coins", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                }
                else
                {
                    if (!client.ValidateCoinsUnregistered(SingletonPlayer.PlayerClient.Coin, codeVerification))
                    {
                        MessageBox.Show("Insufficient coins", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return false;
                    }
                }
                return true;
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            catch (CommunicationException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            return false;
        }

        private void GoLogin()
        {
            Login login = new Login();
            login.Show();
            Close();
        }

    }
}
