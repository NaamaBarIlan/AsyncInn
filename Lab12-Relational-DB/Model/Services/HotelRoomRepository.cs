using Lab12_Relational_DB.Data;
using Lab12_Relational_DB.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private AsyncInnDbContext _context;

        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new HotelRoom in the HotelRoom database table,
        /// based on the hotelRoom and a hotelId parameters. 
        /// </summary>
        /// <param name="hotelRoom">The unique hotelRoom object to be created</param>
        /// <param name="hotelId">The unique hotelId used to create the hotelRoom</param>
        /// <returns>The hotelRoom object that was created</returns>
        public async Task<HotelRoom> Create(HotelRoom hotelRoom, int hotelId)
        {
            hotelRoom.HotelId = hotelId;
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        /// <summary>
        /// Deletes a HotelRoom from the HotelRoom table in the database, 
        /// based on the hotelId and roomNumber parameters.
        /// </summary>
        /// <param name="hotelId">The unique hotelId to be deleted</param>
        /// <param name="roomNumber">The unique roomNumber to be deleted</param>
        /// <returns>The empty task object</returns>
        public async Task Delete(int hotelId, int roomNumber)
        {
            var hotelRoom = await GetSingleHotelRoom(roomNumber, hotelId);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a list of all the HotelRooms with the hotelId parameter,
        /// from the HotelRoom database table.
        /// </summary>
        /// <param name="hotelId">The unique hotelId used to filter the table</param>
        /// <returns>A list of all hotelRooms with a specific hotelId</returns>
        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> hotelRooms = await _context.HotelRoom.Where(x => x.HotelId == hotelId)
                                                                  .Include(x => x.Room)
                                                                  .ToListAsync();

            return hotelRooms;
        }

        /// <summary>
        /// Returns a single HotelRoom from the HotelRoom database table,
        /// based on the hotelId and roomNumber parameters
        /// </summary>
        /// <param name="hotelId">The unique hotelId used to filter the table</param>
        /// <param name="roomNumber">The unique roomNumber used to filter the table</param>
        /// <returns>The single HotelRoom object</returns>
        public async Task<HotelRoom> GetSingleHotelRoom(int hotelId, int roomNumber)
        {
            // Add LINQ
            //var hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);

            var room = await _context.HotelRoom.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                               .Include(x => x.Hotel)
                                               .Include(x => x.Room)
                                               .ThenInclude(x => x.RoomAmenities)
                                               .ThenInclude(x => x.Amenity)
                                               .FirstOrDefaultAsync();

            return room;
        }

        /// <summary>
        /// Updates a hotelroom in the database HotelRoom table, 
        /// based on the hotelroom parameter. 
        /// </summary>
        /// <param name="hotelroom">A unique hotelRoom object to update</param>
        /// <returns>An empty task object</returns>
        public async Task Update(HotelRoom hotelroom)
        {
            _context.Entry(hotelroom).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
