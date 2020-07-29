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

        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);

            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

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

        public async Task<List<Hotel>> GetHotels()
        {
            var hotels = await _context.Hotels.Include(h => h.HotelRooms)
                                              .ThenInclude(hr => hr.Room)
                                              .ThenInclude(r => r.RoomAmenities)
                                              .ThenInclude(ra => ra.Amenity)
                                              .ToListAsync();

            return hotels;
        }

        public async Task<Hotel> Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotel;

        }
    }
}
