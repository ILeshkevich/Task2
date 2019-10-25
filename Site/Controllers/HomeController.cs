using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSVParser.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Models;
using Site.Models.ViewModels;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly DbWorker db;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, DbWorker worker, IHostingEnvironment environment)
        {
            this.logger = logger;
            db = worker;
            hostingEnvironment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
            DirectoryInfo info = new DirectoryInfo(uploads);
            FileInfo[] fileInfos = info.GetFiles();
            return View(fileInfos);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View(new FileViewModel());
        }

        [HttpPost]
        public IActionResult Upload(FileViewModel model)
        {
            if (model.File != null)
            {
                var uniqueFileName = GetUniqueFileName(model.File.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, uniqueFileName);
                model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Upload File to continue");
            return View(new FileViewModel());
        }

        [HttpGet]
        public IActionResult Output()
        {
            return View(new PeriodViewModel());
        }

        [HttpPost]
        public IActionResult Output(PeriodViewModel model)
        {
            if (model.StartDate > model.FinishDate)
            {
                ModelState.AddModelError(string.Empty, "Start date should be < then finish date.");
                return View(model);
            }

            return View("Table", db.Select(model.StartDate, model.FinishDate, model.Format, model.Type));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
