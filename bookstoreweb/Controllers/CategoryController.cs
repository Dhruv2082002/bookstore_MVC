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
                ModelState.AddModelError("Name","Display Order and Name must not be the same");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(_obj);
                _db.SaveChanges();
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

            Category? Target = _db.Categories.Find(id);
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
                _db.Categories.Update(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
            
        }




        [HttpPost,ActionName("Delete")]

        public IActionResult Delete(int id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
