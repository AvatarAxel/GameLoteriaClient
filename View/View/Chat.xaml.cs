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

namespace View
{
    public partial class Chat : Window, ServiceReference.IChatServiceCallback
    {
        public Chat()
        {
            InitializeComponent();
            txtChat.IsEnabled = false;
        }
        private string username;

        public void ReceiveData(string playerUsername) 
        {
            InstanceContext context = new InstanceContext(this);
            ServiceReference.ChatServiceClient client = new ServiceReference.ChatServiceClient(context);
            username = playerUsername;            
            try
            {
                client.JoinChat(username);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            ServiceReference.ChatServiceClient client = new ServiceReference.ChatServiceClient(context);
            try
            {
                client.SendMessage(txtMessage.Text, username);
                txtMessage.Clear();
            }
            catch(EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ReciveMessage(string player, string message)
        {
            txtChat.Text += player + ":  " + message + "\r\n";
        }
    }
}
