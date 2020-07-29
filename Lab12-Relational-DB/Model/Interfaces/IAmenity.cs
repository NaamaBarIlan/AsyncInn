using Lab12_Relational_DB.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IAmenity
    {
        /// <summary>
        /// This method takes an AmenityDTO object as paramater 
        /// and creates a new entry in the Amenity database table
        /// </summary>
        /// <param name="amenityDto">A unique AmenityDTO object</param>
        /// <returns>The amenityDto object</returns>
        Task<AmenityDTO> Create(AmenityDTO amenityDto);

        /// <summary>
        /// This method pulls a list of all the rows in the Amenities database table 
        /// and converts them into AmenityDTOs.
        /// </summary>
        /// <returns>Returns a list of all of the AmenityDtos</returns>
        Task<List<AmenityDTO>> GetAmenities();

        /// <summary>
        /// This method takes an Amenity id, 
        /// pulls that specific row in the Amenities database table 
        /// and converts it into an AmenityDto.
        /// </summary>
        /// <param name="id">A unique integer amenity ID value</param>
        /// <returns>A specific amenityDto object</returns>
        Task<AmenityDTO> GetAmenity(int id);

        /// <summary>
        /// This method takes an amenityDto object
        /// and updates the entry in the Amenity database.
        /// </summary>
        /// <param name="amenityDto">A unique amenityDto object</param>
        /// <returns>The updated amenityDto object</returns>
        Task<AmenityDTO> Update(AmenityDTO amenityDto);

        /// <summary>
        /// This method takes an Amenity Id integer
        /// and deletes the corresponding row from the Amenities database table
        /// </summary>
        /// <param name="id">A unique Amenity ID number</param>
        /// <returns>The complete task</returns>
        Task Delete(int id);
    }
}
