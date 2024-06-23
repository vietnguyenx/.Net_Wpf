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
    /// Interaction logic for WindowBooking.xaml
    /// </summary>
    public partial class WindowBooking : Window
    {
        private readonly BookingObject bookingObject;

        public WindowBooking()
        {
            bookingObject = new BookingObject();
            InitializeComponent();
            Loaded += BookingsLoad;
        }

        private void BookingsLoad(object sender, RoutedEventArgs e)
        {
            LoadedBookings();
        }

        private void LoadedBookings()
        {
            dgBooking.ItemsSource = null;
            dgBooking.ItemsSource = bookingObject.GetBooking();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            BookingPopUp popUp = new BookingPopUp();
            popUp.WindowClosed += BookingsLoad;
            popUp.Show();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void DeleteBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (dgBooking.SelectedItem is BookingReservation selectedBooking)
            {
                if (MessageBox.Show($"Are you sure you want to delete booking reservation {selectedBooking.BookingDate}?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    bookingObject.DeleteBooking(selectedBooking.BookingReservationId);
                    LoadedBookings();
                }
            }
            else
            {
                MessageBox.Show("Please select a booking reservation to delete.");
            }
        }

        private void UpdateBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (dgBooking.SelectedItem is BookingReservation selectedBooking)
            {
                BookingPopUp popUp = new BookingPopUp(selectedBooking);
                popUp.WindowClosed += BookingsLoad;
                popUp.Show();
            }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            if(dpStartDate.SelectedDate == null)
            {
                ShowWarningMessageBox("Please input Start Date!");
                return;
            }
            if (dpEndDate.SelectedDate == null)
            {
                ShowWarningMessageBox("Please input End Date!");
                return;
            }
            if(dpStartDate.SelectedDate > dpEndDate.SelectedDate)
            {
                ShowWarningMessageBox("End date must greater than start date");
                return;
            }

            DateOnly startDate = DateOnly.FromDateTime(dpStartDate.SelectedDate.Value);
            DateOnly endDate = DateOnly.FromDateTime(dpEndDate.SelectedDate.Value);

            List<BookingReservation> bookings = bookingObject.GetBookingsByDate(startDate, endDate);
            dgBooking.ItemsSource = null;
            dgBooking.ItemsSource = bookings;
        }

        private void ShowWarningMessageBox(string warningMessage)
        {
            MessageBox.Show(warningMessage, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnLoadAll_Click(object sender, RoutedEventArgs e)
        { 
            LoadedBookings();
        }
    }
}
