using GarduationDashbord.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarduationDashbord.Controllers
{
    public class Type_placeController : Controller
    {
        private readonly AppDbContext _repoBase;
        public Type_placeController(AppDbContext repoBase)
        {
            _repoBase = repoBase;

        }

        public IActionResult Index()
        {

            return View(_repoBase.types.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Type_place city)
        {
            _repoBase.types.Add(city);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {

            return View(_repoBase.types.Find(id));
        }
        [HttpPost]
        public IActionResult Update(Type_place type_Place)
        {
            Type_place old= _repoBase.types.Find(type_Place.Id);
            old.Name= type_Place.Name;
            _repoBase.types.Update(old);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Type_place user = _repoBase.types.Find(id);
            _repoBase.types.Remove(user);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
