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
        /// Creates a new HotelRoom in the HotelRoom database table,
        /// based on the hotelRoom and a hotelId parameters. 
        /// </summary>
        /// <param name="hotelRoom">The unique hotelRoom object to be created</param>
        /// <param name="hotelId">The unique hotelId used to create the hotelRoom</param>
        /// <returns>The hotelRoom object that was created</returns>
        Task<HotelRoom> Create(HotelRoom hotelroom, int hotelId);

        /// <summary>
        /// Updates a hotelroom in the database HotelRoom table, 
        /// based on the hotelroom parameter. 
        /// </summary>
        /// <param name="hotelroom">A unique hotelRoom object to update</param>
        /// <returns>An empty task object</returns>
        Task Update(HotelRoom hotelroom);

        /// <summary>
        /// Deletes a HotelRoom from the HotelRoom table in the database, 
        /// based on the hotelId and roomNumber parameters.
        /// </summary>
        /// <param name="hotelId">The unique hotelId to be deleted</param>
        /// <param name="roomNumber">The unique roomNumber to be deleted</param>
        /// <returns>The empty task object</returns>
        Task Delete(int hotelId, int roomNumber);

        /// <summary>
        /// Returns a list of all the HotelRooms with the hotelId parameter,
        /// from the HotelRoom database table.
        /// </summary>
        /// <param name="hotelId">The unique hotelId used to filter the table</param>
        /// <returns>A list of all hotelRooms with a specific hotelId</returns>
        Task<List<HotelRoom>> GetHotelRooms(int hotelId);

        /// <summary>
        /// Returns a single HotelRoom from the HotelRoom database table,
        /// based on the hotelId and roomNumber parameters
        /// </summary>
        /// <param name="hotelId">The unique hotelId used to filter the table</param>
        /// <param name="roomNumber">The unique roomNumber used to filter the table</param>
        /// <returns>The single HotelRoom object</returns>
        Task<HotelRoom> GetSingleHotelRoom(int hotelId, int roomNumber);

    }
}
