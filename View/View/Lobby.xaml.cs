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
    /// <summary>
    /// Lógica de interacción para Lobby.xaml
    /// </summary>
    public partial class Lobby : Window, ServiceReference.IJoinGameServiceCallback
    {
        public Lobby()
        {
            InitializeComponent();
            ConfigureLobby();
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            if (SingletonPlayer.PlayerClient.PlayerType)
            {

                InstanceContext context = new InstanceContext(this);
                ServiceReference.JoinGameServiceClient client = new ServiceReference.JoinGameServiceClient(context);
                
                ExitPlayer();
                client.EliminateGame(SingletonGameRound.GameRound.CodeGame);

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

        public void CodeExist(bool status)
        {
            throw new NotImplementedException();
        }

        private void ExitPlayer()
        {
            InstanceContext context = new InstanceContext(this);
            ServiceReference.JoinGameServiceClient client = new ServiceReference.JoinGameServiceClient(context);

            client.ExitGame(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
        }
    }
}
