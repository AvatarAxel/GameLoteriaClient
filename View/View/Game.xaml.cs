using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        int numberPhoto = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            string relativeTabs = @"../../Images/Cards/";
            string[] photo = Directory.GetFiles(relativeTabs, "*.jpg");

            numberPhoto += 1;

            try
            {         
                string path = System.IO.Path.GetFullPath(photo[numberPhoto].ToString());
                Uri uri = new Uri(path, UriKind.Absolute);

                Position1.Stretch = Stretch.Fill;
                Position1.Source = new BitmapImage(uri);

                if (numberPhoto > 1)
                {
                    string otro = System.IO.Path.GetFullPath(photo[numberPhoto - 1].ToString());
                    Uri urin = new Uri(otro, UriKind.Absolute);
                    Position2.Stretch = Stretch.Fill;
                    Position2.Source = new BitmapImage(urin);
                }

                if (numberPhoto > 2)
                {
                    string otro1 = System.IO.Path.GetFullPath(photo[numberPhoto - 2].ToString());
                    Uri urin1 = new Uri(otro1, UriKind.Absolute);
                    Position3.Stretch = Stretch.Fill;
                    Position3.Source = new BitmapImage(urin1);
                }

            }
            catch (IndexOutOfRangeException E)
            {
                MessageBox.Show("No hay más archivos para mostrar", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
    }
}
