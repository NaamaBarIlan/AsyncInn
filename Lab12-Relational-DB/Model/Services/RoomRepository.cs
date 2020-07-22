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

        public async Task<Room> GetRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();

            return rooms;
        }

        public async Task<Room> Update(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return room;
        }
    }
}
