using GarduationDashbord.Models;
using GarduationDashbord.Repo.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GarduationDashbord.Controllers
{
   public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Bdata { get; set; }
        public string Image { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger,AppDbContext repoBase)
        {
            _context = repoBase;
            
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Person> list = new List<Person>();
                list.AddRange(new List<Person>
                {
                    new Person { Name = "Ahmed Abd Elmnem",Role ="BackEnd Developer",Bdata="23-10-2003",Image="/images/1d41dd16-7816-4a6a-99d1-086609e0f8fd_Screenshot 2024-09-13 012009.png" },
                    new Person { Name = "Ahmed Helal Shokry",Role ="BackEnd Developer",Bdata="23-10-2001", Image = "/images/Helo.png" },
                    new Person { Name = "Ahmed Abd Elmnem",Role ="BackEnd Developer",Bdata="23-10-2003",Image="/images/1d41dd16-7816-4a6a-99d1-086609e0f8fd_Screenshot 2024-09-13 012009.png" },
                  
                });

                return View(list);
        }
        public IActionResult GetChartData()
        {
            var data = new
            {
                labels = new[] { "1", "2", "3", "4", "5", "6" },
                datasets = new[]
                {
                new { label = "Category 1", backgroundColor = "#ff6384", data = new[] { 5000, 7000, 6500, 7200, 8000, 9000 } },
                new { label = "Category 2", backgroundColor = "#9966ff", data = new[] { 4000, 6000, 5500, 6900, 7500, 8500 } },
                new { label = "Category 3", backgroundColor = "#36a2eb", data = new[] { 3000, 5000, 4500, 6200, 7000, 7800 } }
              }
            };
            return Json(data);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
