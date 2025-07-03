using GarduationDashbord.Models;
using Microsoft.AspNetCore.Mvc;

namespace GarduationDashbord.Controllers
{
    public class CityController : Controller
    {
        private readonly AppDbContext _repoBase;
        public CityController(ILogger<HomeController> logger, AppDbContext repoBase)
        {
            _repoBase = repoBase;

        }

        public IActionResult Index()
        {
            return View(_repoBase.Cities.ToList());
        }

        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(City city)
        {
            _repoBase.Cities.Add(city);
            _repoBase.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
