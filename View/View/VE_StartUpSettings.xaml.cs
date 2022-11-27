using System;
using System.ServiceModel;
using System.Windows;
using Logic;
using View.ServiceReference;

namespace View
{
    public partial class VE_StartUpSettings : Window, ServiceReference.IGameServiceCallback, ServiceReference.IChatServiceCallback
    {
        private InstanceContext context;
        private GameServiceClient gameClient;
        private ChatServiceClient chatClient;
        public VE_StartUpSettings()
        {
            InitializeComponent();
            context = new InstanceContext(this);
            gameClient = new GameServiceClient(context);
            chatClient = new ChatServiceClient(context);
        }

        private void BtnAccept_Click(Object sender, RoutedEventArgs e)
        {
            if (ValidationField())
            {
                int limitPlayer = int.Parse( cmbxNumberPlayer.Text);

                CodeGame codeGame = new CodeGame();
                SingletonGameRound.GameRound.CodeGame = codeGame.GenerateRandomCode();

                try
                {
                    gameClient.CreateGame(SingletonGameRound.GameRound.CodeGame, limitPlayer);
                    chatClient.CreateChat(SingletonGameRound.GameRound.CodeGame);

                    SendSpeed();
                    Lobby lobby = new Lobby();
                    lobby.Show();

                    try
                    {
                        gameClient.Close();
                        chatClient.Close();
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
                gameClient.Close();
                chatClient.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private bool ValidationField()
        {
            if (cmbxNumberPlayer.SelectedIndex == -1 || cmbxAmountOfMoney.SelectedIndex == -1)
            {
                MessageBox.Show("Rectify the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (rdbtPrivate.IsChecked == true || rdbtPublic.IsChecked == true || rdbtnSlow.IsChecked == true  || rdbtnStandard.IsChecked == true || rdbtQuickly.IsChecked == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Rectify the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }

        public void ReciveWinner(string username)
        {

        }

        public void ResponseTotalPlayers(int totalPlayers)
        {
            SingletonGameRound.GameRound.TotalPlayers = totalPlayers;
        }

        public void ReciveMessage(string player, string message)
        {

        }

        public void SendNextHostGameResponse(bool status)
        {

        }

        public void GoToPlay(bool status)
        {
            throw new NotImplementedException();
        }

        public void SendCard(int idCard)
        {
            throw new NotImplementedException();
        }

        public void SendSpeed()
        {
            if (rdbtnSlow.IsChecked == true)
            {
                SingletonGameRound.GameRound.SpeedGame = 3000;
            }
            else if (rdbtnStandard.IsChecked == true)
            {
                SingletonGameRound.GameRound.SpeedGame = 2000;
            }
            else if (rdbtQuickly.IsChecked == true)
            {
                SingletonGameRound.GameRound.SpeedGame = 1000;
            }
        }

    }
}
