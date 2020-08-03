using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab12_Relational_DB.Data;
using Lab12_Relational_DB.Model;
using Lab12_Relational_DB.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Lab12_Relational_DB.Model.DTOs;

namespace Lab12_Relational_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;
        private readonly IRoom _room;
        private readonly IAmenity _amenity;

        public HotelRoomsController(IHotelRoom hotelRoom, IRoom room, IAmenity amenity)
        {
            _hotelRoom = hotelRoom;
            _room = room;
            _amenity = amenity;
        }

        // GET: api/Hotels/4/Rooms
        [HttpGet, Route("/api/Hotels/{hotelId}/Rooms")]
        [Authorize(Policy = "BronzePrivileges")]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms(int hotelId)
        {
            return  await _hotelRoom.GetHotelRooms(hotelId);
        }


        // GET: api/Hotels/5/Rooms/{roomNumber}
        [HttpGet("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        [Authorize(Policy = "BronzePrivileges")]
        public async Task<ActionResult<HotelRoomDTO>> GetSingleHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoomDTO hotelRoomDto = await _hotelRoom.GetSingleHotelRoom(hotelId, roomNumber);

            if (hotelRoomDto == null)
            {
                return NotFound();
            }

            return hotelRoomDto;
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        [Authorize(Policy = "BronzePrivileges")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoomDto)
        {
            if (hotelId != hotelRoomDto.HotelID || roomNumber != hotelRoomDto.RoomNumber)
            {
                return BadRequest();
            }

            //update
            await _hotelRoom.Update(hotelRoomDto);

            return NoContent();
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost, Route("/api/Hotels/{hotelId}/Rooms")]
        [Authorize(Policy = "SilverPrivileges")]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(HotelRoomDTO hotelRoomDto, int hotelId)
        {
            await _hotelRoom.Create(hotelRoomDto, hotelId);

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoomDto.HotelID, roomId = hotelRoomDto.RoomID}, hotelRoomDto);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        [Authorize(Policy = "GoldPrivileges")]
        public async Task<ActionResult<HotelRoomDTO>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _hotelRoom.Delete(roomNumber, hotelId);

            return NoContent();
        }
    }
}
