using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

        public void ReceiveUserName(string playerUsername) 
        {
            username = playerUsername;
            InstanceContext context = new InstanceContext(this);
            ServiceReference.ChatServiceClient client = new ServiceReference.ChatServiceClient(context);
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
            string message = txtMessage.Text;
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    client.SendMessage(message, username);
                    txtMessage.Clear();
                }
                catch (EndpointNotFoundException) 
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ReciveMessage(string player, string message)
        {
            txtChat.Text += player + ": " + message+ "\r\n";
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            ServiceReference.ChatServiceClient client = new ServiceReference.ChatServiceClient(context);
            try 
            {
                client.ExitChat(username);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Application.Current.Shutdown();
        }
    }
}
