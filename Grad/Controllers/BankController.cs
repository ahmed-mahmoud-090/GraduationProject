using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Grad.Models;
using Grad.Repo.Base;
using WebApplication4.Models;
using Grad.DTO;
using Microsoft.EntityFrameworkCore;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private IRepoBase<Bank> _BankRepo;
        private IRepoBase<City> _city;
        private readonly AppDbContext _appDbContext;

        public BankController(IRepoBase<Bank> BankRepo, IRepoBase<City> city,AppDbContext appDbContext)
        {
            _BankRepo = BankRepo;
            _city = city;
            _appDbContext = appDbContext;
        }

        [HttpPost("Create Bank")]
        public async Task<IActionResult> Create(Bank place)
        {
           
            if (!ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }

            place.Name = place.Name.ToUpper();
            _BankRepo.CreateAsync(place);
            return Ok("Bank added successfully.");
        }

        // Return all Banks
        [HttpGet("Get All Banks")]
        public async Task<IActionResult> GetAllBanks()
        {
            List<Bank>? places = (List<Bank>)await _BankRepo.GetAsyncAll();
            List<BankWithCity> bankWithCitylist = new List<BankWithCity>();
            foreach (var place in places)
            {
                BankWithCity bankWithCity=new BankWithCity();
                bankWithCity.Id = place.Id;
                bankWithCity.BankName = place.Name;
                bankWithCity.Image = place.Image;
                bankWithCity.Rating = place.Rating;
                bankWithCity.Longitude = place.Longitude;
                bankWithCity.Latitude = place.Latitude;
                City c= await _city.GetByIdAsync(place.CityId);
                bankWithCity.CityName =c.Name;
                bankWithCitylist.Add(bankWithCity);


            }
            return Ok(bankWithCitylist);
        }
        // Returns a Bank through id

        [HttpGet]
        public async Task<IActionResult> LoadCityById(int id)
        {
            Bank? place = await _BankRepo.GetByIdAsync(id);
            City city2 =await _city.GetByIdAsync(id);
            var bankWithCity = _appDbContext.Banks.Include(e=>e.city).Select(e=>new BankWithCity { Id=e.Id, BankName = e.Name,Image=e.Image,Rating=e.Rating,Longitude=e.Longitude,Latitude=e.Latitude,CityName=e.city.Name}).Where(x=>x.Id==id);
            HttpContext.Session.SetInt32("BankId", id);
            return Ok(bankWithCity);

        }
        [HttpPost("UpdateBankById")]
        public async Task<IActionResult> Update([FromBody] Bank updatedPlace)
        {
            int? sessionId = HttpContext.Session.GetInt32("BankId");
            if (sessionId == null)
            {
                return  Ok("Bank ID not found in session.");
            }
            int id = sessionId.Value;
            Bank? oldBank = await _BankRepo.GetByIdAsync(id);
            if (oldBank == null)
            {
                return Ok($"No bank found with ID {id}");
            }
            oldBank.Name = updatedPlace.Name.ToUpper();
            oldBank.Image = updatedPlace.Image;
            oldBank.Rating = updatedPlace.Rating;
            oldBank.ScialMedia = updatedPlace.ScialMedia;
            oldBank.Longitude = updatedPlace.Longitude;
            oldBank.Latitude = updatedPlace.Latitude;
            oldBank.CityId = updatedPlace.CityId;

            await _BankRepo.UpdateAsync(oldBank);
            HttpContext.Session.Remove("BankId");
            return Ok("Update completed successfully");
        }
      
                 [HttpDelete]
                public async Task<IActionResult> DeleteBank(int bank)
                {
                    try
                    {

                        await _BankRepo.DeleteAsync(bank);
                        return Ok("Deleted");
                    }
                    catch
                    {
                        return Ok("Canot Delete This Bank ");
                    }
                }
               
        
    }
}
