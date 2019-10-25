using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSVParser.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Models.ViewModels;

namespace Site.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly ILogger<DatabaseController> logger;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly DbWorker db;

        public DatabaseController(ILogger<DatabaseController> logger, DbWorker worker, IHostingEnvironment environment)
        {
            db = worker;
            this.logger = logger;
            hostingEnvironment = environment;
        }

        [HttpGet]
        public IActionResult Range()
        {
            return View(new PeriodViewModel());
        }

        [HttpPost]
        public IActionResult Table(PeriodViewModel model)
        {
            if (model.StartDate > model.FinishDate)
            {
                ModelState.AddModelError(string.Empty, "Start date should be less then finish date.");
                return View(model);
            }

            return View("Table", db.Select(model.StartDate, model.FinishDate, model.Format, model.Type));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateStar()
        {
            var result = db.ToStar();
            if (result)
            {
                ModelState.AddModelError(string.Empty, "Star created");
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTable()
        {
            db.DeleteTable();
            return RedirectToAction("Index", "Home");
        }
    }
}