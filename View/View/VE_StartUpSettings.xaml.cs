using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
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
                if (SingletonPlayer.PlayerClient.Coin < int.Parse(cmbxAmountOfMoney.Text.Substring(1)))
                {
                    MessageBox.Show(Properties.Langs.Lang.insufficientCoins, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    GameRoundDTO gameRoundDTO = new GameRoundDTO();
                    CodeGame codeGame = new CodeGame();
                    SendSpeed();
                    SendTypeGame();
                    SingletonGameRound.GameRound.CodeGame = codeGame.GenerateRandomCode();
                    gameRoundDTO.VerificationCode = SingletonGameRound.GameRound.CodeGame;
                    gameRoundDTO.LimitPlayer = int.Parse(cmbxNumberPlayer.Text);
                    gameRoundDTO.Bet = int.Parse(cmbxAmountOfMoney.Text.Substring(1));
                    gameRoundDTO.Speed = SingletonGameRound.GameRound.SpeedGame;
                    gameRoundDTO.PrivateGame = SingletonGameRound.GameRound.PrivateGame;

                    try
                    {
                        gameClient.CreateGame(gameRoundDTO);
                        chatClient.CreateChat(SingletonGameRound.GameRound.CodeGame);
                        Lobby lobby = new Lobby();
                        lobby.Show();
                        gameClient.Close();
                        chatClient.Close();
                        Close();
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                        GoLogin();
                    }
                    Close();
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
                SingletonPlayer.PlayerClient.PlayerType = false;
                gameClient.Close();
                chatClient.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            Close();
        }

        private bool ValidationField()
        {
            if (cmbxNumberPlayer.SelectedIndex == -1 || cmbxAmountOfMoney.SelectedIndex == -1)
            {
                MessageBox.Show(Properties.Langs.Lang.rectifyTheFields, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (rdbtPrivate.IsChecked == true || rdbtPublic.IsChecked == true || rdbtnSlow.IsChecked == true  || rdbtnStandard.IsChecked == true || rdbtQuickly.IsChecked == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show(Properties.Langs.Lang.rectifyTheFields, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }

        public void ResponseTotalPlayers(int totalPlayers)
        {
            SingletonGameRound.GameRound.TotalPlayers = totalPlayers;
        }

        public void ReciveMessage(string player, string message)
        {
            throw new NotImplementedException();
        }

        public void SendNextHostGameResponse(bool status)
        {
            throw new NotImplementedException();
        }

        public void GoToPlay(bool status)
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

        public void SendTypeGame()
        {
            if (rdbtPrivate.IsChecked == true)
            {
                SingletonGameRound.GameRound.PrivateGame = true;
            }
            else
            {
                SingletonGameRound.GameRound.PrivateGame = false;
            }
        }

        public void GetListPlayer(string[] PlayerLobby)
        {
            throw new NotImplementedException();
        }

        public void UpdateBetCoinsResponse(int coins, int bet)
        {
            throw new NotImplementedException();
        }
        public void BanPlayerResponse(bool status)
        {
            throw new NotImplementedException();
        }
        private void GoLogin()
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
