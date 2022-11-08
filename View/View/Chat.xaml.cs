using Logic;
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
        private InstanceContext context;
        public Chat()
        {
            InitializeComponent();
            context = new InstanceContext(this);
            InitializeChat();
            txtChat.IsEnabled = false;
        }
        


        public void InitializeChat() 
        {
            ServiceReference.ChatServiceClient client = new ServiceReference.ChatServiceClient(context);
            try
            {
                client.JoinChat(SingletonPlayer.PlayerClient.Username);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally 
            {
                client.Close();
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference.ChatServiceClient client = new ServiceReference.ChatServiceClient(context);
            string message = txtMessage.Text;
            if (!string.IsNullOrEmpty(message)) 
            {
                try
                {
                    client.SendMessage(message, SingletonPlayer.PlayerClient.Username);
                    txtMessage.Clear();
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Offline, please try again later", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    client.Close();
                }
            }
        }

        public void ReciveMessage(string player, string message)
        {
            txtChat.Text += player + ":  " + message + "\r\n";
        }
    }
}
