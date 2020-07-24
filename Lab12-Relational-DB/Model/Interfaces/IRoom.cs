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

        // Create: RoomAmenities method
        /// <summary>
        /// Adds a specified amenity from a specific room
        /// </summary>
        /// <param name="roomId">Unique identifier of the room</param>
        /// <param name="amenityId">Unique identifier of the</param>
        /// <returns>Task of completion</returns>
        Task AddAmenityToRoom(int roomId, int amenityId);

        // Delete: Remove amenity from room method
        /// <summary>
        /// Removes a specified amenity from a specific room
        /// </summary>
        /// <param name="roomId">Unique identifier of the room</param>
        /// <param name="amenityId">Unique identifier of the amenity</param>
        /// <returns>Task of completion</returns>
        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }

   
}
