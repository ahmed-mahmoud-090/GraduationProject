using Grad.DTO;
using Grad.Models;
using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nito.Collections;
using System.Collections.Generic;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private IRepoBase<Bank> _BankRepo;
        private IRepoBase<City> _city;
        private readonly AppDbContext _appDbContext;

        public CityController(IRepoBase<Bank> BankRepo, IRepoBase<City> city,AppDbContext appDbContext)
        {
            _BankRepo = BankRepo;
            _city = city;
            _appDbContext = appDbContext;   
        }

        [HttpPost("{Name:alpha}")]
        public async  Task<IActionResult> AddNewCity([FromRoute]City ?city)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            city.Name= city.Name.ToUpper();
            _city.CreateAsync(city);
            return Ok("City ADD ");
        }
        [HttpGet]
        public async Task<IActionResult> LoadCityById(int id)
        {

             City?  city =(City)await _city.GetByIdAsync(id);
            // City? xxx = _appDbContext.Cities.Include(c => c.Banks).FirstOrDefault(c => c.Id == id);


           /* CityWithAll lstcityWithAll = (CityWithAll)_appDbContext.Cities.Include(c => c.Banks).
                Select(e => new CityWithAll()
                {
                    Id = e.Id,
                    CityName = e.Name,
                    Banks = (List<Bank>)_appDbContext.Banks.Select(e=>new BankWithCity()
                    {
                        Id = e.Id,
                        BankName = e.Name,
                    })
                });*/
            HttpContext.Session.SetInt32("CityId",id);
            return Ok(city);

        }
        private async Task< List<Bank>> lstbanAsync(int id )
        {
            List<Bank>? lstbank = (List<Bank>) await _BankRepo.GetAsyncAll();
            lstbank = lstbank.Where(x => x.CityId == id).ToList();
            return lstbank;
        }
        [HttpPost("Update City")]
        public async Task< IActionResult> UpdateCity(City city)
        {
            int id =(int)(HttpContext.Session.GetInt32("CityId"));
            City? oldCity = await _city.GetByIdAsync(id);
            
            oldCity.Name= city.Name.ToUpper();
            _city.UpdateAsync(oldCity);
            HttpContext.Session.Remove("CityId");
            return Ok("Updated Done");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCity(int city)
        {
            try
            {

                await _city.DeleteAsync(city);
                return Ok("Deleted");
            }
            catch 
            {
                return BadRequest("Canot Delete This City ");
            }
        }
        [HttpGet("Get All City")]
        public async Task<IActionResult> GetAllCity()
        {
            List<City> lstcity = (List<City>)await _city.GetAsyncAll();
            List<Bank> lstbank = (List<Bank>)await _BankRepo.GetAsyncAll();
            List<CityWithAll> lstcityWithAll = new List<CityWithAll>();
           
            foreach (var item in lstcity)
            {
                CityWithAll city = new CityWithAll();
                city.Id = item.Id;
                city.CityName = item.Name;
                lstbank= lstbank.Where(x => x.Id == item.Id).ToList();                              
                city.Banks= lstbank;
                lstcityWithAll.Add(city);
            }              
            return Ok(lstcityWithAll);
        }
    }
}
