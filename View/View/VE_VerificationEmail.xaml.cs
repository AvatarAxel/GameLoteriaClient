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

    public partial class VE_VerificationEmail : Window, ServiceReference.IEmailServiceCallback
    {
        private string codeVerificationComparation;
        private InstanceContext context;
        private ServiceReference.EmailServiceClient client;


        public VE_VerificationEmail()
        {
            InitializeComponent();
            context = new InstanceContext(this);
            client = new ServiceReference.EmailServiceClient(context);
        }

        public void MailSentByThePlayer(string emailPlayer)
        {
           
            try
            {
                client.ValidationEmail(emailPlayer);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            SingletonPlayer.PlayerClient.RegisteredUser = false;
            Close();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVerification.Text))
            {
                MessageBox.Show("Pon algo en el campo please uwu", "Asi no >.<", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(!codeVerificationComparation.Equals(null))
            {

                string verificarionUser = txtVerification.Text;

                if (codeVerificationComparation.Equals(verificarionUser))
                {
                    MessageBox.Show("Todo chido", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                    try
                    {
                        client.Close();
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    SingletonPlayer.PlayerClient.RegisteredUser = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("El codigo de verificación no coincide", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }

        }

        public void ResponseEmail(string verificationCode)
        {
            codeVerificationComparation = verificationCode;
        }
    }
}
