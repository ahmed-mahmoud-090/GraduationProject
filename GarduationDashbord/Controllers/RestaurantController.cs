using GarduationDashbord.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarduationDashbord.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly AppDbContext _repoBase;
        public RestaurantController(AppDbContext repoBase)
        {
            _repoBase = repoBase;
        }
        public IActionResult Index()
        {
            return View(_repoBase.Restaurants.ToList());
        }
        
        public IActionResult Create(Restaurant em)
        {

            if (!ModelState.IsValid)
            {
                return View(em);
            }
            if (em.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(em.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    em.ImageFile.CopyTo(fileStream);
                }

                em.Image = "/images/" + uniqueFileName;
            }
            _repoBase.Restaurants.Add(em);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Update(int id)
        {
            Restaurant Banks = _repoBase.Restaurants.Find(id);

            if (Banks == null)
            {
                return NotFound();
            }

            return View(Banks);
        }

        [HttpPost]
        public IActionResult Update(Restaurant Bank)
        {
            if (!ModelState.IsValid)
            {
                return View(Bank);
            }

            Restaurant existingUser = _repoBase.Restaurants.Find(Bank.Id);

            if (existingUser == null)
            {
                return NotFound();
            }


            existingUser.Name = Bank.Name;
            existingUser.OpiningHour = Bank.OpiningHour;
            existingUser.CityId = Bank.CityId;
            existingUser.Rating = Bank.Rating;
            existingUser.PhoneNumber = Bank.PhoneNumber;
            existingUser.Image = Bank.Image;
            existingUser.ScialMedia = Bank.ScialMedia;
            existingUser.TypeOfFood = Bank.TypeOfFood;
            if (Bank.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(Bank.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Bank.ImageFile.CopyTo(fileStream);
                }

                existingUser.Image = "/images/" + uniqueFileName;
            }

            _repoBase.Restaurants.Update(existingUser);
            _repoBase.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Restaurant user = _repoBase.Restaurants.Find(id);
            _repoBase.Restaurants.Remove(user);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Search(string? query)
        {

            List<Restaurant>? res = _repoBase.Restaurants
            .Where(u => u.Name.Contains(query) || u.ScialMedia.Contains(query) || u.TypeOfFood.Contains(query))
            .ToList();
            return View(res);
        }

    }
}
