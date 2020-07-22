using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IRoom
    {
        // Create
        Task<Room> Create(Room room);

        // Read
        // Get All
        Task<List<Room>> GetRooms();

        // Get individually (by Id)
        Task<Room> GetRoom(int id);

        // Update
        Task<Room> Update(Room room);

        // Delete
        Task Delete(int id);
    }
}
