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
    [Authorize(Policy = "GoldPrivileges")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Policy = "DistrictManagerPrivileges")]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            
            return await _hotel.GetHotels();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            HotelDTO hotelDto = await _hotel.GetHotel(id);
            return hotelDto;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }
            var updatedHotel = await _hotel.Update(hotel);

            return Ok(updatedHotel);
        }

        // POST: api/Hotels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HotelDTO>> PostHotel(HotelDTO hotelDto)
        {
            await _hotel.Create(hotelDto);

            return CreatedAtAction("GetHotel", new { id = hotelDto.ID }, hotelDto);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            await _hotel.Delete(id);
            return NoContent();

        }
    }
}
