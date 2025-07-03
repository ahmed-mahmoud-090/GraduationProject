using Grad.Models;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistnationController : ControllerBase
    {
        private IRepoBase<City> _city;
        private IRepoBase<Bank> _bank;
        private IRepoBase<Embasse> _embasse;
        private IRepoBase<EntertainmentPlace> _entertainmentPlace;
        private IRepoBase<Hotel> _hotel;
        private IRepoBase<Restaurant> _restaurant;
        private IRepoBase<Tourismt_Place> _tourismt_place;
        private IRepoBase<TransportProvider> _transprovider;

        public DistnationController(IRepoBase<City> city, IRepoBase<Bank> Bank, IRepoBase<Embasse> embasse, IRepoBase<EntertainmentPlace> Ent, IRepoBase<Hotel> hotl, IRepoBase<Restaurant> rest, IRepoBase<Tourismt_Place> Tp, IRepoBase<TransportProvider> TPR)
        {
            _city = city;
            _bank = Bank;
            _embasse = embasse;
            _entertainmentPlace = Ent;
            _hotel = hotl;
            _transprovider = TPR;
            _tourismt_place = Tp;
            _restaurant = rest;

        }
        [HttpGet("Tack one city from list")]
        public async Task<List<City>> AllCityForDistination()
        {

            List<City>? cities = (List<City>)await _city.GetAsyncAll();
            return cities;        
        }
         [HttpPost("Your City For Distinaton")]
        public async Task<bool> DistinationCity(int id)
        {
            HttpContext.Session.SetInt32("Cityid",id);
            return true;
        }

        [HttpGet("Top Reating Of Bank")]
        public async Task<List<Bank>> TopReatingBank(int reat)
        {
            List<Bank>? banks = (List<Bank>?)await _bank.GetAsyncAll();

            // التحقق من وجود معرف المدينة في الجلسة
            int? cityId = HttpContext.Session.GetInt32("Cityid");
            if (cityId == null || banks == null)
            {
                return new List<Bank>(); // إرجاع قائمة فارغة عند وجود مشكلة
            }

            // التصفية والترتيب
            banks = banks
                .Where(x => x.CityId == cityId&&x.Rating==reat)
                .OrderByDescending(x => x.Rating) // الترتيب تنازلي حسب التقييم
                .ToList();

            return banks;
        }
        [HttpGet("Top Hotel")]
        public async Task<List<Hotel>> TopReatingHotel(int reat)
        {
            List<Hotel>? Hotel = (List<Hotel>)await _hotel.GetAsyncAll();
            int cityid = (int)HttpContext.Session.GetInt32("Cityid");
            Hotel = Hotel.Where(x => x.CityId == cityid&&x.Rating==reat).OrderByDescending(x => x.Rating).ToList();

            return Hotel;

        }
        [HttpGet("Top Restaurant")]
        public async Task<List<Restaurant>> TopRestaurant(int reat)
        {
            List<Restaurant>? res = (List<Restaurant>)await _restaurant.GetAsyncAll();
            int cityid = (int)HttpContext.Session.GetInt32("Cityid");
            res = res.Where(x => x.CityId == cityid&&x.Rating==reat).OrderByDescending(x => x.Rating).ToList();

            return res;

        }
         [HttpGet("Top Tourismt_Place")]
        public async Task<List<Tourismt_Place>> TopFiveTourismt_Place(int reat)
        {
            List<Tourismt_Place>? Tourismt_Place = (List<Tourismt_Place>)await _tourismt_place.GetAsyncAll();
            int cityid = (int)HttpContext.Session.GetInt32("Cityid");

            Tourismt_Place = Tourismt_Place.Where(x => x.CityId == cityid&&x.Rating==reat).OrderByDescending(x => x.Rating).ToList();

            HttpContext.Session.Remove("Cityid");
            return Tourismt_Place;

        }

    }
}
