using BusinessObject;
using DataAccess.Models;
using System;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Windows;

namespace NguyenXuanVietWPF
{
    public partial class BookingPopUp : Window
    {
        private BookingObject bookingObject;
        private CustomerObject customerObject;
        public event EventHandler<RoutedEventArgs> WindowClosed;
        private readonly BookingReservation existBooking;

        public BookingPopUp() => InitProp();

        public BookingPopUp(BookingReservation booking)
        {
            this.existBooking = booking;
            InitProp();
            PopulateFields(booking);
        }

        private void InitProp()
        {
            InitializeComponent();
            bookingObject = new BookingObject();
            customerObject = new CustomerObject();
            Loaded += InitForm;
        }

        private void InitForm(object sender, RoutedEventArgs e)
        {
            cbCustomer.ItemsSource = customerObject.GetCustomers();
            cbCustomer.DisplayMemberPath = "CustomerFullName";
            cbCustomer.SelectedValuePath = "CustomerId";
            if (existBooking != null)
            {
                dpBookingDate.SelectedDate = existBooking.BookingDate?.ToDateTime(TimeOnly.MinValue);
                tbTotalPrice.Text = existBooking.TotalPrice.ToString();
                tbBookingStatus.Text = existBooking.BookingStatus.ToString();
                cbCustomer.SelectedValue = existBooking.CustomerId;
            }
        }

        private void AddBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!TakeBooking(out BookingReservation? booking))
            {
                return;
            }

            if (existBooking != null)
            {
                booking!.BookingReservationId = existBooking.BookingReservationId;
                bookingObject.UpdateBooking(booking);
            }
            else
            {
                bookingObject.AddBooking(booking!);
            }

            WindowClosed?.Invoke(sender, e);
            Close();
        }

        private void UpdateBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (existBooking == null) return;

            if (!TakeBooking(out BookingReservation? updatedBooking))
            {
                return;
            }

            updatedBooking.BookingReservationId = existBooking.BookingReservationId;
            bookingObject.UpdateBooking(updatedBooking);
            WindowClosed?.Invoke(sender, e);
            Close();
        }

        private bool TakeBooking(out BookingReservation? validBooking)
        {
            StringBuilder errorMessage = new StringBuilder();
            if (!dpBookingDate.SelectedDate.HasValue)
            {
                errorMessage.Append("Booking date is required.\n");
            }

            if (!decimal.TryParse(tbTotalPrice.Text, out decimal totalPrice))
            {
                errorMessage.Append("Total price must be a valid decimal number.\n");
            }

            if (cbCustomer.SelectedIndex == -1)
            {
                errorMessage.Append("Customer is required.\n");
            }

            if (!byte.TryParse(tbBookingStatus.Text, out byte bookingStatus))
            {
                errorMessage.Append("Booking status must be a byte value.\n");
            }

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage.ToString(), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                validBooking = null;
                return false;
            }

            DateTime bookingDateTime = dpBookingDate.SelectedDate.Value;
            DateOnly bookingDate = DateOnly.FromDateTime(bookingDateTime);

            validBooking = new BookingReservation
            {
                BookingDate = bookingDate,
                TotalPrice = totalPrice,
                CustomerId = (int)cbCustomer.SelectedValue,
                BookingStatus = bookingStatus
            };
            return true;
        }

        private void CancelBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel?", "Booking", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void PopulateFields(BookingReservation booking)
        {
            dpBookingDate.SelectedDate = booking.BookingDate?.ToDateTime(TimeOnly.MinValue);
            tbTotalPrice.Text = booking.TotalPrice.ToString();
            tbBookingStatus.Text = booking.BookingStatus.ToString();
            cbCustomer.SelectedValue = booking.CustomerId;
        }
    }
}
