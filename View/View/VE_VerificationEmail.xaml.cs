﻿using System;
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
        InstanceContext context;
        private string codeVerificationComparation;
        private ServiceReference.EmailServiceClient client;

        public VE_VerificationEmail()
        {
            InitializeComponent();
            context = new InstanceContext(this);
            client = new ServiceReference.EmailServiceClient(context);
        }

        public void MailSentByThePlayer(string emailPlayer)
        {
            ServiceReference.EmailServiceClient client = new ServiceReference.EmailServiceClient(context);
            try
            {
                client.ValidationEmail(emailPlayer);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ResponseEmail(string verificationCode)
        {
            codeVerificationComparation = verificationCode;
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

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVerification.Text))
            {
                MessageBox.Show("Pon algo en el campo please uwu", "Asi no >.<", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    Close();
                }
                else
                {
                    MessageBox.Show("El username y/o password que ingreso no se encuentra(n) registrados, verifique que sean los datos correctos o regístrese", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            }

        }
    }
}
