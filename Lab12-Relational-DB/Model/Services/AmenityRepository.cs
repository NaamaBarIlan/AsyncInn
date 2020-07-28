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
    public class AmenityRepository : IAmenity
    {
        private AsyncInnDbContext _context;

        public AmenityRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return amenity;
        }

        public async Task Delete(int id)
        {
            Amenity amenity = await GetAmenity(id);

            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Amenity>> GetAmenities()
        {
            var amenities = await _context.Amenities.Include(a => a.RoomAmenities)
                                                    .ThenInclude(ra => ra.Room)
                                                    .ToListAsync();

            return amenities;
        }

        public async Task<Amenity> GetAmenity(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            var amenities = await _context.RoomAmenities.Where(x => x.AmenityId == id)
                                                        .Include(x => x.Room)
                                                        .ToListAsync();

            amenity.RoomAmenities = amenities;
            return amenity;
        }

        public async Task<Amenity> Update(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return amenity;
        }
    }
}
