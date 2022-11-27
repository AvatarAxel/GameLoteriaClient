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
            Login login = new Login();
            login.Show();
            try
            {
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private void BtnUpdateData_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationFields())
            {
                string email;
                string passwordValidation;
                email = txtEmail.Text;
                passwordValidation = txtPasswordValidation.Password;
                string hashedPassword;
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
                        MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }

        }

        public bool ValidationFields()
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Password) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPasswordValidation.Password))
            {
                MessageBox.Show("Rectify the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            FieldValidation fieldValidation = new FieldValidation();
            if (!fieldValidation.PasswordValidation(txtPassword.Password, txtPasswordValidation.Password))
            {
                MessageBox.Show("Passwords do not match", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!fieldValidation.ValidationEmailFormat(txtEmail.Text))
            {
                MessageBox.Show("Invalid mail format", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        public void ResponseChangePassword(bool status)
        {
            if (status)
            {
                MessageBox.Show("You have successfully updated", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                Login login = new Login();
                login.Show();
                try
                {
                    client.Close();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Close();
            }
            else
            {
                MessageBox.Show("Could not update successfully", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
