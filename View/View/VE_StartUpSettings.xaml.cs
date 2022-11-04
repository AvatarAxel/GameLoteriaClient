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
    /// Lógica de interacción para VE_StartUpSettings.xaml
    /// </summary>
    public partial class VE_StartUpSettings : Window
    {
        public VE_StartUpSettings()
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

        private void BtnAccept_Click(Object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby();
            lobby.Show();
            Close();
        }
    }
}
