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
using System.Security.Cryptography.X509Certificates;
using SQLitePCL;
using Microsoft.AspNetCore.Authorization;
using Lab12_Relational_DB.Model.DTOs;

namespace Lab12_Relational_DB.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        // GET: api/Rooms
        [HttpGet]
        [Authorize(Policy = "SilverPrivileges")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            return await _room.GetRooms();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [Authorize(Policy = "SilverPrivileges")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            RoomDTO roomDto = await _room.GetRoom(id);
            return roomDto;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "SilverPrivileges")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO roomDto)
        {
            if (id != roomDto.ID)
            {
                return BadRequest();
            }

            var updatedRoom = await _room.Update(roomDto);
            return Ok(updatedRoom);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = "GoldPrivileges")]
        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO roomDto)
        {
            await _room.Create(roomDto);

            return CreatedAtAction("GetRoom", new { id = roomDto.ID }, roomDto);
        }

        [HttpPost]
        [Authorize(Policy = "BronzePrivileges")]
        [Route("{roomId}/Amenity/{AmenityId}")]
        //POST: {roomId}/Amenity/{AmenityId}
        // Model Binding
        public async Task<IActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            await _room.AddAmenityToRoom(roomId, amenityId);
            return Ok();
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "GoldPrivileges")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            await _room.Delete(id);
            return NoContent();
        }

        // DELETE An Amenity from room
        [HttpDelete]
        [Authorize(Policy = "BronzePrivileges")]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            await _room.RemoveAmenityFromRoom(roomId, amenityId);
            return Ok();
        }


    }
}
