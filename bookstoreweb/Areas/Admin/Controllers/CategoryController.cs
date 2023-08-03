using bookstore.DataAccess.Repository.IRepository;
using bookstore.Models;
using bookstore.Utility;
using bookstore.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookstoreweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
            List<Category> ObjCategoriesList = _unitofwork.Category.GetAll().ToList();
            return View(ObjCategoriesList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category _obj)
        {
            if (_obj.Name == _obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order and Name must not be the same");
            }

            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(_obj);
                _unitofwork.Save();
                return RedirectToAction("Index");

            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? Target = _unitofwork.Category.Get(u => u.CategoryId == id);
            //Category? target2 = _db.Categories.FirstOrDefault(u => u.CategoryId == id);
            //Category? target2 = _db.Categories.Where(u => u.CategoryId == id).FirstOrDefault();
            return View(Target);
        }

        [HttpPost]

        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order and Name must not be the same");
            }

            if (ModelState.IsValid)
            {
                _unitofwork.Category.update(obj);
                _unitofwork.Save();

                return RedirectToAction("Index");
            }
            return View();

        }




        [HttpPost, ActionName("Delete")]

        public IActionResult Delete(int id)
        {
            Category? obj = _unitofwork.Category.Get(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitofwork.Category.Remove(obj);
            _unitofwork.Save();

            return RedirectToAction("Index");
        }

    }
}
