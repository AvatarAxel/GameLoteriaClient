using Logic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using View.ServiceReference;
using Path = System.Windows.Shapes.Path;

namespace View
{

    public partial class Game : Window
    {
        private FillingOutTheLetter DeckCardRandom = new FillingOutTheLetter();
        private List<int> DeckOfCards;
        private List<Uri> uriList = new List<Uri>();
        public Game()
        {
            InitializeComponent();
            FillingOutLetter();
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnPosition1Cards_Click(Object sender, RoutedEventArgs e)
        {

            string relativeTabs = @"../../Images/CardsBeans/";
            string[] photo = Directory.GetFiles(relativeTabs, "*.png");

            string path = System.IO.Path.GetFullPath(photo[DeckOfCards[0]].ToString());
            Uri uri = new Uri(path, UriKind.Absolute);

            Position1Cards.Source = new BitmapImage(uri);

            Position1Cards.IsEnabled = false;
        }
        private void BtnPosition2Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position2Cards.IsEnabled = false;
        }
        private void BtnPosition3Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position3Cards.IsEnabled = false;
        }

        private void BtnPosition4Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position4Cards.IsEnabled = false;
        }

        private void BtnPosition5Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position5Cards.IsEnabled = false;
        }

        private void BtnPosition6Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position6Cards.IsEnabled = false;
        }

        private void BtnPosition7Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position7Cards.IsEnabled = false;
        }

        private void BtnPosition8Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position8Cards.IsEnabled = false;
        }

        private void BtnPosition9Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position9Cards.IsEnabled = false;
        }

        private void BtnPosition10Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position10Cards.IsEnabled = false;
        }

        private void BtnPosition11Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position11Cards.IsEnabled = false;
        }

        private void BtnPosition12Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position12Cards.IsEnabled = false;
        }

        private void BtnPosition13Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position13Cards.IsEnabled = false;
        }

        private void BtnPosition14Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position14Cards.IsEnabled = false;
        }

        private void BtnPosition15Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position15Cards.IsEnabled = false;
        }

        private void BtnPosition16Cards_Click(Object sender, RoutedEventArgs e)
        {
            Position16Cards.IsEnabled = false;
        }

        private int counter = 0;
        private List<int> photoListIndex = new List<int>();
        
        public void ReciveCard(int idCard) 
        {
            string relativeTabs = @"../../Images/Cards/";
            string[] photo = Directory.GetFiles(relativeTabs, "*.jpg");

            photoListIndex.Add(idCard);
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
    }
}
