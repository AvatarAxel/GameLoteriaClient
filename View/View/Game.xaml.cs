using Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using View.ServiceReference;

namespace View
{

    public partial class Game : Window, ILoteriaServiceCallback
    {
        private InstanceContext context;
        private LoteriaServiceClient client;
        private FillingOutTheLetter DeckCardRandom;
        private List<int> DeckOfCards;
        private List<Uri> uriList;
        private Queue displayedTail;
        private int counter;
        private List<int> photoListIndex;
        private int totalPlayers;
        private int counterCells;
        public Game()
        {
            InitializeComponent();
            InitiatingVariables();
            FillingOutLetter();
            CreateLoteriaGame();
        }

        private void InitiatingVariables() 
        {
            context = new InstanceContext(this);
            client = new LoteriaServiceClient(context);
            DeckCardRandom = new FillingOutTheLetter();
            uriList = new List<Uri>();
            counter = 0;
            counterCells = 0;
            totalPlayers = 0;
            photoListIndex = new List<int>();
            displayedTail = new Queue();
            displayedTail.Enqueue(0);
            displayedTail.Enqueue(0);
            displayedTail.Enqueue(0);
        }

        public void RecieveTotalPlayersLoteria(int totalPlayer) 
        {
            this.totalPlayers = totalPlayer;
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            if (counter >= 54) 
            {
                Close();
            }
        }

        private void BtnPosition1Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[0])) 
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[0]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position1Cards.Source = new BitmapImage(uri);

                Position1Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition2Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[1])) 
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[1]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position2Cards.Source = new BitmapImage(uri);
                Position2Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition3Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[2]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[2]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position3Cards.Source = new BitmapImage(uri);
                Position3Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition4Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[3]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[3]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position4Cards.Source = new BitmapImage(uri);
                Position4Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition5Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[4]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[4]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position5Cards.Source = new BitmapImage(uri);
                Position5Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition6Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[5]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[5]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position6Cards.Source = new BitmapImage(uri);
                Position6Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition7Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[6]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[6]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position7Cards.Source = new BitmapImage(uri);
                Position7Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition8Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[7]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[7]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position8Cards.Source = new BitmapImage(uri);
                Position8Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition9Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[8]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[8]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position9Cards.Source = new BitmapImage(uri);
                Position9Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition10Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[9]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[9]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position10Cards.Source = new BitmapImage(uri);
                Position10Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition11Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[10]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[10]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position11Cards.Source = new BitmapImage(uri);
                Position11Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition12Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[11]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[11]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position12Cards.Source = new BitmapImage(uri);
                Position12Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition13Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[12]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[12]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position13Cards.Source = new BitmapImage(uri);
                Position13Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition14Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[13]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[13]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position14Cards.Source = new BitmapImage(uri);
                Position14Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition15Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[14]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[14]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position15Cards.Source = new BitmapImage(uri);
                Position15Cards.IsEnabled = false;
                counterCells++;
            }
        }

        private void BtnPosition16Cards_Click(Object sender, RoutedEventArgs e)
        {
            if (displayedTail.Contains(DeckOfCards[15]))
            {
                string relativeTabs = @"../../Images/CardsBeans/";
                string[] photo = Directory.GetFiles(relativeTabs, "*.png");

                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[15]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position16Cards.Source = new BitmapImage(uri);
                Position16Cards.IsEnabled = false;
                counterCells++;
            }
        }
        
        public void SendCard(int idCard) 
        {
            string relativeTabs = @"../../Images/Cards/";
            string[] photo = Directory.GetFiles(relativeTabs, "*.jpg");

            photoListIndex.Add(idCard);
            UpdateQueue(idCard);
            try
            {
                string path = System.IO.Path.GetFullPath(photo[photoListIndex[counter]].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position1.Stretch = Stretch.Fill;
                Position1.Source = new BitmapImage(uri);

                if (counter >= 1)
                {
                    string otro = System.IO.Path.GetFullPath(photo[photoListIndex[counter - 1]].ToString());
                    Uri urin = new Uri(otro, UriKind.Absolute);
                    Position2.Stretch = Stretch.Fill;
                    Position2.Source = new BitmapImage(urin);
                }

                if (counter >= 2)
                {
                    string otro1 = System.IO.Path.GetFullPath(photo[photoListIndex[counter - 2]].ToString());
                    Uri urin1 = new Uri(otro1, UriKind.Absolute);
                    Position3.Stretch = Stretch.Fill;
                    Position3.Source = new BitmapImage(urin1);
                }
                counter++;

            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("There are no more files to display", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void FillingOutLetter()
        {
            DeckOfCards = DeckCardRandom.FillDeck();

            string relativeTabs = @"../../Images/Cards/";
            string[] photo = Directory.GetFiles(relativeTabs, "*.jpg");

            for (int i = 0; i < DeckOfCards.Count; i++)
            {
                string path = System.IO.Path.GetFullPath(photo[DeckOfCards[i]].ToString());
                
                Uri uri = new Uri(path, UriKind.Absolute);
                uriList.Add(uri);
            }
            
            Position1Cards.Stretch = Stretch.Fill;
            Position1Cards.Source = new BitmapImage(uriList[0]);

            Position2Cards.Stretch = Stretch.Fill;
            Position2Cards.Source = new BitmapImage(uriList[1]);

            Position3Cards.Stretch = Stretch.Fill;
            Position3Cards.Source = new BitmapImage(uriList[2]);

            Position4Cards.Stretch = Stretch.Fill;
            Position4Cards.Source = new BitmapImage(uriList[3]);

            Position5Cards.Stretch = Stretch.Fill;
            Position5Cards.Source = new BitmapImage(uriList[4]);

            Position6Cards.Stretch = Stretch.Fill;
            Position6Cards.Source = new BitmapImage(uriList[5]);

            Position7Cards.Stretch = Stretch.Fill;
            Position7Cards.Source = new BitmapImage(uriList[6]);

            Position8Cards.Stretch = Stretch.Fill;
            Position8Cards.Source = new BitmapImage(uriList[7]);

            Position9Cards.Stretch = Stretch.Fill;
            Position9Cards.Source = new BitmapImage(uriList[8]);

            Position10Cards.Stretch = Stretch.Fill;
            Position10Cards.Source = new BitmapImage(uriList[9]);

            Position11Cards.Stretch = Stretch.Fill;
            Position11Cards.Source = new BitmapImage(uriList[10]);

            Position12Cards.Stretch = Stretch.Fill;
            Position12Cards.Source = new BitmapImage(uriList[11]);

            Position13Cards.Stretch = Stretch.Fill;
            Position13Cards.Source = new BitmapImage(uriList[12]);

            Position14Cards.Stretch = Stretch.Fill;
            Position14Cards.Source = new BitmapImage(uriList[13]);

            Position15Cards.Stretch = Stretch.Fill;
            Position15Cards.Source = new BitmapImage(uriList[14]);

            Position16Cards.Stretch = Stretch.Fill;
            Position16Cards.Source = new BitmapImage(uriList[15]);
        }

        private void CreateLoteriaGame()
        {
            if (SingletonPlayer.PlayerClient.PlayerType)
            {
                try
                {
                    client.CreateLoteria(SingletonGameRound.GameRound.CodeGame);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
                catch (CommunicationException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
            }
        }

        public void JoinServices()
        {
            try
            {
                client.JoinLoteria(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame);
                if (!SingletonPlayer.PlayerClient.RegisteredUser) 
                {
                    SingletonPlayer.PlayerClient.Coin = SingletonPlayer.PlayerClient.Coin - SingletonGameRound.GameRound.Bet;
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(CommunicationException) 
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateQueue(int idCard) 
        {                        
            displayedTail.Enqueue(idCard);
            displayedTail.Dequeue();
        }

        public void SendWinner(string username)
        {
            btnLoteria.IsEnabled = false;
            MessageBox.Show(Properties.Langs.Lang.weHaveWinner + username, Properties.Langs.Lang.winner, MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }

        private void btnLoteria_Click(object sender, RoutedEventArgs e)
        {
            if (counterCells == 16)
            {
                try
                {
                    btnLoteria.IsEnabled = false;
                    int bet = CalculateBet();
                    client.ReciveWinner(SingletonPlayer.PlayerClient.Username, SingletonGameRound.GameRound.CodeGame, bet);
                    if (!SingletonPlayer.PlayerClient.RegisteredUser)
                    {
                        SingletonPlayer.PlayerClient.Coin += bet;
                    }
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
                catch (CommunicationException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
            }
        }

        private int CalculateBet()
        {
            int totalBet = SingletonGameRound.GameRound.Bet * totalPlayers;
            return totalBet;
        }

        public void StartGame() 
        {
            if (SingletonPlayer.PlayerClient.PlayerType)
            {
                try
                {
                    client.StartGameLoteria(SingletonGameRound.GameRound.CodeGame);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
                catch (CommunicationException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
            }
        }

        private void GoLogin()
        {
            Login login = new Login();
            login.Show();
            Close();
        }

    }
}
