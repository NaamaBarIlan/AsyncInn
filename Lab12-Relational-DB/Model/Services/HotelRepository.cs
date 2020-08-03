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
    public class HotelRepository : IHotel
    {
        private AsyncInnDbContext _context;
        private IHotelRoom _hotelRoom;

        public HotelRepository(AsyncInnDbContext context, IHotelRoom hotelRoom)
        {
            _context = context;
            _hotelRoom = hotelRoom;
        }

        /// <summary>
        /// Creates a new entry in the Hotels database table,
        /// based on the hotel parameter.
        /// </summary>
        /// <param name="hotel">Unique identifier of the hotel</param>
        /// <returns>The created hotel object</returns>
        public async Task<HotelDTO> Create(HotelDTO hotelDto)
        {
            Hotel hotel = ConvertDTOIntoHotel(hotelDto);

            // when I have a hotel, I want to add it to the database.
            _context.Entry(hotel).State = EntityState.Added;

            //tell our DB to save changes Async:
            // the hotel gets 'saved' here, and then associated with an id 
            await _context.SaveChangesAsync();

            return hotelDto;

        }

        /// <summary>
        /// Deletes a hotel from the Hotels database table,
        /// based on the hotel Id parameter.
        /// </summary>
        /// <param name="id">Unique identifier of the room</param>
        /// <returns>Task of completion</returns>
        public async Task Delete(int id)
        {
            HotelDTO hotelDto = await GetHotel(id);

            _context.Entry(hotelDto).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns the specified hotel and a list of all of the rooms, room amenities, and amenities 
        /// associated with the hotel in the Hotels database table.
        /// </summary>
        /// <param name="id">Unique identifier of the hotel</param>
        /// <returns>The specified hotel and a list of all of the associated 
        /// rooms, room amenities, and amenities</returns>
        public async Task<HotelDTO> GetHotel(int id)
        {
            // Look in the DB on the Hotel table, 
            // where the id is equal to the id that was brought in as an argument
            var hotel = await _context.Hotels.Where(h => h.Id == id)
                                             .Include(h => h.HotelRooms)
                                             .ThenInclude(hr => hr.Room)
                                             .ThenInclude(r => r.RoomAmenities)
                                             .ThenInclude(ra => ra.Amenity)
                                             .FirstOrDefaultAsync();


            HotelDTO hotelDto = ConvertHotelIntoDTO(hotel);

            hotelDto.Rooms = await _hotelRoom.GetHotelRooms(hotel.Id);

            return hotelDto;
        }

        /// <summary>
        /// Returns a list of all of the hotels in the Hotels database table,
        /// and all of the rooms, room amenities, and amenities associated with each hotel.
        /// </summary>
        /// <returns>A list of all hotels and all of the rooms, room amenities, 
        /// and amenities associated with each hotel.</returns>
        public async Task<List<HotelDTO>> GetHotels()
        {
            var hotels = await _context.Hotels.Include(h => h.HotelRooms)
                                              .ThenInclude(hr => hr.Room)
                                              .ThenInclude(r => r.RoomAmenities)
                                              .ThenInclude(ra => ra.Amenity)
                                              .ToListAsync();

            List<HotelDTO> hotelDtos = new List<HotelDTO>();
            foreach (var item in hotels)
            {
                hotelDtos.Add(await GetHotel(item.Id));
            }

            return hotelDtos;
        }

        /// <summary>
        /// Updates a specific hotel in the Hotels database table,
        /// based on the hotel parameter.
        /// </summary>
        /// <param name="hotel">Unique identifier of the hotel</param>
        /// <returns>The updated hotel object</returns>
        public async Task<Hotel> Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hotel;

        }

        /// <summary>
        /// Helper method that takes a hotel  
        /// and converts it into a hotelDto.
        /// </summary>
        /// <param name="hotel">A unique hotel object</param>
        /// <returns>A hotelRoomDto object</returns>
        private HotelDTO ConvertHotelIntoDTO(Hotel hotel)
        {
            HotelDTO hotelDto = new HotelDTO
            {
                ID = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
            };

            return hotelDto;
        }

        /// <summary>
        /// Helper method that takes a hotelDto  
        /// and converts it into a hotel object.
        /// </summary>
        /// <param name="hotelDto">A unique hotelDto object</param>
        /// <returns>A hotel object</returns>
        private Hotel ConvertDTOIntoHotel(HotelDTO hotelDto)
        {
            Hotel hotel = new Hotel
            {
                Id = hotelDto.ID,
                Name = hotelDto.Name,
                StreetAddress = hotelDto.StreetAddress,
                City = hotelDto.City,
                State = hotelDto.State,
                Phone = hotelDto.Phone,
            };
            
            return hotel;
        }
    }
}
