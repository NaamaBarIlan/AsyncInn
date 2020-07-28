using Lab12_Relational_DB.Data;
using Lab12_Relational_DB.Model.DTOs;
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

        public async Task<AmenityDTO> Create(AmenityDTO amenityDto)
        {
            Amenity amenity = new Amenity
            {
                Id = amenityDto.ID,
                Name = amenityDto.Name
            };
            
            _context.Entry(amenity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return amenityDto;
        }

        public async Task Delete(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            var amenities = await _context.Amenities.Include(a => a.RoomAmenities)
                                                    .ThenInclude(ra => ra.Room)
                                                    .ToListAsync();
            
            List<AmenityDTO> amenitiesDtos = new List<AmenityDTO>();
            foreach (var item in amenities)
            {
                AmenityDTO amenityDTO = new AmenityDTO
                {
                    ID = item.Id,
                    Name = item.Name
                };

                amenitiesDtos.Add(amenityDTO);
            }

            return amenitiesDtos;
        }

        public async Task<AmenityDTO> GetAmenity(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            AmenityDTO amenityDto = new AmenityDTO
            {
                ID = amenity.Id,
                Name = amenity.Name
            };

            return amenityDto;
        }

        public async Task<AmenityDTO> Update(AmenityDTO amenityDto)
        {
            Amenity amenity = new Amenity
            {
                Id = amenityDto.ID,
                Name = amenityDto.Name
            };

            _context.Entry(amenity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return amenityDto;
        }
    }
}
