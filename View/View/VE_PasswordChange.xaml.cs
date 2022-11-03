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
    public partial class VE_PasswordChange : Window
    {
        public VE_PasswordChange()
        {
            InitializeComponent();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void BtnUpdateDat_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Password) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPasswordValidation.Password))
            {
                MessageBox.Show("Campos vacios, intentelo de nuevo", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string email;
                string passwordValidation;
                email = txtEmail.Text;
                passwordValidation = txtPasswordValidation.Password;
                string hashedPassword;

                VE_VerificationEmail changePassword = new VE_VerificationEmail();
                changePassword.ShowDialog();
                


                Encryption encryption = new Encryption();
                hashedPassword = encryption.HashPassword256(txtPassword.Password);
            }

        }
    }
}
