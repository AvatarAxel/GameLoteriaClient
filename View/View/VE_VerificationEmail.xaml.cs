﻿using Logic;
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
        private Login login = new Login();

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
                    MessageBox.Show("Check your email", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("Could not send email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
            }
            catch (CommunicationException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
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
                login.Show();
                Close();
            }
            SingletonPlayer.PlayerClient.Verificated = false;
            Close();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVerification.Text))
            {
                MessageBox.Show("Invalid format", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string verificarionUser = txtVerification.Text;

                if (codeVerificationComparation.Equals(verificarionUser))
                {
                    MessageBox.Show("Correct code", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    try
                    {
                        client.Close();
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        login.Show();
                        Close();
                    }

                    SingletonPlayer.PlayerClient.Verificated = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Verification code does not match", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
