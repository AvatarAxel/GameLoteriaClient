using Logic;
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
using View.ServiceReference;

namespace View
{
    public partial class VE_PasswordChange : Window
    {
        private ServiceReference.ChangePasswordServiceClient client;

        public VE_PasswordChange()
        {
            InitializeComponent();
            client = new ServiceReference.ChangePasswordServiceClient();
            SingletonPlayer.PlayerClient.Verificated = false;
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            GoLogin();
        }

        private void BtnUpdateData_Click(object sender, RoutedEventArgs e)
        {
            string email;
            string passwordValidation;
            email = txtEmail.Text;
            passwordValidation = txtPasswordValidation.Password;
            string hashedPassword;

            if (ValidationFields() && client.ExistEmail(email))
            {

                BtnUpdateData.IsEnabled = false;
                VE_VerificationEmail changePassword = new VE_VerificationEmail();

                if (changePassword.MailSentByThePlayer(email))
                {
                    changePassword.ShowDialog();
                }

                Encryption encryption = new Encryption();
                hashedPassword = encryption.HashPassword256(passwordValidation);

                if (SingletonPlayer.PlayerClient.Verificated)
                {
                    try
                    {
                        bool status = false;
                        status = client.ChangePassword(email, hashedPassword);
                        ResponseChangePassword(status);
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                        GoLogin();
                    }
                }

            }
            else
            {
                MessageBox.Show(Properties.Langs.Lang.thisEmailDoesNotExist, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public bool ValidationFields()
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Password) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPasswordValidation.Password))
            {
                MessageBox.Show(Properties.Langs.Lang.rectifyTheFields, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            FieldValidation fieldValidation = new FieldValidation();
            if (!fieldValidation.PasswordValidation(txtPassword.Password, txtPasswordValidation.Password))
            {
                MessageBox.Show(Properties.Langs.Lang.passwordsDoNotMatch, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!fieldValidation.ValidationEmailFormat(txtEmail.Text))
            {
                MessageBox.Show(Properties.Langs.Lang.invalidMailFormat, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        public void ResponseChangePassword(bool status)
        {
            if (status)
            {
                MessageBox.Show(Properties.Langs.Lang.youHaveSuccessfullyUpdated, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                try
                {
                    client.Close();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    GoLogin();
                }
                Close();
            }
            else
            {
                MessageBox.Show(Properties.Langs.Lang.couldNotUpdateSuccessfully, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
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

