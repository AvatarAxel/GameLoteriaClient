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
    public partial class VE_EnterGameCode : Window, ServiceReference.IJoinGameServiceCallback
    {
        private InstanceContext context;
        private ServiceReference.JoinGameServiceClient client;
        public VE_EnterGameCode()
        {
            InitializeComponent();
            context = new InstanceContext(this);
        }

        public void BtnJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            string codeVerification = txtCode.Text;
            client = new ServiceReference.JoinGameServiceClient(context);
            try
            {
                client.ValidationLobby(codeVerification);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ResponseCodeExist(bool status)
        {
            if (!status)          
                MessageBox.Show("No existe esa sala", "Error", MessageBoxButton.OK, MessageBoxImage.Error);            
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
            //client.Close();
            Close();
        }

        public void ResponseCompleteLobby(bool status)
        {
            if (status)
            {
                MessageBox.Show("Sala llena uwu", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SingletonGameRound.GameRound.CodeGame = txtCode.Text;
                Lobby lobby = new Lobby();
                lobby.Show();              
                client.Close();
                Close();
            }

        }

        public void ResponseTotalPlayers(int totalPlayers)
        {

        }
    }

}
