using GarduationDashbord.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarduationDashbord.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _repoBase;
        public UserController(AppDbContext repoBase)
        {
            _repoBase = repoBase;
        }
        public IActionResult Index()
        {
            return View(_repoBase.Users.ToList());
        }

        public IActionResult Update(int id)
        {
            var user = _repoBase.Users.Find(id);

            if (user == null)
            {
                return NotFound(); 
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            User existingUser = _repoBase.Users.Find(user.Id);

            if (existingUser == null)
            {
                return NotFound();
            }

           
            existingUser.FirstName = user.FirstName;
            existingUser.SacondName = user.SacondName;
            existingUser.Email = user.Email;
            existingUser.City = user.City;
            existingUser.BDate = user.BDate;
            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = user.Password;
            }

            if (user.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(user.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    user.ImageFile.CopyTo(fileStream);
                }

                existingUser.ImageBytes = "/images/" + uniqueFileName;
            }

            _repoBase.Users.Update(existingUser);
            _repoBase.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            User user = _repoBase.Users.Find(id);
            _repoBase.Users.Remove(user);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Search(string? query)
        {

            List<User>? res= _repoBase.Users
                .Where(u => u.FirstName.Contains(query) || u.SacondName.Contains(query))
                 .ToList();
            return View(res);
        }

    }
}
