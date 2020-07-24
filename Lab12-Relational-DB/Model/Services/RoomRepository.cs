using Lab12_Relational_DB.Data;
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

        public RoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Room> Create(Room room)
        {
            // when I have a room, I want to add them to the db:
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            // the room gets 'saved' here, and then associated with an id. 
            await _context.SaveChangesAsync();

            return room;
        }

        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);

            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a specific room by Id, including all of the RoomAmenities that the room has
        /// </summary>
        /// <param name="id">Unique identifier of the room</param>
        /// <returns>The list of all RoomAmenities in the specific room</returns>
        public async Task<Room> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            // include all of the RoomAmenities that the room has
            var roomAmenities = await _context.RoomAmenities.Where(x => x.AmenityId == id)
                                                            .Include(x => x.Amenity)
                                                            .ToListAsync();

            room.RoomAmenities = roomAmenities;
            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _context.Rooms.Include(x => x.RoomAmenities)
                                            .ToListAsync();

            return rooms;
        }

        public async Task<Room> Update(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return room;
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
