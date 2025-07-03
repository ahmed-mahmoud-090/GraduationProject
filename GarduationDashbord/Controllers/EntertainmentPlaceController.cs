using GarduationDashbord.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarduationDashbord.Controllers
{
    public class EntertainmentPlaceController : Controller
    {
        private readonly AppDbContext _repoBase;
        public EntertainmentPlaceController(AppDbContext repoBase)
        {
            _repoBase = repoBase;
        }
        public IActionResult Index()
        {
            return View(_repoBase.EntertainmentPlaces.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EntertainmentPlace em)
        {

            if (!ModelState.IsValid) { 
            return View(em);    
            }
            _repoBase.EntertainmentPlaces.Add(em);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Update(int id)
        {
            EntertainmentPlace Banks = _repoBase.EntertainmentPlaces.Find(id);

            if (Banks == null)
            {
                return NotFound();
            }

            return View(Banks);
        }

        [HttpPost]
        public IActionResult Update(EntertainmentPlace Bank)
        {
            if (!ModelState.IsValid)
            {
                return View(Bank);
            }

            EntertainmentPlace existingUser = _repoBase.EntertainmentPlaces.Find(Bank.Id);

            if (existingUser == null)
            {
                return NotFound();
            }


            existingUser.Name = Bank.Name;
            existingUser.OpiningHour = Bank.OpiningHour;
            existingUser.CityId = Bank.CityId;
            existingUser.ContactInfo = Bank.ContactInfo;
            existingUser.PlaceType = Bank.PlaceType;

            
            _repoBase.EntertainmentPlaces.Update(existingUser);
            _repoBase.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            EntertainmentPlace user = _repoBase.EntertainmentPlaces.Find(id);
            _repoBase.EntertainmentPlaces.Remove(user);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
