using Logic;
using System;
using System.ServiceModel;
using System.Windows;

namespace View
{
    public partial class RegisterUser : Window
    {
        private ServiceReference.PlayerDTO playerDTO;
        private ServiceReference.UserRegistrationServiceClient client;
        public RegisterUser()
        {
            InitializeComponent();
            client = new ServiceReference.UserRegistrationServiceClient();
            playerDTO = new ServiceReference.PlayerDTO();
            SingletonPlayer.PlayerClient.Verificated = false;
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
                string usernameUser = txtUsername.Text;
                try
                {
                    if (client.ValidationEmailDataBase(emailUser))
                    {
                        MessageBox.Show("Mail already registered", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    if (client.ValidationUsernameDataBase(usernameUser))
                    {
                        MessageBox.Show("Nickname already registered", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    RegistreUser(emailUser);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }    
        }

        public void ResponseRegister(bool status)
        {
            if (status)
            {
                MessageBox.Show("Registration successful", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegistreUser(string emailUser)
        {
            VE_VerificationEmail goToPopUpWindow = new VE_VerificationEmail();

            if (goToPopUpWindow.MailSentByThePlayer(emailUser))
            {
                goToPopUpWindow.ShowDialog();

            }       

            if (SingletonPlayer.PlayerClient.Verificated)
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

                    bool status = false;
                    status = client.RegistrerUserDataBase(playerDTO);
                    ResponseRegister(status);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public bool ValidationFields() 
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtUsername.Text)
                || string.IsNullOrEmpty(txtPassword.Password) || string.IsNullOrEmpty(txtPasswordValidation.Password))
            {
                MessageBox.Show("Rectify the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            FieldValidation fieldValidation = new FieldValidation();
            if (!fieldValidation.PasswordValidation(txtPassword.Password, txtPasswordValidation.Password))
            {
                MessageBox.Show("Passwords do not match", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!fieldValidation.ValidationEmailFormat(txtEmail.Text))
            {
                MessageBox.Show("Invalid mail format", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!fieldValidation.ValidationUsernameFormat(txtUsername.Text))
            {
                MessageBox.Show("Invalid nickname", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            String Birthday = calendarBirthday.SelectedDate.ToString();
            if (string.IsNullOrEmpty(Birthday))
            {
                MessageBox.Show("Incomplete date of birth", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!fieldValidation.PasswordSecure(txtPassword.Password))
            {
                MessageBox.Show("Deficient password", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
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
