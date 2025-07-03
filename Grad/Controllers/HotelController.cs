using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private  IRepoBase<Hotel> _hotelRepo;

        public HotelsController(IRepoBase<Hotel>? hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        // add
        [HttpPost("Add New Hotel")]
        public async Task<IActionResult> AddNewHotel([FromBody] Hotel? hotel)
        {
            if (!ModelState.IsValid)
               
                {
                    return RedirectToAction("Index", "Home");
                }

            hotel.Name = hotel.Name.ToUpper();
             _hotelRepo.CreateAsync(hotel);
            return Ok("Hotel Added");
        }

        // Get
        [HttpGet("{id:int}")]
        public async Task<IActionResult> LoadHotelById(int id)
        {
            Hotel? hotel = await _hotelRepo.GetByIdAsync(id);
            if (hotel == null)
            {
                return NotFound("Hotel not found");
            }

            HttpContext.Session.SetInt32("HotelId", id);
            return Ok(hotel);
        }

        [HttpPost("UpdateHotel")]
        public async Task<IActionResult> UpdateHotel(Hotel hotel)
        {
            /**/
            int id = (int)(HttpContext.Session.GetInt32("HotelId") );
            Hotel? oldHotel = await _hotelRepo.GetByIdAsync(id);

            if (oldHotel == null)
            {
                return NotFound("Hotel not found");
            }

            oldHotel.Name = hotel.Name.ToUpper();
           

            _hotelRepo.UpdateAsync(oldHotel);
            HttpContext.Session.Remove("HotelId");
            return Ok("Hotel Updated Done");
        }

        // Delete
        [HttpDelete]
        public async Task<IActionResult> DeleteHotel(int hotel)
        {
            try
            {
                await _hotelRepo.DeleteAsync(hotel);
                return Ok("Hotel Deleted");
            }
            catch
            {
                return BadRequest("Cannot delete this hotel");
            }
        }

        // All
        [HttpGet("GetAllHotels")]
        public async Task<IActionResult> GetAllHotels()
        {
            return Ok( await _hotelRepo.GetAsyncAll());
        }
    }
}
