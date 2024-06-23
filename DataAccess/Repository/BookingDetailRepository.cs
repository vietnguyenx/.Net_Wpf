using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public void Delete(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                BookingDetail? deleteBookingDetail = context.BookingDetails.FirstOrDefault(bd => bd.BookingReservationId == id) ?? throw new Exception("No booking details was found");
                context.BookingDetails.Remove(deleteBookingDetail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookingDetail> GetAll()
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.BookingDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookingDetail GetById(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.BookingDetails.FirstOrDefault(c => c.BookingReservationId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertBookingDetail(BookingDetail detail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.BookingDetails.Add(detail);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBookingDetail(BookingDetail detail)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Entry(detail).State = EntityState.Modified;
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
