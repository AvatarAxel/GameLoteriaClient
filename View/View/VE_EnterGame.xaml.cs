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
    public partial class VE_EnterGameCode : Window
    {
        private ServiceReference.JoinGameServiceClient client;
        private Login login = new Login();
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
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
            }
            Close();
        }

        public void BtnJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            string codeVerification = txtCode.Text;
            if (!string.IsNullOrEmpty(codeVerification) ) {
                try
                {
                    if (!client.ResponseCodeExist(codeVerification))
                    {
                        MessageBox.Show(Properties.Langs.Lang.thisRoomDoesNotExist, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (client.ResponseCompleteLobby(codeVerification))
                    {
                        MessageBox.Show(Properties.Langs.Lang.roomFull, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (client.ResponseUsernameExist(codeVerification, SingletonPlayer.PlayerClient.Username))
                    {
                        MessageBox.Show(Properties.Langs.Lang.youCannotLogInTwice, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    SingletonGameRound.GameRound.CodeGame = txtCode.Text;
                    Lobby lobby = new Lobby();
                    lobby.Show();

                    client.Close();
                    Close();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    login.Show();
                    Close();
                }
            }
        }

    }

}
