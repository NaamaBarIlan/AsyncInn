using Lab12_Relational_DB.Data;
using Lab12_Relational_DB.Model.DTOs;
using Lab12_Relational_DB.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Services
{
    public class RoomRepository : IRoom
    {
        private AsyncInnDbContext _context;
        private IAmenity _amenity;

        public RoomRepository(AsyncInnDbContext context, IAmenity amenity)
        {
            _context = context;
            _amenity = amenity;
        }

        /// <summary>
        /// Creates a new entry in the Rooms database table,
        /// based on the room parameter.
        /// </summary>
        /// <param name="room">Unique identifier of the room</param>
        /// <returns>The created room object</returns>
        public async Task<RoomDTO> Create(RoomDTO roomDto)
        {
            Room room = new Room
            {
                Id = roomDto.ID,
                Name = roomDto.Name,
                Layout = roomDto.Layout,
            };

            // when I have a room, I want to add them to the db:
            _context.Entry(room).State = EntityState.Added;

            // the room gets 'saved' here, and then associated with an id. 
            await _context.SaveChangesAsync();

            return roomDto;
        }

        /// <summary>
        /// Deletes a room from the Rooms database table,
        /// based on the room Id parameter.
        /// </summary>
        /// <param name="id">Unique identifier of the room</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int id)
        {
            RoomDTO roomDto = await GetRoom(id);

            Room room = new Room
            {
                Id = roomDto.ID,
                Name = roomDto.Name,
                Layout = roomDto.Layout,

            };

            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a list of all of the RoomAmenities associated with the specified room,
        /// in the Rooms database table
        /// </summary>
        /// <param name="id">Unique identifier of the room</param>
        /// <returns>A list of all RoomAmenities in the specific room</returns>
        public async Task<RoomDTO> GetRoom(int id)
        {
            var room = await _context.Rooms.Where(r => r.Id == id)
                                           .Include(r => r.RoomAmenities)
                                           .ThenInclude(ra => ra.Amenity)
                                           .FirstOrDefaultAsync();

            RoomDTO roomDto = new RoomDTO
            {
                ID = room.Id,
                Name = room.Name,
                Layout = room.Layout,

            };

            roomDto.Amenities = new List<AmenityDTO>();

            foreach (var amenity in room.RoomAmenities)
            {
                roomDto.Amenities.Add(await _amenity.GetAmenity(amenity.AmenityId));
            }

            return roomDto;
        }

        /// <summary>
        /// Returns a list of all of the rooms in the Rooms database table,
        /// and all of the RoomAmenities associated with each room.
        /// </summary>
        /// <returns>A list of all the rooms and associated RoomAmenities</returns>
        public async Task<List<RoomDTO>> GetRooms()
        {
            var rooms = await _context.Rooms.Include(r => r.RoomAmenities)
                                            .ThenInclude(ra => ra.Amenity)
                                            .ToListAsync();

            List<RoomDTO> roomDTOs = new List<RoomDTO>();
            foreach (var room in rooms)
            {

                roomDTOs.Add(await GetRoom(room.Id));
            }

            return roomDTOs;
        }

        /// <summary>
        /// Updates a specific room in the Rooms database table,
        /// based on the room parameter.
        /// </summary>
        /// <param name="room">Unique identifier of the room</param>
        /// <returns>The updated room object</returns>
        public async Task<RoomDTO> Update(RoomDTO roomDto)
        {
            Room room = new Room
            {
                Id = roomDto.ID,
                Name = roomDto.Name,
                Layout = roomDto.Layout,
            };

            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return roomDto;
        }

        /// <summary>
        /// Adds a specified amenity from a specific room to the Rooms database table.
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
        /// Removes a specified amenity from a specific room in the Rooms database table.
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