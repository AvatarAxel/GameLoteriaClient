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
using Logic;
using View.ServiceReference;

namespace View
{
    public partial class VE_StartUpSettings : Window, ServiceReference.IJoinGameServiceCallback
    {
        private InstanceContext context;
        public VE_StartUpSettings()
        {
            InitializeComponent();
            context = new InstanceContext(this);
        }
        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void BtnAccept_Click(Object sender, RoutedEventArgs e)
        {
            int limitPlayer = (int)cmbxNumberPlayer.SelectedItem;

            ServiceReference.JoinGameServiceClient client = new ServiceReference.JoinGameServiceClient(context);
            CodeGame codeGame = new CodeGame();  
            SingletonGameRound.GameRound.CodeGame = codeGame.GenerateRandomCode();

            try
            {
                client.CreateGame(SingletonGameRound.GameRound.CodeGame);
                Lobby lobby = new Lobby();
                lobby.Show();
                Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                client.Close();
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
    }
}
