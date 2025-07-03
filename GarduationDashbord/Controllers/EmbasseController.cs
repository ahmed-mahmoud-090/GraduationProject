using GarduationDashbord.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarduationDashbord.Controllers
{
    public class EmbasseController : Controller
    {
        private readonly AppDbContext _repoBase;
        public EmbasseController(AppDbContext repoBase)
        {
            _repoBase = repoBase;

        }

        public IActionResult Index()
        {
            return View(_repoBase.Embasses.ToList());
        }
        public IActionResult Create(Embasse em)
        {

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
            if (!ModelState.IsValid)
            {
                return View(em);
            }
            _repoBase.Embasses.Add(em);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");
            
        }

        public IActionResult Update(int id)
        {
            Embasse Banks = _repoBase.Embasses.Find(id);

            if (Banks == null)
            {
                return NotFound();
            }

            return View(Banks);
        }

        [HttpPost]
        public IActionResult Update(Embasse Bank)
        {
            if (!ModelState.IsValid)
            {
                return View(Bank);
            }

            Embasse existingUser = _repoBase.Embasses.Find(Bank.Id);

            if (existingUser == null)
            {
                return NotFound();
            }


            existingUser.Name = Bank.Name;
            existingUser.WorkingHours = Bank.WorkingHours;
            existingUser.CityId = Bank.CityId;
            existingUser.Image = Bank.Image;
            existingUser.ContactInfo = Bank.ContactInfo;
            existingUser.Country = Bank.Country;

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

            _repoBase.Embasses.Update(existingUser);
            _repoBase.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Embasse user = _repoBase.Embasses.Find(id);
            _repoBase.Embasses.Remove(user);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Search(string? query)
        {

            List<Embasse>? res = _repoBase.Embasses
            .Where(u => u.Name.Contains(query) || u.Country.Contains(query))
            .ToList();
            return View(res);
        }


    }
}
