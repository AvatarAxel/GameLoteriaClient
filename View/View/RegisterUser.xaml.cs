using Logic;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for RegisterUser.xaml
    /// </summary>
    public partial class RegisterUser : Window, ServiceReference.IAuthenticationServiceCallback
    {
        public RegisterUser()
        {
            InitializeComponent();
        }

        public void NombreEjemplo(bool result)
        {
            if(result)
            {
                MessageBox.Show("Siii", "Siiiuuu", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Nooo", "Noooooo", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        public void RegistrerUser(bool status)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtUsername.Text)
                || string.IsNullOrEmpty(txtPassword1.Password) || string.IsNullOrEmpty(txtPassword2.Password))
            {
                MessageBox.Show("Campos invalidos", "Campos vacios", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            FieldValidation fieldValidation = new FieldValidation();
            if (!fieldValidation.passwordValidation(txtPassword1.Password, txtPassword2.Password))
            {
                MessageBox.Show("Las contraseñas no coinciden", "Las contraseñas no coinciden", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!fieldValidation.ValidationEmailFormat(txtEmail.Text)) 
            {
                MessageBox.Show("Formato de correo invalido", "formato de correo invalido", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            String Birthday = calendarBirthday.SelectedDate.ToString();
            if (string.IsNullOrEmpty(Birthday)) 
            {
                MessageBox.Show("Por favor elija su fecha de nacimiento", "formato invalido", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ServiceReference.PlayerDTO playerDTO = new ServiceReference.PlayerDTO();
            InstanceContext context = new InstanceContext(this);
            ServiceReference.AuthenticationServiceClient client = new ServiceReference.AuthenticationServiceClient(context);
            playerDTO.username = txtUsername.Text;
            playerDTO.Email = txtEmail.Text;
            playerDTO.Password = txtPassword1.Password;            
            DateTime dateTime = DateTime.Parse(Birthday);
            playerDTO.Birthday = dateTime;
            try
            {
                client.RegistrerUserBD(playerDTO);
            }
            catch (Exception ex) {
                MessageBox.Show("Por favor elija su fecha de nacimiento", "formato invalido", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
