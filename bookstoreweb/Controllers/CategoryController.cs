using bookstoreweb.Data;
using bookstoreweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace bookstoreweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBcontext _db;
        public CategoryController(ApplicationDBcontext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            List<Category> ObjCategoriesList = _db.Categories.ToList();
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
                ModelState.AddModelError("Display Order and Name must not be the same","Name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(_obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
