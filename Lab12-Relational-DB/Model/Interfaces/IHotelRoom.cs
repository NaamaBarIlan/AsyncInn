using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IHotelRoom
    {
        Task<HotelRoom> Create(HotelRoom hotelroom, int hotelId);

        Task Update(HotelRoom hotelroom);

        Task Delete(int hotelId, int roomNumber);

        Task<List<HotelRoom>> GetHotelRooms(int hotelId);

        Task<HotelRoom> GetSingleHotelRoom(int hotelId, int roomNumber);

    }
}
