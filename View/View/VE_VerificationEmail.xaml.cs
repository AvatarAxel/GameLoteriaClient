using Logic;
using System;
using System.ServiceModel;
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

    public partial class VE_VerificationEmail : Window
    {
        private string codeVerificationComparation;
        private ServiceReference.EmailServiceClient client;

        public VE_VerificationEmail()
        {
            InitializeComponent();            
            client = new ServiceReference.EmailServiceClient();
        }

        public bool MailSentByThePlayer(string emailPlayer)
        {
            CodeGame code = new CodeGame();
            codeVerificationComparation = code.GenerateRandomCode();

            try
            {
                bool status =  client.ValidationEmail(emailPlayer, codeVerificationComparation);
                if (status)
                {
                    MessageBox.Show(Properties.Langs.Lang.checkYourEmail, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show(Properties.Langs.Lang.couldNotSendEmail, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            catch (TimeoutException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }
            catch (CommunicationException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                GoLogin();
            }

            return false;
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
            SingletonPlayer.PlayerClient.Verificated = false;
            Close();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVerification.Text))
            {
                MessageBox.Show(Properties.Langs.Lang.invalidFormat, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string verificarionUser = txtVerification.Text;

                if (codeVerificationComparation.Equals(verificarionUser))
                {
                    MessageBox.Show(Properties.Langs.Lang.correctCode, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                    try
                    {
                        client.Close();
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                        GoLogin();
                    }

                    SingletonPlayer.PlayerClient.Verificated = true;
                    Close();
                }
                else
                {
                    MessageBox.Show(Properties.Langs.Lang.verificationCodeDoesNotMatch,Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
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
