using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IHotel
    {

        //Contain methods and properties that are required for the classes to implement

        // Create
        Task<Hotel> Create(Hotel hotel);

        // Read
        // Get All
        Task<List<Hotel>> GetHotels();

        // Get individually (by Id)
        Task<Hotel> GetHotel(int id);

        // Update
        Task<Hotel> Update(Hotel hotel);

        // Delete
        Task Delete(int id);


    }
}
