using Lab12_Relational_DB.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IAmenity
    {
        // Create
        Task<AmenityDTO> Create(AmenityDTO amenityDto);

        // Read
        // Get ALL
        Task<List<AmenityDTO>> GetAmenities();

        // Get individually (by id)
        Task<AmenityDTO> GetAmenity(int id);

        // Update
        Task<AmenityDTO> Update(AmenityDTO amenityDto);

        // Delete
        Task Delete(int id);
    }
}
