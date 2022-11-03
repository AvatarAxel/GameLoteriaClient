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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic;


namespace View
{
    public partial class Login : Window, ServiceReference.IAuthenticationServiceCallback
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Password) || string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Campos vacios, intentelo de nuevo", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
            {
                InstanceContext context = new InstanceContext(this);
                ServiceReference.AuthenticationServiceClient client = new ServiceReference.AuthenticationServiceClient(context);
                string username;
                string hashedPassword;
                username = txtUser.Text;
                Encryption encryption = new Encryption();
                hashedPassword = encryption.HashPassword256(txtPassword.Password);
                client.AuthenticationLogin(username, hashedPassword);
            }    
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            
            RegisterUser registerUser = new RegisterUser();
            registerUser.Show();
            Close();

        }

        public void ResponseAuthenticated(bool status)
        {
            if (status)
            {
                /*MainWindow mainWindow = new MainWindow();
                Close();
                mainWindow.Show();*/
                Chat chat = new Chat();
                chat.ReceiveData(txtUser.Text);
                Close();
                chat.Show();
            }
            else
            {
                MessageBox.Show("El username y/o password que ingreso no se encuentra(n) registrados, verifique que sean los datos correctos o regístrese", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLanguage.SelectedIndex == 0)
                Properties.Settings.Default.languageCode = "en";
            else 
                Properties.Settings.Default.languageCode = "es";
           
            Properties.Settings.Default.Save();
        }

        public void ResponseEmail(string verificationCode)
        {
            throw new NotImplementedException();
        }

        private void LbReset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VE_PasswordChange passwordChange = new VE_PasswordChange();
            passwordChange.Show();
            Close();
        }

        private void LbPlayingAsGuest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close(); 
            mainWindow.Show();
        }
    }
}
