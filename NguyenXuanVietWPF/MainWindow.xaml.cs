using BusinessObject;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NguyenXuanVietWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CustomerObject customerObject;
        public MainWindow()
        {
            InitializeComponent();
            customerObject = new CustomerObject();
            Loaded += CheckRole;
        }

        private void CheckRole(object sender, RoutedEventArgs e)
        {
            if (CustomerObject.AccountLogin != null)
            {
                if (CustomerObject.AccountLogin.Role.Equals("customer"))
                {
                    btnCustomer.Content = "Profile Management";

                    btnRoom.IsEnabled = false;
                    btnBooking.IsEnabled = false;
                }
            }
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerObject.AccountLogin != null)
            {
                if (CustomerObject.AccountLogin.Role.Equals("customer"))
                {
                    WindowProfile windowProfile = new WindowProfile(CustomerObject.AccountLogin.Id ?? 0);
                    windowProfile.Show();
                }
                else
                {
                    WindowCustomer window = new WindowCustomer();
                    window.Show();
                }
                Close();
            }
        }


        private void btnRoom_Click(object sender, RoutedEventArgs e)
        {
            WindowRoom window = new WindowRoom();
            window.Show();
            Close();
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            WindowBooking window = new WindowBooking();
            window.Show();
            Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            WindowLogin window = new WindowLogin();
            window.Show();
            Close();
        }
    }
}