using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IHotelRoom
    {
        /// <summary>
        /// This method creates a new HotelRoom row in the HotelRoom database table,
        /// it takes hotelRoom and a hotelId as parameters and returns a hotelRoom object. 
        /// </summary>
        /// <param name="hotelRoom">The unique hotelRoom object to be created</param>
        /// <param name="hotelId">The unique hotelId used to create the hotelRoom</param>
        /// <returns>The hotelRoom object that was created</returns>
        Task<HotelRoom> Create(HotelRoom hotelroom, int hotelId);

        Task Update(HotelRoom hotelroom);

        /// <summary>
        /// This method takes in a hotelId and roomNumber as parameters, 
        /// and deletes the row from the HotelRoom table in the database.
        /// </summary>
        /// <param name="hotelId">The unique hotelId to be deleted</param>
        /// <param name="roomNumber">The unique roomNumber to be deleted</param>
        /// <returns>The complete task</returns>
        Task Delete(int hotelId, int roomNumber);

        /// <summary>
        /// This method takes a hotelId integer and returns a list
        /// of all the HotelRooms with that Id in the HotelRoom database table. 
        /// </summary>
        /// <param name="hotelId">The unique hotelId used to filter the table rows</param>
        /// <returns>A list of all hotelRooms with a specific hotelId</returns>
        Task<List<HotelRoom>> GetHotelRooms(int hotelId);

        /// <summary>
        /// This method takes a hotelId integer and returns a single
        /// HotelRoom with that Id in the HotelRoom database table.
        /// </summary>
        /// <param name="hotelId">The unique hotelId used to filter the table</param>
        /// <param name="roomNumber">The unique roomNumber used to filter the table</param>
        /// <returns>The single HotelRoom object</returns>
        Task<HotelRoom> GetSingleHotelRoom(int hotelId, int roomNumber);

    }
}
