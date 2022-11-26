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
            client = new ServiceReference.JoinGameServiceClient(context);
        }

        public void BtnJoinLobby_Click(object sender, RoutedEventArgs e)
        {
            string codeVerification = txtCode.Text;
            if (!string.IsNullOrEmpty(codeVerification) ) {
                try
                {
                    if (!client.ResponseCodeExist(codeVerification))
                    {
                        MessageBox.Show("No existe esa sala", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (client.ResponseCompleteLobby(codeVerification))
                    {
                        MessageBox.Show("Sala llena uwu", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
            }
            Close();
        }

        public void ReciveWinner(string username)
        {
            throw new NotImplementedException();
        }

        public void ResponseTotalPlayers(int totalPlayers)
        {
            throw new NotImplementedException();
        }

        public void SendNextHostGameResponse(bool status)
        {
            throw new NotImplementedException();
        }

        public void SendCard(int idCard)
        {
            throw new NotImplementedException();
        }

        public void GoToPlay(bool status)
        {
            throw new NotImplementedException();
        }
    }

}
