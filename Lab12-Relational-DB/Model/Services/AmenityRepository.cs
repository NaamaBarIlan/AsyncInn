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

        /// <summary>
        /// Creates a new entry in the Amenity database table,
        /// based on the AmenityDTO parameter.
        /// </summary>
        /// <param name="amenityDto">A unique AmenityDTO object</param>
        /// <returns>The created amenityDto object</returns>
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

        /// <summary>
        /// Deletes an Amenity from the Amenities database table,
        /// based on the Amenity Id parameter.
        /// </summary>
        /// <param name="id">A unique Amenity ID number</param>
        /// <returns>An empty task object</returns>
        public async Task Delete(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);

            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a list of all the Amenities in the Amenities database table,
        /// converted into AmenityDTOs objects.
        /// </summary>
        /// <returns>A list of all of the AmenityDtos</returns>
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

        /// <summary>
        /// Returns a specific Amenity from the Amenities database table,
        /// converted into an AmenityDto.
        /// </summary>
        /// <param name="id">A unique integer amenity ID value</param>
        /// <returns>A specific amenityDto object</returns>
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

        /// <summary>
        /// Updates a specific Amenity in the Amenity database,
        /// based on the amenityDto parameter.
        /// </summary>
        /// <param name="amenityDto">A unique amenityDto object</param>
        /// <returns>An updated amenityDto object</returns>
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
