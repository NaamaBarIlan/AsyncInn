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
        private HotelDbContext _context;

        public HotelRoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoom> Create(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom);
        }

        public async Task Delete(int roomNumber, int hotelId)
        {
            var hotelRoom = GetSingleHotelRoom(roomNumber, hotelId);
            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<HotelRoom>> GetRooms(int hotelId)
        {
            List<HotelRoom> hotelRooms = await _context.HotelRooms.Where(x => x.Hotel.Id == hotelId)
                                                                  .Include()
                                                                  .ToListAsync();

            return hotelRooms;
        }

        public async Task<HotelRoom> GetSingleHotelRoom(int roomNumber, int hotelId)
        {
            // Add LINQ
            var hotelRoom = await _context.HotelRooms.FindAsync(roomNumber, hotelId);

            return hotelRoom;
        }

        public async Task Update(HotelRoom hotelroom)
        {
            _context.Entity(hotelroom).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
