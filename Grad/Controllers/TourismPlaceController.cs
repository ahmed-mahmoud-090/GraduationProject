using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourismPlaceController : ControllerBase
    {
        private readonly IRepoBase<Tourismt_Place> _placeRepo;

        public TourismPlaceController(IRepoBase<Tourismt_Place> placeRepo)
        {
            _placeRepo = placeRepo;
        }

        [HttpPost("Create Tourismt_Place")]
        public async Task<IActionResult> Create([FromBody] Tourismt_Place place)
        {
            if (!ModelState.IsValid)
            {
                return Ok("Invalid data");
            }
            
            place.Name=place.Name.ToUpper();
            _placeRepo.CreateAsync(place);
            return Ok("Place added successfully.");
        }
        // Return all Tourism Places
        [HttpGet("Get All Tourism Places")]
        public async Task<IActionResult> GetAllTourismPlaces()
        {
            var places = await _placeRepo.GetAsyncAll();
            return Ok(places);
        }

        // Returns a Tourism place through id

        [HttpGet("Get Tourism Place through id")]
        public async Task<IActionResult> LoadTourismById(int id)
        {
            Tourismt_Place? place = await _placeRepo.GetByIdAsync(id);
            HttpContext.Session.SetInt32("TourismPlaceId", id);
            return Ok(place);

        }
        [HttpPost("Update TourismPlace")]
        public async Task<IActionResult> UpdateTourismPlace(Tourismt_Place TOplace)
        {
            int id = (int)(HttpContext.Session.GetInt32("TourismPlaceId"));
            Tourismt_Place? oldTourismt = await _placeRepo.GetByIdAsync(id);

            oldTourismt.Name = TOplace.Name;
            oldTourismt.Discription = TOplace.Discription;
            oldTourismt.TicketPrice = TOplace.TicketPrice;
            oldTourismt.Image = TOplace.Image;
            oldTourismt.Rating = TOplace.Rating;
            oldTourismt.Longitude = TOplace.Longitude;
            oldTourismt.Latitude = TOplace.Latitude;
            oldTourismt.CityId = TOplace.CityId;

            await _placeRepo.UpdateAsync(oldTourismt); 
            HttpContext.Session.Remove("TourismPlaceId");
            return Ok("Updated Done");
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteTourismPlace(int TourismPlace)
        {
            try
            {

                await _placeRepo.DeleteAsync(TourismPlace);
                return Ok("Deleted");
            }
            catch
            {
                return BadRequest("Canot Delete This TourismPlace ");
            }
        }
        

    }
}
