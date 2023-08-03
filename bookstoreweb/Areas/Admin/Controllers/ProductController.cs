using bookstore.DataAccess.Repository;
using bookstore.DataAccess.Repository.IRepository;
using bookstore.Models;
using bookstore.Models.ViewModels;
using bookstore.Utility;
using bookstore.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;

namespace bookstoreweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> ObjProductList = _unitofwork.Product.GetAll(includeProperties:"Category").ToList();
            return View(ObjProductList);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = _unitofwork.Category.GetAll()
            //    .Select(u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.CategoryId.ToString()
            //    }

            //) ;

            //ViewBag.CategoryList = CategoryList;

            ProductVM ProductVM = new ProductVM
            {
                Product = new Product(),
                CategoryList = _unitofwork.Category.GetAll()
                    .Select(u =>
                    {
                        return new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.CategoryId.ToString()
                        };
                    })
            };
            if (id == null| id == 0)
            {
                //create
                return View(ProductVM);
            }
            else
            {
                //update
                ProductVM.Product = _unitofwork.Product.Get(u => u.ProductId == id);
                return View(ProductVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM,IFormFile file)
        {

                if (ModelState.IsValid)
                {
                    string wwwRootpath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = Path.Combine(wwwRootpath, @"images\product");

                        if (!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                        {
                            var oldImagePath = Path.Combine(wwwRootpath, ProductVM.Product.ImageUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);

                            }
                        }

                        using (var filestream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                        {
                            file.CopyTo(filestream);
                        }

                        ProductVM.Product.ImageUrl = @"\images\product\" + filename;

                    }

                    if (ProductVM.Product.ProductId == 0)
                    {
                        _unitofwork.Product.Add(ProductVM.Product);
                    }
                    else
                    {
                        _unitofwork.Product.update(ProductVM.Product);
                    }
                    _unitofwork.Save();
                    return RedirectToAction("Index");

                }
                else
                {
                    ProductVM.CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.CategoryId.ToString()

                    });
                }
                return View(ProductVM);

        }
   

        [HttpPost, ActionName("Delete")]

        public IActionResult Delete(int id)
        {
            Product? obj = _unitofwork.Product.Get(u => u.ProductId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitofwork.Product.Remove(obj);
            _unitofwork.Save();

            return RedirectToAction("Index");
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> ObjProductList = _unitofwork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = ObjProductList});
        }

        public IActionResult Delet(int? id)
        {
            var obj = _unitofwork.Product.Get(u => u.ProductId == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "error" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);

            }

            _unitofwork.Product.Remove(obj);
            _unitofwork.Save();

            return Json(new {success = true,message = "success"});  
        }
        #endregion

    }
}
