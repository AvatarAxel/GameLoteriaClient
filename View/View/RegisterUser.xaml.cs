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
using View.ServiceReference;

namespace View
{
    public partial class RegisterUser : Window, ServiceReference.IUserRegistrationServiceCallback
    {
        private InstanceContext context;
        private ServiceReference.PlayerDTO playerDTO;
        private ServiceReference.UserRegistrationServiceClient client;
        public RegisterUser()
        {
            InitializeComponent();
            context = new InstanceContext(this);
            client = new ServiceReference.UserRegistrationServiceClient(context);
            playerDTO = new ServiceReference.PlayerDTO();
            SingletonPlayer.PlayerClient.Verificated = false;
        }

        public void ResponseRegister(bool status)
        {
            if (status)
            {
                MessageBox.Show("Registro exitoso", "Bienvenido(a)", MessageBoxButton.OK, MessageBoxImage.Information);
                FillSingleton();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
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
                MessageBox.Show("Por el momento no hay conexión con el servidor de la base de datos, inténtelo de nuevo más tarde", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
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

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationFields()) {

                string emailUser = txtEmail.Text;
                VE_VerificationEmail goToPopUpWindow = new VE_VerificationEmail();
                goToPopUpWindow.MailSentByThePlayer(emailUser);
                goToPopUpWindow.ShowDialog();                
                if(SingletonPlayer.PlayerClient.Verificated) 
                {
                    BtnRegister.IsEnabled = false;
                    Encryption encryption = new Encryption();

                    playerDTO.Username = txtUsername.Text;
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
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }               
            }    
        }

        public bool ValidationFields() 
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtUsername.Text)
                || string.IsNullOrEmpty(txtPassword.Password) || string.IsNullOrEmpty(txtPasswordValidation.Password))
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
            if (!fieldValidation.ValidationUsernameFormat(txtUsername.Text))
            {
                MessageBox.Show("Formato de username invalido", "formato de username invalido", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            String Birthday = calendarBirthday.SelectedDate.ToString();
            if (string.IsNullOrEmpty(Birthday))
            {
                MessageBox.Show("Por favor elija su fecha de nacimiento", "formato invalido", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private void FillSingleton() 
        {
            SingletonPlayer.PlayerClient.Username = txtUsername.Text;
            SingletonPlayer.PlayerClient.Email = txtEmail.Text;
            SingletonPlayer.PlayerClient.RegisteredUser = true;
            SingletonPlayer.PlayerClient.Coin = 500;            
        }
    }
}
