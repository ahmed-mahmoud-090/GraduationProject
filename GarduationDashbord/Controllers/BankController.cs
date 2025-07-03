using GarduationDashbord.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarduationDashbord.Controllers
{
    public class BankController : Controller
    {
        private readonly AppDbContext _repoBase;
        public BankController(AppDbContext repoBase)
        {
            _repoBase = repoBase;
        }
        public IActionResult Index()
        {
            return View(_repoBase.Banks.ToList());
        }

        public IActionResult Update(int id)
        {
            Bank Banks = _repoBase.Banks.Find(id);

            if (Banks == null)
            {
                return NotFound();
            }

            return View(Banks);
        }

        [HttpPost]
        public IActionResult Update(Bank Bank)
        {
            if (!ModelState.IsValid)
            {
                return View(Bank);
            }

            Bank existingUser = _repoBase.Banks.Find(Bank.Id);

            if (existingUser == null)
            {
                return NotFound();
            }


            existingUser.Name = Bank.Name;
            existingUser.Rating = Bank.Rating;
            existingUser.CityId = Bank.CityId;
            existingUser.Image = Bank.Image;
           
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

            _repoBase.Banks.Update(existingUser);
            _repoBase.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Bank user = _repoBase.Banks.Find(id);
            _repoBase.Banks.Remove(user);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Create(Bank bank)
        {

            if (bank.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(bank.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    bank.ImageFile.CopyTo(fileStream);
                }

                bank.Image = "/images/" + uniqueFileName;
            }
            if (!ModelState.IsValid) {
                return View(bank);
            }
            _repoBase.Banks.Add(bank);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");


        }
        public IActionResult Search(string? query)
        {

            List<Bank>? res = _repoBase.Banks
            .Where(u => u.Name.Contains(query) || u.ScialMedia.Contains(query))
            .ToList();
            return View(res);
        }


    }
}
