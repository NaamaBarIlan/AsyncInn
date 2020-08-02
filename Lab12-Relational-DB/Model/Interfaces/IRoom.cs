using Lab12_Relational_DB.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IRoom
    {
        /// <summary>
        /// Creates a new entry in the Rooms database table,
        /// based on the room parameter.
        /// </summary>
        /// <param name="room">Unique identifier of the room</param>
        /// <returns>The created room object</returns>
        Task<RoomDTO> Create(RoomDTO roomDto);

        /// <summary>
        /// Returns a list of all of the rooms in the Rooms database table,
        /// and all of the RoomAmenities associated with each room.
        /// </summary>
        /// <returns>A list of all the rooms and associated RoomAmenities</returns>
        Task<List<RoomDTO>> GetRooms();

        /// <summary>
        /// Returns a list of all of the RoomAmenities associated with the specified room,
        /// in the Rooms database table
        /// </summary>
        /// <param name="id">Unique identifier of the room</param>
        /// <returns>A list of all RoomAmenities in the specific room</returns>
        Task<RoomDTO> GetRoom(int id);

        /// <summary>
        /// Updates a specific room in the Rooms database table,
        /// based on the room parameter.
        /// </summary>
        /// <param name="room">Unique identifier of the room</param>
        /// <returns>The updated room object</returns>
        Task<RoomDTO> Update(RoomDTO roomDto);

        /// <summary>
        /// Deletes a room from the Rooms database table,
        /// based on the room Id parameter.
        /// </summary>
        /// <param name="id">Unique identifier of the room</param>
        /// <returns>Task of completion</returns>
        Task Delete(int id);

        /// <summary>
        /// Adds a specified amenity from a specific room to the Rooms database table.
        /// </summary>
        /// <param name="roomId">Unique identifier of the room</param>
        /// <param name="amenityId">Unique identifier of the</param>
        /// <returns>Task of completion</returns>
        Task AddAmenityToRoom(int roomId, int amenityId);

        /// <summary>
        /// Removes a specified amenity from a specific room in the Rooms database table.
        /// </summary>
        /// <param name="roomId">Unique identifier of the room</param>
        /// <param name="amenityId">Unique identifier of the amenity</param>
        /// <returns>Task of completion</returns>
        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }

   
}
