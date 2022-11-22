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
using Logic;

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
                    MessageBox.Show("Revisa tu correo electornico", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }
                else
                {
                    MessageBox.Show("No pude enviarte el correo :/ ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SingletonPlayer.PlayerClient.Verificated = false;
            Close();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVerification.Text))
            {
                MessageBox.Show("Formato invalido", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
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

                    SingletonPlayer.PlayerClient.Verificated = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("El codigo de verificación no coincide", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
