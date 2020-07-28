using Lab12_Relational_DB.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Interfaces
{
    public interface IRoomManager
    {
        Task<RoomDTO> CreateRoom(RoomDTO room);

        Task UpdateRoom(RoomDTO room);

        Task DeleteRoom(int id);

        Task<List<RoomDTO>> GetRooms();

        Task<RoomDTO> GetRoom(int id);

        Task AddAmenityToRoom(int roomId, int amenityId);

        Task RemoveAmenityFromRoom(int roomId, int amenityId);

    }
}
