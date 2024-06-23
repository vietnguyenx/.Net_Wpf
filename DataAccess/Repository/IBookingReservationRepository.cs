using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBookingReservationRepository
    {
        public List<BookingReservation> GetAll();
        public BookingReservation GetById(int id);
        public void DeleteBookingReservation(int id);
        public void InsertBookingReservation(BookingReservation b);
        public void UpdateBookingReservation(BookingReservation b);
        List<BookingReservation> GetBookingsByDate(DateOnly startDate, DateOnly endDate);
        List<BookingReservation> GetBookingsOfCustomer(int customerId);
    }
}
