using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public void DeleteBookingReservation(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                BookingReservation? deleteBooking = context.BookingReservations.Include(b => b.BookingDetails).FirstOrDefault(b => b.BookingReservationId == id);
                if (deleteBooking == null) throw new Exception("No booking reservation was found");
                context.BookingReservations.Remove(deleteBooking);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookingReservation> GetAll()
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.BookingReservations.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookingReservation> GetBookingsByDate(DateOnly startDate, DateOnly endDate)
        {
            try
            {

                using var context = new FuminiHotelManagementContext();
                return context.BookingReservations
                    .Include(b => b.Customer)
                    .Where(b => b.BookingDate >= startDate && b.BookingDate <= endDate)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BookingReservation> GetBookingsOfCustomer(int customerId)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.BookingReservations
                    .Where(b => b.CustomerId == customerId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookingReservation GetById(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.BookingReservations.FirstOrDefault(br => br.BookingReservationId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertBookingReservation(BookingReservation b)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingReservations.Add(b);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBookingReservation(BookingReservation reservation)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry(reservation).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
