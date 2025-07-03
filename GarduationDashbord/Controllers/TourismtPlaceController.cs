using GarduationDashbord.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarduationDashbord.Controllers
{
    public class TourismtPlaceController : Controller
    {
        private readonly AppDbContext _repoBase;
        public TourismtPlaceController(AppDbContext repoBase)
        {
            _repoBase = repoBase;
        }
        public IActionResult Index()
        {
            return View(_repoBase.Tourismt_Places.ToList());
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        public IActionResult Create(Tourismt_Place em)
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
            _repoBase.Tourismt_Places.Add(em);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Update(int id)
        {
            Tourismt_Place Banks = _repoBase.Tourismt_Places.Find(id);

            if (Banks == null)
            {
                return NotFound();
            }

            return View(Banks);
        }

        [HttpPost]
        public IActionResult Update(Tourismt_Place Bank)
        {
            if (!ModelState.IsValid)
            {
                return View(Bank);
            }

            Tourismt_Place existingUser = _repoBase.Tourismt_Places.Find(Bank.Id);

            if (existingUser == null)
            {
                return NotFound();
            }


            existingUser.Name = Bank.Name;
            existingUser.TicketPrice = Bank.TicketPrice;
            existingUser.CityId = Bank.CityId;
            existingUser.Rating = Bank.Rating;
            existingUser.Discription = Bank.Discription;
            existingUser.Image = Bank.Image;
            existingUser.Typeofplaceid = Bank.Typeofplaceid;
           
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

            _repoBase.Tourismt_Places.Update(existingUser);
            _repoBase.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Tourismt_Place user = _repoBase.Tourismt_Places.Find(id);
            _repoBase.Tourismt_Places.Remove(user);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Search(string? query)
        {

            List<Tourismt_Place>? res = _repoBase.Tourismt_Places
            .Where(u => u.Name.Contains(query) || u.Discription.Contains(query) )
            .ToList();
            return View(res);
        }

    }
}
