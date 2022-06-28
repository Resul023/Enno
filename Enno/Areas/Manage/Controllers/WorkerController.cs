using Enno.DAL;
using Enno.Helper;
using Enno.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enno.Areas.Manage.Controllers
{
    [Area("manage")]
    public class WorkerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public WorkerController(AppDbContext context,IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            var worker = _context.Workers.ToList();
            return View(worker);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (worker.ImageFile !=null)
            {
                if (worker.ImageFile.ContentType != "image/jpeg" && worker.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile","Image format must be jpeg/png");
                }
                if (worker.ImageFile.Length >2500000)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less 2.5mb");
                }
                worker.Image = FileManager.Save(_env.WebRootPath,"upload/worker",worker.ImageFile);
            }
            else
            {
                return Content("Image is required");
            }
            
            _context.Workers.Add(worker);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int Id)
        {
            var isExists = _context.Workers.FirstOrDefault(x=>x.Id == Id);
            if (isExists == null)
            {
                return Content("This worker is not exits");
            }
            return View(isExists);
        }
        [HttpPost]
        public IActionResult Edit(Worker worker)
        {
            var isExists = _context.Workers.FirstOrDefault(x => x.Id == worker.Id);
            if (isExists == null)
            {
                return Content("This worker is not exits");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (worker.ImageFile!=null )
            {
                if (worker.ImageFile.ContentType != "image/jpeg" && worker.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Image format must be jpeg/png");
                }
                if (worker.ImageFile.Length > 2500000)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be less 2.5mb");
                }
                var newImageFile = FileManager.Save(_env.WebRootPath,"upload/worker",worker.ImageFile);
                FileManager.Delete(_env.WebRootPath, "upload/worker", isExists.Image);
                isExists.Image = newImageFile;
            }
            isExists.Fullname = worker.Fullname;
            isExists.Desc = worker.Desc;
            isExists.Position = worker.Position;
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Delete(int Id)
        {
            var isExists = _context.Workers.FirstOrDefault(x=>x.Id == Id);
            if (isExists == null)
            {
                return Content("This worker is not exists");
            }
            return View(isExists);
        }
        [HttpPost]
        public IActionResult Delete(Worker worker)
        {
            var isExists = _context.Workers.FirstOrDefault(x => x.Id == worker.Id);
            if (isExists == null)
            {
                return Content("This worker is not exists");
            }
            _context.Workers.Remove(isExists);
            _context.SaveChanges();
            return RedirectToAction("index");
        }


       
    }
}
