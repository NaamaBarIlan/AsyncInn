using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IAmenity
    {
        // Create
        Task<Amenity> Create(Amenity amenity);

        // Read
        // Get ALL
        Task<List<Amenity>> GetAmenities();

        // Get individually (by id)
        Task<Amenity> GetAmenity(int id);

        // Update
        Task<Amenity> Update(Amenity amenity);

        // Delete
        Task Delete(int id);
    }
}
