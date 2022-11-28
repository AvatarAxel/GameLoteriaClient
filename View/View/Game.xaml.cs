using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

    public partial class Game : Window
    {
        public Game()
        {
            InitializeComponent();
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

        private int counter = 0;
        private List<int> photoListIndex = new List<int>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("LOTERIA", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        public void SendCard(int idCard) 
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
    }
}
