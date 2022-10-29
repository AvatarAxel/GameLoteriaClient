using Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.ApplicationServices;
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
    public partial class RegisterUser : Window, ServiceReference.IAuthenticationServiceCallback
    {
        public RegisterUser()
        {
            InitializeComponent();
        }

        public void ResponseAuthenticated(bool status)
        {
            if (status)
            {
                MessageBox.Show("Registro exitoso", "Bienvenido(a)", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Por el momento no hay conexión con el servidor de la base de datos, inténtelo de nuevo más tarde", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidationFields()) {
                return;
            }
            ServiceReference.PlayerDTO playerDTO = new ServiceReference.PlayerDTO();
            InstanceContext context = new InstanceContext(this);
            ServiceReference.AuthenticationServiceClient client = new ServiceReference.AuthenticationServiceClient(context);
            Encryption encryption = new Encryption();
            playerDTO.username = txtUsername.Text;
            playerDTO.Email = txtEmail.Text;
            string hashedPassword = encryption.HashPassword256(txtPassword.Password);
            playerDTO.Password = hashedPassword;
            String Birthday = calendarBirthday.SelectedDate.ToString();
            DateTime dateTime = DateTime.Parse(Birthday);
            playerDTO.Birthday = dateTime;
            try
            {
                client.RegistrerUserBD(playerDTO);
            }
            catch(EndpointNotFoundException) 
            {
                MessageBox.Show("Sin conexión, inténtelo más tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        public bool ValidationFields() 
        { 
            bool validation = false;
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtUsername.Text)
                || string.IsNullOrEmpty(txtPassword.Password) || string.IsNullOrEmpty(txtPasswordValidation.Password))
            {
                MessageBox.Show("Campos invalidos", "Campos vacios", MessageBoxButton.OK, MessageBoxImage.Information);
                return validation;
            }
            FieldValidation fieldValidation = new FieldValidation();
            if (!fieldValidation.passwordValidation(txtPassword.Password, txtPasswordValidation.Password))
            {
                MessageBox.Show("Las contraseñas no coinciden", "Las contraseñas no coinciden", MessageBoxButton.OK, MessageBoxImage.Information);
                return validation;
            }
            if (!fieldValidation.ValidationEmailFormat(txtEmail.Text))
            {
                MessageBox.Show("Formato de correo invalido", "formato de correo invalido", MessageBoxButton.OK, MessageBoxImage.Information);
                return validation;
            }
            String Birthday = calendarBirthday.SelectedDate.ToString();
            if (string.IsNullOrEmpty(Birthday))
            {
                MessageBox.Show("Por favor elija su fecha de nacimiento", "formato invalido", MessageBoxButton.OK, MessageBoxImage.Information);
                return validation;
            }
            validation = true;
            return validation;
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void RegistrerUser(bool status)
        {
            throw new NotImplementedException();
        }

    }
}
