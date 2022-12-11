using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SingletonGameRound.GameRound = new SingletonGameRound();
            lbUsername.Text = SingletonPlayer.PlayerClient.Username;
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            SingletonPlayer.PlayerClient.Username = null;
            SingletonPlayer.PlayerClient.Coin = 0;
            SingletonPlayer.PlayerClient.Email = null;
            SingletonPlayer.PlayerClient.RegisteredUser = false;
            GoLogin();
        }
        private void BtnCreateGame_Click(object sender, RoutedEventArgs e)
        {
            if (SingletonPlayer.PlayerClient.RegisteredUser)
            {
                VE_StartUpSettings vE_StartUpSettings = new VE_StartUpSettings();
                SingletonPlayer.PlayerClient.PlayerType = true;
                vE_StartUpSettings.Show();               
                Close();
            }
            else
            {
                MessageBox.Show(Properties.Langs.Lang.youAreNOTRegisteredYet, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnJoinGame_Click(object sender, RoutedEventArgs e)
        {
            VE_EnterGameCode game = new VE_EnterGameCode();
            game.Show();
            Close();
        }

        private void BtnConfigureProfile_Click(object sender, RoutedEventArgs e)
        {
            if (SingletonPlayer.PlayerClient.RegisteredUser)
            {
                ConfigureProfile configureProfile = new ConfigureProfile();    
                configureProfile.Show();
                Close();
            }
            else
            {
                MessageBox.Show(Properties.Langs.Lang.youAreNOTRegisteredYet, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void GoLogin() {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
