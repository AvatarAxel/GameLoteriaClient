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

        public void ReponseAuthenticated(bool result)
        {
            if(result == true)
            {

            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            ServiceReference.AuthenticationServiceClient client = new ServiceReference.AuthenticationServiceClient(context);
            string username;
            username = txtUsername.Text;
            string email;
            email = txtEmail.Text;
            string password01;
            password01 = txtPassword1.Password;
            string password02;
            password02 = txtPassword2.Password;
            client.ReponseAuthenticated(username, email, password01);
        }
    }
}
