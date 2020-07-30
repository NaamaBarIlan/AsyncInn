using Lab12_Relational_DB.Data;
using Lab12_Relational_DB.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Services
{
    public class HotelRepository : IHotel
    {
        private AsyncInnDbContext _context;

        public HotelRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new entry in the Hotels database table,
        /// based on the hotel parameter.
        /// </summary>
        /// <param name="hotel">Unique identifier of the hotel</param>
        /// <returns>The created hotel object</returns>
        public async Task<Hotel> Create(Hotel hotel)
        {
            // when I have a hotel, I want to add them to the database.
            // old way:
            //_context.Hotels.Add(hotel);
            // new way:
            _context.Entry(hotel).State = EntityState.Added;

            //tell our DB to save changes Async:
            // the hotel gets 'saved' here, and then associated with an id 
            await _context.SaveChangesAsync();

            return hotel;

        }

        /// <summary>
        /// Deletes a hotel from the Hotels database table,
        /// based on the hotel Id parameter.
        /// </summary>
        /// <param name="id">Unique identifier of the room</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);

            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns the specified hotel and a list of all of the rooms, room amenities, and amenities 
        /// associated with the hotel in the Hotels database table.
        /// </summary>
        /// <param name="id">Unique identifier of the hotel</param>
        /// <returns>The specified hotel and a list of all of the associated 
        /// rooms, room amenities, and amenities</returns>
        public async Task<Hotel> GetHotel(int id)
        {
            // Look in the DB on the Hotel table, 
            // where the id is equal to the id that was brought in as an argument
            var hotel = await _context.Hotels.Where(h => h.Id == id)
                                             .Include(h => h.HotelRooms)
                                             .ThenInclude(hr => hr.Room)
                                             .ThenInclude(r => r.RoomAmenities)
                                             .ThenInclude(ra => ra.Amenity)
                                             .FirstOrDefaultAsync();

            return hotel;
        }

        /// <summary>
        /// Returns a list of all of the hotels in the Hotels database table,
        /// and all of the rooms, room amenities, and amenities associated with each hotel.
        /// </summary>
        /// <returns>A list of all hotels and all of the rooms, room amenities, 
        /// and amenities associated with each hotel.</returns>
        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels.Include(h => h.HotelRooms)
                                              .ThenInclude(hr => hr.Room)
                                              .ThenInclude(r => r.RoomAmenities)
                                              .ThenInclude(ra => ra.Amenity)
                                              .ToListAsync();

            return hotels;
        }

        /// <summary>
        /// Updates a specific hotel in the Hotels database table,
        /// based on the hotel parameter.
        /// </summary>
        /// <param name="hotel">Unique identifier of the hotel</param>
        /// <returns>The updated hotel object</returns>
        public async Task<Hotel> Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotel;

        }
    }
}
