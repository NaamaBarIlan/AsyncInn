using Lab12_Relational_DB.Data;
using Lab12_Relational_DB.Model.DTOs;
using Lab12_Relational_DB.Model.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Services
{
    public class RoomRepository : IRoomManager
    {
        private AsyncInnDbContext _context;
        private IAmenityManager _amenities;

        public RoomRepository(AsyncInnDbContext context, IAmenityManager amenities)
        {
            _context = context;
            _amenities = amenities;
        }

        public async Task<RoomDTO> CreateRoom(RoomDTO dto)
        {
            // convert a roomdto to a room entity
            Enum.TryParse(dto.Layout, out Layout layout);
            Room room = new Room()
            {
                Name = dto.Name,
                Layout = layout
            };

            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);

            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific room by Id, including all of the RoomAmenities that the room has
        /// </summary>
        /// <param name="id">Unique identifier of the room</param>
        /// <returns>The list of all RoomAmenities in the specific room</returns>
        public async Task<RoomDTO> GetRoom(int id)
        {
            // include all of the RoomAmenities that the room has
            var room = await _context.Rooms.Where(x => x.ID == id)
                                           .Include(x => x.RoomAmenity)
                                           .FirstOrDefaultAsync();

            RoomDTO dto = new RoomDTO
            {
                Name = room.Name,
                Layout = room.Layout.ToString(),
                ID = room.Id
            };

            dto.Amenities = new List<AmenityDTO>();
            foreach (var item in room.RoomAmenities)
            {
                dto.Amenities.Add(await _amenities.GetAmenity(item.AmenitiesID));
            }

            return dto;
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            List<RoomDTO> dtos = new List<RoomDTO>();

            foreach (var room in rooms)
            {
                dtos.Add(await GetRoom(room.ID));
            }

            return dtos;
        }

        public async Task UpdateRoom(RoomDTO dto)
        {
            Enum.TryParse(dto.Layout, out Layout layout);

            Room room = new Room()
            {
                Layout = layout,
                Name = dto.Name,
                Id = dto.ID,
            };

            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a specified amenity from a specific room
        /// </summary>
        /// <param name="roomId">Unique identifier of the room</param>
        /// <param name="amenityId">Unique identifier of the</param>
        /// <returns>Task of completion</returns>
        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenities roomAmenities = new RoomAmenities()
            {
                RoomId = roomId,
                AmenityId = amenityId
            };

            _context.Entry(roomAmenities).State = EntityState.Added;
            await _context.SaveChangesAsync();

        }
       
        /// <summary>
        /// Removes a specified amenity from a specific room
        /// </summary>
        /// <param name="roomId">Unique identifier of the room</param>
        /// <param name="amenityId">Unique identifier of the amenity</param>
        /// <returns>Task of completion</returns>
        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            //look in the RoomAmenities table for the entry that matches the courseId and the studentId
            var result = await _context.RoomAmenities.FirstOrDefaultAsync(X => X.RoomId == roomId && X.AmenityId == amenityId);
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
           
        }

    }
}
