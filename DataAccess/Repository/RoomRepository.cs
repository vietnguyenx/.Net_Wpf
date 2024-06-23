using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RoomRepository : IRoomRepository
    {
        public void DeleteRoom(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                RoomInformation? deleteRoom = context.RoomInformations.FirstOrDefault(r => r.RoomId == id) ?? throw new Exception("No room was found");
                context.RoomInformations.Remove(deleteRoom);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RoomInformation> GetAll()
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.RoomInformations
                    .Include(r => r.RoomType)
                    .ToList();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public RoomInformation GetById(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.RoomInformations.FirstOrDefault(r => r.RoomId == id);
          
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertRoom(RoomInformation r)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.RoomInformations.Add(r);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRoom(RoomInformation room)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var existingRoom = context.RoomInformations.FirstOrDefault(r => r.RoomId == room.RoomId);
                if (existingRoom == null)
                {
                    throw new Exception("Room not found");
                }

                existingRoom.RoomNumber = room.RoomNumber;
                existingRoom.RoomDetailDescription = room.RoomDetailDescription;
                existingRoom.RoomMaxCapacity = room.RoomMaxCapacity;
                existingRoom.RoomTypeId = room.RoomTypeId;
                existingRoom.RoomStatus = room.RoomStatus;
                existingRoom.RoomPricePerDay = room.RoomPricePerDay;

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RoomType> GetRoomTypes()
        {
            using var context = new FuminiHotelManagementContext();
            return context.RoomTypes.ToList();
        }

        public List<RoomInformation> SearchRoom(string keyword)
        {
            using var context = new FuminiHotelManagementContext();
            return context.RoomInformations
                .Include(r => r.RoomType)
                .Where(r => r.RoomNumber.Contains(keyword) ||
                            r.RoomDetailDescription.Contains(keyword) ||
                            r.RoomType.RoomTypeName.Contains(keyword))
                .ToList();
        }
    }
}
