using Lab12_Relational_DB.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IHotel
    {
        /// <summary>
        /// Creates a new entry in the Hotels database table,
        /// based on the hotel parameter.
        /// </summary>
        /// <param name="hotel">Unique identifier of the hotel</param>
        /// <returns>The created hotel object</returns>
        Task<HotelDTO> Create(HotelDTO hotelDto);

        /// <summary>
        /// Returns a list of all of the hotels in the Hotels database table,
        /// and all of the rooms, room amenities, and amenities associated with each hotel.
        /// </summary>
        /// <returns>A list of all hotels and all of the rooms, room amenities, 
        /// and amenities associated with each hotel.</returns>
        Task<List<HotelDTO>> GetHotels();

        /// <summary>
        /// Returns the specified hotel and a list of all of the rooms, room amenities, and amenities 
        /// associated with the hotel in the Hotels database table.
        /// </summary>
        /// <param name="id">Unique identifier of the hotel</param>
        /// <returns>The specified hotel and a list of all of the associated 
        /// rooms, room amenities, and amenities</returns>
        Task<HotelDTO> GetHotel(int id);

        /// <summary>
        /// Updates a specific hotel in the Hotels database table,
        /// based on the hotel parameter.
        /// </summary>
        /// <param name="hotel">Unique identifier of the hotel</param>
        /// <returns>The updated hotel object</returns>
        Task<Hotel> Update(Hotel hotel);

        /// <summary>
        /// Deletes a hotel from the Hotels database table,
        /// based on the hotel Id parameter.
        /// </summary>
        /// <param name="id">Unique identifier of the hotel</param>
        /// <returns>Task of completion</returns>
        Task Delete(int id);

    }
}
