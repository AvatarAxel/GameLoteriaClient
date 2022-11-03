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

namespace View
{

    public partial class VE_VerificationEmail : Window, ServiceReference.IAuthenticationServiceCallback
    {
        private string codeVerificationComparation;

        public VE_VerificationEmail()
        {
            InitializeComponent();
        }

        public void MailSentByThePlayer(string emailPlayer)
        {
            InstanceContext context = new InstanceContext(this);
            ServiceReference.AuthenticationServiceClient client = new ServiceReference.AuthenticationServiceClient(context);
            client.ValidationEmail(emailPlayer);
        }

        public void ResponseAuthenticated(bool status)
        {
            throw new NotImplementedException();
        }

        public void ResponseEmail(string verificationCode)
        {
            codeVerificationComparation = verificationCode;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtVerification.Text))
            {
                MessageBox.Show("No seas imbecil pon algo en el campo", "Baboso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                string verificarionUser = txtVerification.Text;

                if (codeVerificationComparation.Equals(verificarionUser))
                {
                    MessageBox.Show("Todo chido", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
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
