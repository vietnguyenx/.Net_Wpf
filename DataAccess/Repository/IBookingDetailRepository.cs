using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBookingDetailRepository
    {
        public List<BookingDetail> GetAll();
        public BookingDetail GetById(int id);
        public void Delete(int id);
        public void InsertBookingDetail(BookingDetail detail);
        public void UpdateBookingDetail(BookingDetail detail);
    }
}
