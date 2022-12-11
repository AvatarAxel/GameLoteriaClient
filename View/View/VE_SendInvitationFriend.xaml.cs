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
using System.Xml.Linq;

namespace View
{
    public partial class VE_SendInvitationFriend : Window
    {
        private string submitter;
        public VE_SendInvitationFriend()
        {
            InitializeComponent();
        }

        public void NameOfSender(string sender)
        {
            submitter = sender;
            lbSender.Text = submitter;
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnAccept_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Langs.Lang.youHaveNewFriend, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Close();
        }
        private void BtnDecline_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Properties.Langs.Lang.applicationRejected, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
