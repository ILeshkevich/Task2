using System.IO;
using System.Linq;
using CSVParser.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Site.Controllers
{
    public class FilesController : Controller
    {

        private readonly ILogger<FilesController> logger;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly DbWorker db;

        public FilesController(ILogger<FilesController> logger, DbWorker worker, IHostingEnvironment environment)
        {
            db = worker;
            this.logger = logger;
            hostingEnvironment = environment;
        }

        [HttpPost]
        public IActionResult Delete(string name)
        {
            var files = Directory.GetFiles(Path.Combine(hostingEnvironment.WebRootPath, "uploads"));
            var file = files.FirstOrDefault(f => f.Contains(name));

            if (file != null)
            {
                System.IO.File.SetAttributes(file, FileAttributes.Normal);
                System.IO.File.Delete(file);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "File not found");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Open(string name)
        {
            var files = Directory.GetFiles(Path.Combine(hostingEnvironment.WebRootPath, "uploads"));
            var file = files.FirstOrDefault(f => f.Contains(name));
            db.CreateTable();

            var result = db.Insert(file);

            if (result)
            {
                result = db.ToStar();
                if (result)
                {
                    return RedirectToAction("Index", "Database");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
