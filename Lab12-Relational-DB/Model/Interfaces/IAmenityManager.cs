using Lab12_Relational_DB.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IAmenityManager
    {
        Task<AmenityDTO> CreateAmenity(AmenityDTO amenity);
        Task DeleteAmenity(int id);
        Task<List<AmenityDTO>> GetAmenities();
        Task<AmenityDTO> GetAmenity(int id);

    }
}
