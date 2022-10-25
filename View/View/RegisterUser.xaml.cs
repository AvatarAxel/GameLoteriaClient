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

            //this.Birtheday.Text = calendarBirthday.Opacity.ToString();
            //this.Birtheday.Text = calendarBirthday.SelectedDates.ToString();
            this.Birtheday.Text = calendarBirthday.SelectedDate.ToString();

            ServiceReference.PlayerDTO playerDTO = new ServiceReference.PlayerDTO();
            InstanceContext context = new InstanceContext(this);
            ServiceReference.AuthenticationServiceClient client = new ServiceReference.AuthenticationServiceClient(context);
            playerDTO.username = txtUsername.Text;
            playerDTO.Email = txtEmail.Text;
            playerDTO.Password = txtPassword1.Password;
            String Birthday = calendarBirthday.SelectedDate.ToString();
            this.Birtheday.Text = Birthday;
            DateTime dateTime = DateTime.Parse(Birthday);
            playerDTO.Birthday = dateTime;
            //playerDTO.Birthday = calendarBirthday.GetValue.ToString();
            client.RegistrerUserBD(playerDTO);
        }
    }
}
