using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRoomRepository
    {
        public RoomInformation GetById(int id);
        public List<RoomInformation> GetAll();
        public void InsertRoom(RoomInformation r);
        public void UpdateRoom(RoomInformation r);
        public void DeleteRoom(int id);
        public List<RoomType> GetRoomTypes();
        public List<RoomInformation> SearchRoom(string keyword);
    }
}
