using DataAccess.Models;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class RoomObject
    {
        public readonly IRoomRepository _roomRepository;

        public RoomObject()
        {
            _roomRepository = new RoomRepository();
        }

        public List<RoomInformation> GetRoom() => _roomRepository.GetAll();

        public List<RoomType> GetRoomTypes() => _roomRepository.GetRoomTypes();

        public void AddRoom(RoomInformation room) => _roomRepository.InsertRoom(room);

        public void UpdateRoom(RoomInformation room) => _roomRepository.UpdateRoom(room);
        public void DeleteRoom(int roomId) => _roomRepository.DeleteRoom(roomId);
        public List<RoomInformation> SearchRoom(string keyword) => _roomRepository.SearchRoom(keyword);

    }
}
