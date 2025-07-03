using Grad.Models;
using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntertainmentPlaceController : ControllerBase
    {
        private IRepoBase<EntertainmentPlace> _entertainmentPlace;

        public EntertainmentPlaceController(IRepoBase<EntertainmentPlace>? entertainmentPlace)
        {
            _entertainmentPlace = entertainmentPlace;
        }

        [HttpPost("Create EntertainmentPlace")]
        public async Task<IActionResult> Create([FromBody] EntertainmentPlace entertainmentPlace)
        {
            if (!ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }

            EntertainmentPlace? existingPlace = await _entertainmentPlace.GetAsyncByParameter(entertainmentPlace.Name);
            if (existingPlace != null)
            {
                return Ok("Place already exists with this name or ID.");
            }
            entertainmentPlace.Name = entertainmentPlace.Name.ToUpper();
            await _entertainmentPlace.CreateAsync(entertainmentPlace);
            return Ok("Bank added successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> LoadEntertainmentPlaceById(int id)
        {
            EntertainmentPlace? place = await _entertainmentPlace.GetByIdAsync(id);
            if (place == null)
            {
                return Ok("Entertainment place not found.");
            }


            HttpContext.Session.SetInt32("EntertainmentPlaceId", id);
            return Ok(place);
        }

        [HttpPost("Update Entertainment Place")]
        public async Task<IActionResult> UpdateEntertainmentPlace([FromBody] EntertainmentPlace? place)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Not Valid Update");
                return BadRequest("Not Valid Update");
            }


            int? id = HttpContext.Session.GetInt32("EntertainmentPlaceId");
            if (id == null)
            {
                return BadRequest("Entertainment place ID not found in session.");
            }


            EntertainmentPlace? oldPlace = await _entertainmentPlace.GetByIdAsync(id.Value);
            if (oldPlace == null)
            {
                return NotFound("Entertainment place not found.");
            }

            oldPlace.Name = place.Name.ToUpper();
            oldPlace.OpiningHour = place.OpiningHour;
            oldPlace.PlaceType = place.PlaceType;
            oldPlace.ContactInfo = place.ContactInfo;
            oldPlace.Longitude = place.Longitude;
            oldPlace.Latitude = place.Latitude;
            oldPlace.CityId = place.CityId;


            _entertainmentPlace.UpdateAsync(oldPlace);


            HttpContext.Session.Remove("EntertainmentPlaceId");

            return Ok("Entertainment place updated successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEntertainmentPlace(int placeId)
        {
            try
            {
                await _entertainmentPlace.DeleteAsync(placeId);
                return Ok("Entertainment place deleted successfully.");
            }
            catch
            {
                return BadRequest("Cannot delete this entertainment place.");
            }
        }

        [HttpGet("Get All Entertainment Places")]
        public async Task<IActionResult> GetAllEntertainmentPlaces()
        {

            return Ok(await _entertainmentPlace.GetAsyncAll());
        }
    }
}
