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
    /// Interaction logic for WindowCustomer.xaml
    /// </summary>
    public partial class WindowCustomer : Window
    {

        private readonly CustomerObject customerObject;

        public WindowCustomer()
        {
            InitializeComponent();
            customerObject = new CustomerObject();
            Loaded += CustomersLoad;
        }

        private void CustomersLoad(object sender, RoutedEventArgs e)
        {
            LoadCustomer();
        }

        private void LoadCustomer()
        {
            dgCustomer.ItemsSource = null;
            dgCustomer.ItemsSource = customerObject.GetCustomers();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerPopUp cusPopUp = new CustomerPopUp();
            cusPopUp.WindowClosed += CustomersLoad;
            cusPopUp.Show();
        }

        private void DeleteBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (dgCustomer.SelectedItem is Customer selectedCustomer)
            {
                if (MessageBox.Show($"Are you sure you want to delete customer {selectedCustomer.CustomerFullName}?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    customerObject.DeleteCustomer(selectedCustomer.CustomerId);
                    LoadCustomer();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }

        private void UpdateBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (dgCustomer.SelectedItem is Customer selectedCustomer)
            {
                CustomerPopUp customerPopUp = new CustomerPopUp(selectedCustomer);
                customerPopUp.WindowClosed += CustomersLoad;
                customerPopUp.Show();
            }
            else
            {
                MessageBox.Show("Please select a customer to update.");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text;
            if (string.IsNullOrEmpty(search))
            {
                MessageBox.Show("Please input search criteria.");
                return;
            }

            var searchResults = customerObject.SearchCustomers(search);

            dgCustomer.ItemsSource = null;
            dgCustomer.ItemsSource = searchResults;
        }



        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void btnLoadAll_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomer();
        }
    }
}
