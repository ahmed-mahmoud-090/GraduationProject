using DashBord.Models;
using Grad.Models;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DashBord.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepoBase<Grad.Models.EntertainmentPlace> _repoBase;
        public HomeController(ILogger<HomeController> logger,IRepoBase<EntertainmentPlace> repoBase)
        {
            _repoBase = repoBase;   
            _logger = logger;
        }

        public IActionResult Index()
        {
            var res = _repoBase.GetAsyncAll();

            return View(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
