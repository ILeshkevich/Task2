using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CSVParser.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Models;
using Site.Models.ViewModels;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbWorker db;

        public HomeController(ILogger<HomeController> logger, DbWorker worker)
        {
            _logger = logger;
            db = worker;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(PeriodViewModel model)
        {
            if(model.StartDate>model.FinishDate)
            {
                ModelState.AddModelError(String.Empty, "Start date should be < then finish date.");
                return View(model);
            }


            return View("Table",db.Select(model.StartDate,model.FinishDate,model.Format));
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
