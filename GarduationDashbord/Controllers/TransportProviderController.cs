using GarduationDashbord.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarduationDashbord.Controllers
{
    public class TransportProviderController : Controller
    {
        private readonly AppDbContext _repoBase;
        public TransportProviderController(AppDbContext repoBase)
        {
            _repoBase = repoBase;
        }
        public IActionResult Index()
        {
            return View(_repoBase.transportProviders.ToList());
        }

        public IActionResult Create(TransportProvider em)
        {

            if (!ModelState.IsValid)
            {
                return View(em);
            }
            
            _repoBase.transportProviders.Add(em);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Update(int id)
        {
            TransportProvider Banks = _repoBase.transportProviders.Find(id);

            if (Banks == null)
            {
                return NotFound();
            }

            return View(Banks);
        }

        [HttpPost]
        public IActionResult Update(TransportProvider Bank)
        {
            if (!ModelState.IsValid)
            {
                return View(Bank);
            }

            TransportProvider existingUser = _repoBase.transportProviders.Find(Bank.Id);

            if (existingUser == null)
            {
                return NotFound();
            }



            existingUser.Name = Bank.Name;
            existingUser.PriceModel = Bank.PriceModel;
            existingUser.CityId = Bank.CityId;
            existingUser.ContactInfo = Bank.ContactInfo;
            existingUser.TypePlaceID = Bank.TypePlaceID;
            existingUser.ProviderType = Bank.ProviderType;
           
            _repoBase.transportProviders.Update(existingUser);
            _repoBase.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            TransportProvider user = _repoBase.transportProviders.Find(id);
            _repoBase.transportProviders.Remove(user);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
