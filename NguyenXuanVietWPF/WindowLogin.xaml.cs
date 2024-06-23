using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace NguyenXuanVietWPF
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private readonly CustomerObject customerObject;

        public WindowLogin()
        {
            InitializeComponent();
            customerObject = new CustomerObject();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(pbPassword.Password))
            {
                MessageBox.Show("Please fill in email and password", "Login");
                return;
            }

            bool isAuthorized = customerObject.Login(txtEmail.Text, pbPassword.Password);
            if (isAuthorized)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed. Email or password incorrect", "Login");
            }
        }
    }
}
