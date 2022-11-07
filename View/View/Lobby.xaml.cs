using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Lógica de interacción para Lobby.xaml
    /// </summary>
    public partial class Lobby : Window
    {
        public Lobby()
        {
            InitializeComponent();
            GenerateRandomCode();
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("UwU", "Verificar", MessageBoxButton.OK, MessageBoxImage.Information);
        }

            public string GenerateRandomCode()
        {
            var random = new Random();
            var value = random.Next(0, 10000);

            string verificationCode = value.ToString();

            return this.lbCodeVerification.Text = verificationCode;
        }
    }
}
