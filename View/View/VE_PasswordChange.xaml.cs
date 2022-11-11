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
    public partial class VE_PasswordChange : Window, ServiceReference.IChangePasswordServiceCallback
    {
        private InstanceContext context;
        public VE_PasswordChange()
        {
            InitializeComponent();
            context = new InstanceContext(this);
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void BtnUpdateDat_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationFields())
            {
                string email;
                string passwordValidation;
                email = txtEmail.Text;
                passwordValidation = txtPasswordValidation.Password;
                string hashedPassword;

                VE_VerificationEmail changePassword = new VE_VerificationEmail();
                changePassword.MailSentByThePlayer(email);
                changePassword.ShowDialog();          

                ServiceReference.ChangePasswordServiceClient client = new ServiceReference.ChangePasswordServiceClient(context);//new ServiceReference.AuthenticationServiceClient(context);
                Encryption encryption = new Encryption();
                hashedPassword = encryption.HashPassword256(txtPassword.Password);

                try
                {
                    client.ChangePassword(email, hashedPassword);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }

        public bool ValidationFields()
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Password) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPasswordValidation.Password))
            {
                MessageBox.Show("Campos invalidos", "Campos vacios", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            FieldValidation fieldValidation = new FieldValidation();
            if (!fieldValidation.PasswordValidation(txtPassword.Password, txtPasswordValidation.Password))
            {
                MessageBox.Show("Las contraseñas no coinciden", "Las contraseñas no coinciden", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!fieldValidation.ValidationEmailFormat(txtEmail.Text))
            {
                MessageBox.Show("Formato de correo invalido", "formato de correo invalido", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
  
            return true;
        }

        public void ResponseChangePassword(bool status)
        {
            if (status == true)
            {
                MessageBox.Show("Correcto", "Se ha actualizado con exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Login login = new Login();
                login.Show();
                Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar correctamente", "Upsi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
