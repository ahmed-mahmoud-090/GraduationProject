using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Grad.Models;
using System.Threading.Tasks;
using Grad.DTO;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmbassiesController : ControllerBase
    {
        private  IRepoBase<Embasse> _embassyRepo;
        private  IRepoBase<City> _city;

        public EmbassiesController(IRepoBase<Embasse> embassyRepo, IRepoBase<City> city)
        {
            _embassyRepo = embassyRepo;
            _city = city;   
        }

        [HttpPost ("Add new emp")]
        public async Task<IActionResult> AddNewEmbassy(Embasse? embassy)
        {
            
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            embassy.Name = embassy.Name.ToUpper();
            await _embassyRepo.CreateAsync(embassy);
            return Ok("Embassy Added Succes");
        }

        [HttpGet]
        public async Task<IActionResult> LoadEmbassyById(int id)
        {
            Embasse? embassy = await _embassyRepo.GetByIdAsync(id);
            if (embassy == null)
            {
                return NotFound("Embassy not found.");
            }

            HttpContext.Session.SetInt32("EmbassyId", id);
            return Ok(embassy);
        }

        [HttpPost("UpdateEmbassy")]
        public async Task<IActionResult> UpdateEmbassy(Embasse embassy)
        {
            int id = (int)(HttpContext.Session.GetInt32("EmbassyId") ?? 0);
            Embasse? oldEmbassy = await _embassyRepo.GetByIdAsync(id);

            if (oldEmbassy == null)
            {
                return NotFound("Embassy not found.");
            }

            oldEmbassy.Name = embassy.Name.ToUpper();
            oldEmbassy.Country = embassy.Country;
            oldEmbassy.WorkingHours = embassy.WorkingHours;
            oldEmbassy.Image = embassy.Image;
            oldEmbassy.ContactInfo = embassy.ContactInfo;
            oldEmbassy.Longitude = embassy.Longitude;
            oldEmbassy.Latitude = embassy.Latitude;
            oldEmbassy.CityId = embassy.CityId;

            await _embassyRepo.UpdateAsync(oldEmbassy);
            HttpContext.Session.Remove("EmbassyId");
            return Ok("Embassy Updated Done");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmbassy(int id)
        {
            try
            {
                await _embassyRepo.DeleteAsync(id);
                return Ok(" Deleted Succes");
            }
            catch
            {
                return BadRequest("Cannot delete this Embassy.");
            }
        }

        [HttpGet("GetAllEmbassies")]
        public async Task<IActionResult> GetAllEmbassies()
        {
            List<EmbassiesWithCity> embassiesWithCity = new List<EmbassiesWithCity>();
            List<Embasse> embasses =(List<Embasse>)await _embassyRepo.GetAsyncAll();

            foreach (var i in embasses)
            {
                City? city = (City)await _city.GetByIdAsync(i.CityId);
                EmbassiesWithCity embasse = new EmbassiesWithCity(i.Id, i.Name, city.Name, i.Country);
                embassiesWithCity.Add(embasse); 
            }
            return Ok(embassiesWithCity);
        }
    }
}
