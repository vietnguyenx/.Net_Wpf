using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class BookingObject
    {
        private readonly IBookingReservationRepository _reservationRepository;

        public BookingObject()
        {
            _reservationRepository = new BookingReservationRepository();
        }

        public List<BookingReservation> GetBooking() => _reservationRepository.GetAll();
        public void AddBooking(BookingReservation booking) => _reservationRepository.InsertBookingReservation(booking);
        public void UpdateBooking(BookingReservation booking) => _reservationRepository.UpdateBookingReservation(booking);
        public void DeleteBooking(int bookingId) => _reservationRepository.DeleteBookingReservation(bookingId);
        public List<BookingReservation> GetBookingsByDate(DateOnly startDate, DateOnly endDate) 
            => _reservationRepository.GetBookingsByDate(startDate, endDate);

        public List<BookingReservation> GetBookingsOfCustomer(int customerId) => _reservationRepository.GetBookingsOfCustomer(customerId);
    }
}
