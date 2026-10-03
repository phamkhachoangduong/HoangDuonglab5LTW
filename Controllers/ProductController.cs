using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HoangDuonglab5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HoangDuonglab5.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public static List<Category> Categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại & Tablet" },
            new Category { Id = 2, Name = "Laptop & Máy tính bảng" },
            new Category { Id = 3, Name = "Phụ kiện công nghệ" }
        };

        public static List<Product> Products = new List<Product>();

        public ProductController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = Categories;
            return View(Products);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageUpload != null)
                {
                    string uploadFolder = Path.Combine(_env.WebRootPath, "products");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        product.ImageUpload.CopyTo(fileStream);
                    }

                    product.Image = uniqueFileName;
                }

                product.Id = Products.Count > 0 ? Products.Max(p => p.Id) + 1 : 1;
                Products.Add(product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public IActionResult Details(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            ViewBag.CategoryName = Categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name;
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            ViewBag.CategoryId = new SelectList(Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null) return NotFound();

            if (product.ImageUpload == null)
            {
                ModelState.Remove("ImageUpload");
            }

            if (ModelState.IsValid)
            {
                if (product.ImageUpload != null)
                {
                    string uploadFolder = Path.Combine(_env.WebRootPath, "products");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        product.ImageUpload.CopyTo(fileStream);
                    }
                    existingProduct.Image = uniqueFileName;
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.SalePrice = product.SalePrice;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                Products.Remove(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}