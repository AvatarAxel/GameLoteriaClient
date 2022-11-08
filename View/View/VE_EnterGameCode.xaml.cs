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
    /// Interaction logic for VE_IngresarCodigoGame.xaml
    /// </summary>
    public partial class VE_EnterGameCode : Window, ServiceReference.IJoinGameServiceCallback
    {
        public VE_EnterGameCode()
        {
            InitializeComponent();
        }

        public void BtnJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            string codeVerification = txtCode.Text;
            InstanceContext context = new InstanceContext(this);
            ServiceReference.JoinGameServiceClient client = new ServiceReference.JoinGameServiceClient(context);
            try
            {
                 client.JoinGame(SingletonPlayer.PlayerClient.Username, codeVerification);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CodeExist(bool status)
        {
            if (status)
            {
                Lobby lobby = new Lobby();
                lobby.Show();
                SingletonGameRound.GameRound.CodeGame = txtCode.Text;
                Close();
            }
            else
            {
                MessageBox.Show("No existe esa sala", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ReciveWinner(string username)
        {
            throw new NotImplementedException();
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }

}
