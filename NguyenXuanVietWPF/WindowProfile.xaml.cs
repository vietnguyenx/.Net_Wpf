using BusinessObject;
using DataAccess.Models;
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
    /// Interaction logic for WindowProfile.xaml
    /// </summary>
    public partial class WindowProfile : Window
    {
        private readonly CustomerObject customerObject;
        private readonly BookingObject bookingObject;
        private readonly int CustomerId;

        public WindowProfile(int CustomerId)
        {
            customerObject = new CustomerObject();
            bookingObject = new BookingObject();
            this.CustomerId = CustomerId;
            Loaded += LoadDataCustomer;
            InitializeComponent();
        }

        private void LoadDataCustomer(object sender, RoutedEventArgs e)
        {
            Customer customer = customerObject.GetCustomer(CustomerId);

            tbCustomerFullName.Text = customer.CustomerFullName;
            tbTelephone.Text = customer.Telephone;
            tbEmailAddress.Text = customer.EmailAddress;
            dpCustomerBirthday.SelectedDate = customer.CustomerBirthday.Value.ToDateTime(new TimeOnly(0, 0));
            tbCustomerStatus.Text = customer.CustomerStatus.ToString();
            tbPassword.Text = customer.Password;

            lvBookingHistory.ItemsSource = bookingObject.GetBookingsOfCustomer(CustomerId);
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void UpdateBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!TakeCustomer(out Customer? c))
            {
                return;
            }
            c!.CustomerId = CustomerId;
            customerObject.UpdateCustomer(c);
        }

        private bool TakeCustomer(out Customer? validCustomer)
        {
            StringBuilder errorMessage = new StringBuilder();
            if (string.IsNullOrEmpty(tbCustomerFullName.Text))
            {
                errorMessage.Append("Customer full name is required.\n");
            }

            if (string.IsNullOrEmpty(tbTelephone.Text))
            {
                errorMessage.Append("Telephone is required.\n");
            }

            if (string.IsNullOrEmpty(tbEmailAddress.Text))
            {
                errorMessage.Append("Email address is required.\n");
            }

            if (!dpCustomerBirthday.SelectedDate.HasValue)
            {
                errorMessage.Append("Customer birthday is required.\n");
            }

            if (!byte.TryParse(tbCustomerStatus.Text, out byte customerStatus))
            {
                errorMessage.Append("Customer status must be a byte value.\n");
            }

            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                errorMessage.Append("Password is required.\n");
            }


            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage.ToString(), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                validCustomer = null;
                return false;
            }

            DateTime customerDateTime = dpCustomerBirthday.SelectedDate.Value;
            DateOnly customerBirthday = DateOnly.FromDateTime(customerDateTime);

            validCustomer = new Customer
            {
                CustomerFullName = tbCustomerFullName.Text,
                Telephone = tbTelephone.Text,
                EmailAddress = tbEmailAddress.Text,
                CustomerBirthday = customerBirthday,
                CustomerStatus = customerStatus,
                Password = tbPassword.Text,
            };
            return true;
        }
    }
}
