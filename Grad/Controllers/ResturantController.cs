using Grad.DTO;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication4.Models;

namespace Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResturantController : ControllerBase
    {
        private IRepoBase<Restaurant> _restaurant;
        private IRepoBase<City> _city;

        public ResturantController(IRepoBase<Restaurant> restaurant, IRepoBase<City> city)
        {
            _restaurant = restaurant;
            _city = city;
        }

        [HttpPost("Create Restaurant")]
        public async Task<IActionResult> Create([FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return Ok("Invalid data.");
            }

            
            restaurant.Name = restaurant.Name.ToUpper();
            await _restaurant.CreateAsync(restaurant);
            return Ok("Restaurant added successfully.");
        }


        [HttpGet]
        public async Task<IActionResult> LoadRestaurantById(int id)
        {
            Restaurant? restaurant = await _restaurant.GetByIdAsync(id);
            if (restaurant == null)
            {
                return Ok("Restaurant not found.");
            }
            HttpContext.Session.SetInt32("RestaurantId", id);
            return Ok(restaurant);
        }

        [HttpPost("Update Restaurant")]
        public async Task<IActionResult> UpdateRestaurant([FromBody] Restaurant? restaurant)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Not Valid Update");
                return BadRequest("Not Valid Update");
            }


            int? id = HttpContext.Session.GetInt32("RestaurantId");
            if (id == null)
            {
                return Ok("Restaurant ID not found in session.");
            }


            Restaurant? oldRestaurant = await _restaurant.GetByIdAsync(id.Value);
            if (oldRestaurant == null)
            {
                return Ok("Restaurant not found.");
            }

            oldRestaurant.Name = restaurant.Name.ToUpper();
            oldRestaurant.OpiningHour = restaurant.OpiningHour;
            oldRestaurant.TypeOfFood = restaurant.TypeOfFood;
            oldRestaurant.Image = restaurant.Image;
            oldRestaurant.Rating = restaurant.Rating;
            oldRestaurant.ScialMedia = restaurant.ScialMedia;
            oldRestaurant.PhoneNumber = restaurant.PhoneNumber;
            oldRestaurant.Longitude = restaurant.Longitude;
            oldRestaurant.Latitude = restaurant.Latitude;
            oldRestaurant.CityId = restaurant.CityId;


            _restaurant.UpdateAsync(oldRestaurant);

            HttpContext.Session.Remove("RestaurantId");

            return Ok("Restaurant updated successfully.");
        }

        [HttpDelete("Delete Restaurant")]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            try
            {
                await _restaurant.DeleteAsync(restaurantId);
                return Ok("Restaurant deleted successfully.");
            }
            catch
            {
                return BadRequest("Cannot delete this restaurant.");
            }
        }
        [HttpGet("Get All Restaurants")]
        public async Task<IActionResult> GetAllRestaurants([FromQuery] bool? orderByRating)
        {
            var restaurants = await _restaurant.GetAsyncAll();
            List<RestaurentWithCity> lst = new List<RestaurentWithCity>();
            foreach(var I in  restaurants)
            {
                City? city = await _city.GetByIdAsync((int)I.CityId);
                RestaurentWithCity restaurentWithCity=new RestaurentWithCity(I.Id, I.Name, I.OpiningHour, I.TypeOfFood, (int)I.Rating, I.PhoneNumber, city.Name);
                lst.Add(restaurentWithCity);
            }
            if (orderByRating.HasValue && orderByRating.Value)
            {
                lst = lst.OrderByDescending(r => r.RestaurentRating).ToList();
            }

            return Ok(lst);
        }
    }
}
