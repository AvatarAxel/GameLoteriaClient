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
using Logic;


namespace View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, ServiceReference.IAuthenticationServiceCallback
    {
        public Login()
        {
            InitializeComponent();
        }
        private void btnMinimize_Click(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void ReponseAuthenticated(bool result)
        {
            this.ResultadoTextBox.Text = result.ToString();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            ServiceReference.AuthenticationServiceClient client = new ServiceReference.AuthenticationServiceClient(context);
            string username;
            username = txtUser.Text;
            string password;
            password = txtPassword.Password;
            client.IsAuthenticated(username, password);
            //MessageBox.Show("Mensaje", "Titulo",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
            
        }
    }
}
