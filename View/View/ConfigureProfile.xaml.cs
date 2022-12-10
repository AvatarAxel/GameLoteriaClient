using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

    public partial class ConfigureProfile : Window
    {
        private ServiceReference.ChangeUsernameServiceClient client;
        public ConfigureProfile()
        {
            InitializeComponent();
            client = new ServiceReference.ChangeUsernameServiceClient();
            lbUsername.Text = SingletonPlayer.PlayerClient.Username;
        }
        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            VE_PasswordChange passwordChange = new VE_PasswordChange();
            passwordChange.Show();
        }

        private void BtnUpdateUsername_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationFields())
            {
                bool changeUsername = client.ChangeUsername(SingletonPlayer.PlayerClient.Email, lbUsername.Text);
                if (changeUsername)
                {
                    MessageBox.Show(Properties.Langs.Lang.youHaveSuccessfullyUpdated, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(Properties.Langs.Lang.couldNotUpdateSuccessfully, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(Properties.Langs.Lang.nameIsInvalid, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public bool ValidationFields()
        {
            bool exitUsername = client.ValidateAvailabilityUsername(lbUsername.Text);
            FieldValidation fieldValidation = new FieldValidation();

            if (SingletonPlayer.PlayerClient.Username != lbUsername.Text && !exitUsername && lbUsername.Text != null && fieldValidation.ValidationUsernameFormat(lbUsername.Text))
            {
                return true;
            }

            return false;
        }
    }
}
