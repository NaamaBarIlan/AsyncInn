using Lab12_Relational_DB.Data;
using Lab12_Relational_DB.Model.DTOs;
using Lab12_Relational_DB.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private AsyncInnDbContext _context;
        private IRoom _room;

        public HotelRoomRepository(AsyncInnDbContext context, IRoom room)
        {
            _context = context;
            _room = room;
        }

        /// <summary>
        /// Creates a new HotelRoom in the HotelRoom database table,
        /// based on the hotelRoom and a hotelId parameters. 
        /// </summary>
        /// <param name="hotelRoom">The unique hotelRoom object to be created</param>
        /// <param name="hotelId">The unique hotelId used to create the hotelRoom</param>
        /// <returns>The hotelRoom object that was created</returns>
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelroomDto, int hotelId)
        {

            HotelRoom hotelRoom = ConvertDTOIntoEntity(hotelroomDto);

            hotelRoom.HotelId = hotelId;
            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return hotelroomDto;
        }

        /// <summary>
        /// Deletes a HotelRoom from the HotelRoom table in the database, 
        /// based on the hotelId and roomNumber parameters.
        /// </summary>
        /// <param name="hotelId">The unique hotelId to be deleted</param>
        /// <param name="roomNumber">The unique roomNumber to be deleted</param>
        /// <returns>The empty task object</returns>
        public async Task Delete(int hotelId, int roomNumber)
        {

            HotelRoomDTO hotelroomDto = await GetSingleHotelRoom(hotelId, roomNumber);

            HotelRoom hotelRoom = ConvertDTOIntoEntity(hotelroomDto);

            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a list of all the HotelRooms with the hotelId parameter,
        /// from the HotelRoom database table.
        /// </summary>
        /// <param name="hotelId">The unique hotelId used to filter the table</param>
        /// <returns>A list of all hotelRooms with a specific hotelId</returns>
        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _context.HotelRoom.Where(x => x.HotelId == hotelId)
                                                                  .Include(x => x.Room)
                                                                  .ToListAsync();

            List<HotelRoomDTO> hotelRoomDTOs = new List<HotelRoomDTO>();
            foreach (var item in hotelRooms)
            {
                hotelRoomDTOs.Add(await GetSingleHotelRoom(item.HotelId, item.RoomNumber));
            }

            return hotelRoomDTOs;
        }

        /// <summary>
        /// Returns a single HotelRoom from the HotelRoom database table,
        /// based on the hotelId and roomNumber parameters
        /// </summary>
        /// <param name="hotelId">The unique hotelId used to filter the table</param>
        /// <param name="roomNumber">The unique roomNumber used to filter the table</param>
        /// <returns>The single HotelRoom object</returns>
        public async Task<HotelRoomDTO> GetSingleHotelRoom(int hotelId, int roomNumber)
        {
            // Add LINQ
            var hotelroom = await _context.HotelRoom.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                               .Include(x => x.Hotel)
                                               .Include(x => x.Room)
                                               .ThenInclude(x => x.RoomAmenities)
                                               .ThenInclude(x => x.Amenity)
                                               .FirstOrDefaultAsync();


            HotelRoomDTO hotelRoomDto = ConvertEntityIntoDTO(hotelroom);

            // Pulling in the RoomDTO data through the _room context and GetRoom method:
            hotelRoomDto.Room = await _room.GetRoom(hotelroom.RoomId);

            return hotelRoomDto;
        }

        /// <summary>
        /// Updates a hotelroom in the database HotelRoom table, 
        /// based on the hotelroom parameter. 
        /// </summary>
        /// <param name="hotelroom">A unique hotelRoom object to update</param>
        /// <returns>An empty task object</returns>
        public async Task Update(HotelRoomDTO hotelRoomDto)
        {

            HotelRoom hotelRoom = ConvertDTOIntoEntity(hotelRoomDto);

            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Helper method that takes a hotelRoomDto 
        /// and converts it into a hotelRoom entity.
        /// </summary>
        /// <param name="hotelRoomDto">A unique hotelRoomDto object</param>
        /// <returns>A hotelRoom object</returns>
        private HotelRoom ConvertDTOIntoEntity(HotelRoomDTO hotelRoomDto)
        {

            HotelRoom hotelRoom = new HotelRoom
            {
                HotelId = hotelRoomDto.HotelID,
                RoomId = hotelRoomDto.RoomID,
                RoomNumber = hotelRoomDto.RoomNumber,
                Rate = hotelRoomDto.Rate,
                PetFriendly = hotelRoomDto.PetFriendly,
            };

            return hotelRoom;
        }

        /// <summary>
        /// Helper method that takes a hotelRoom  
        /// and converts it into a hotelRoomDto.
        /// </summary>
        /// <param name="hotelRoom">A unique hotelRoom object</param>
        /// <returns>A hotelRoomDto object</returns>
        private HotelRoomDTO ConvertEntityIntoDTO(HotelRoom hotelRoom)
        {

            HotelRoomDTO hotelRoomDto = new HotelRoomDTO
            {
                HotelID = hotelRoom.HotelId,
                RoomID = hotelRoom.RoomId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
            };

            return hotelRoomDto;
        }

    }
}
