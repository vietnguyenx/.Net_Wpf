using BusinessObject;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
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
    /// Interaction logic for CustomerPopUp.xaml
    /// </summary>
    public partial class CustomerPopUp : Window
    {
        private CustomerObject customerObject;
        public Action<object, RoutedEventArgs> WindowClosed;
        private readonly Customer existCustomer;

        public CustomerPopUp() => InitProp();

        public CustomerPopUp(Customer c)
        {
            InitProp();
            this.existCustomer = c;
        }

        public void InitProp()
        {
            InitializeComponent();
            customerObject = new CustomerObject();
            Loaded += LoadedUpdateForm;
        }

        private void LoadedUpdateForm(object sender, RoutedEventArgs e)
        {

            if (existCustomer != null)
            {
                tbCustomerFullName.Text = existCustomer.CustomerFullName;
                tbTelephone.Text = existCustomer.Telephone;
                tbEmailAddress.Text = existCustomer.EmailAddress;
                dpCustomerBirthday.SelectedDate = existCustomer.CustomerBirthday.Value.ToDateTime(new TimeOnly(0, 0));
                tbCustomerStatus.Text = existCustomer.CustomerStatus.ToString();
                tbPassword.Text = existCustomer.Password;

            }
        }

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!TakeCustomer(out Customer? c))
            {
                return;
            }

            if (existCustomer != null)
            {
                c!.CustomerId = existCustomer.CustomerId;
                customerObject.UpdateCustomer(c);
            }
            else
            {
                customerObject.AddCustomer(c!);
            }

            WindowClosed?.Invoke(sender, e);
            Close();
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

        private void CancelBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel?", "Customer",
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }

        }
    }
}
