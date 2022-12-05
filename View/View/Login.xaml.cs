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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic;
using View.ServiceReference;

namespace View
{
    public partial class Login : Window
    {
        private ServiceReference.AuthenticationServiceClient client;

        public Login()
        { 
            InitializeComponent();
            SingletonPlayer.PlayerClient = new SingletonPlayer();
            client = new ServiceReference.AuthenticationServiceClient();
        }

        public void AuthenticateLogin(PlayerDTO playerDTO)
        {
            if (playerDTO.IsActive)
            {
                SingletonPlayer.PlayerClient.Username = playerDTO.Username;
                SingletonPlayer.PlayerClient.Email = playerDTO.Email;
                SingletonPlayer.PlayerClient.Coin = playerDTO.Coin;
                SingletonPlayer.PlayerClient.RegisteredUser = true;
                SingletonPlayer.PlayerClient.PlayerType = false;

                try
                {
                    client.Close();

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("The username and/or password you entered are not registered.", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Password) || string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Rectify the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
            {               
                string username;
                string hashedPassword;
                username = txtUser.Text;
                Encryption encryption = new Encryption();
                hashedPassword = encryption.HashPassword256(txtPassword.Password);
                try
                {
                    PlayerDTO playerDTO;
                    playerDTO= client.AuthenticationLogin(username, hashedPassword);
                    AuthenticateLogin(playerDTO);

                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }    
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            
            RegisterUser registerUser = new RegisterUser();
            registerUser.Show();
            try
            {
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();

        }

        private void LbReset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VE_PasswordChange passwordChange = new VE_PasswordChange();
            passwordChange.Show();
            try
            {
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private void LbPlayingAsGuest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var random = new Random();
            var value = random.Next(0, 10000);
            SingletonPlayer.PlayerClient.Username = "Guest" + value;
            SingletonPlayer.PlayerClient.Coin = 500;
            SingletonPlayer.PlayerClient.RegisteredUser = false;
            MainWindow mainWindow = new MainWindow();
            try
            {
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
            mainWindow.Show();
            MessageBox.Show("Your earned coins will not be saved", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLanguage.SelectedIndex == 0)
                Properties.Settings.Default.languageCode = "EN";
            else 
                Properties.Settings.Default.languageCode = "SP";
           
            Properties.Settings.Default.Save();
        }
    }
}
