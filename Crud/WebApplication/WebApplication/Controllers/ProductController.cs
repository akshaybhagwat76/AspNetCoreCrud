using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();

        private IHostingEnvironment _hostingEnvironment;

        public ProductController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        [Route("")]
        [Route("index")]
        [Route("~/")]


        public IActionResult Index()
        {
            ViewBag.lstProducts = db.Product.ToList();
            return View();
        }
        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {
            return View("Add", new Product());
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile photo)
        {
            if (photo == null || photo.Length== 0)
            {
                product.Photo = "no-image.jpg";
            }
            else
            {
                if (photo.Length > 0)
                {
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    var filePath = Path.Combine(uploads, photo.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(fileStream);
                        product.Photo = photo.FileName;
                    }
                }
            }
            db.Product.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("Edit/{id?}")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Product product, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                product.Photo = db.Product.Find(product.Id).Photo;
            }
            else
            {
                if (photo.Length > 0)
                {
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    var filePath = Path.Combine(uploads, photo.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(fileStream);
                        product.Photo = photo.FileName;
                    }
                }
            }
            db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int Id)
        {
            db.Remove(db.Product.Find(Id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View("Edit", db.Product.Find(id));
        }

    }
}