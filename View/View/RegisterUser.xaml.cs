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
        private Login login = new Login();
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
            login.Show();
            try
            {
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
            }
            catch (CommunicationException)
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
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
                        MessageBox.Show(Properties.Langs.Lang.mailAlreadyRegistered, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    if (client.ValidationUsernameDataBase(usernameUser))
                    {
                        MessageBox.Show(Properties.Langs.Lang.nicknameAlreadyRegistered, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    RegistreUser(emailUser);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    login.Show();
                    Close();
                }
                catch (CommunicationException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    login.Show();
                    Close();
                }
            }    
        }

        public void ResponseRegister(bool status)
        {
            if (status)
            {
                MessageBox.Show(Properties.Langs.Lang.registrationSuccessful, Properties.Langs.Lang.welcomeTo, MessageBoxButton.OK, MessageBoxImage.Information);
                FillSingleton();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                try
                {
                    client.Close();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    login.Show();
                    Close();
                }
                Close();
            }
            else
            {
                MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                login.Show();
                Close();
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
                    MessageBox.Show(Properties.Langs.Lang.offlinePleaseTryAgainLater, Properties.Langs.Lang.error, MessageBoxButton.OK, MessageBoxImage.Error);
                    login.Show();
                    Close();
                }
            }
        }

        public bool ValidationFields() 
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtUsername.Text)
                || string.IsNullOrEmpty(txtPassword.Password) || string.IsNullOrEmpty(txtPasswordValidation.Password))
            {
                MessageBox.Show(Properties.Langs.Lang.rectifyTheFields, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            FieldValidation fieldValidation = new FieldValidation();
            if (!fieldValidation.PasswordValidation(txtPassword.Password, txtPasswordValidation.Password))
            {
                MessageBox.Show(Properties.Langs.Lang.passwordsDoNotMatch, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!fieldValidation.ValidationEmailFormat(txtEmail.Text))
            {
                MessageBox.Show(Properties.Langs.Lang.invalidMailFormat, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!fieldValidation.ValidationUsernameFormat(txtUsername.Text))
            {
                MessageBox.Show(Properties.Langs.Lang.invalidNickname, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            String Birthday = calendarBirthday.SelectedDate.ToString();
            if (string.IsNullOrEmpty(Birthday))
            {
                MessageBox.Show(Properties.Langs.Lang.incompleteDateOfBirth, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!fieldValidation.PasswordSecure(txtPassword.Password))
            {
                MessageBox.Show(Properties.Langs.Lang.deficientPassword, Properties.Langs.Lang.warning, MessageBoxButton.OK, MessageBoxImage.Information);
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
