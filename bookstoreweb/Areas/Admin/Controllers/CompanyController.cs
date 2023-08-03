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

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public CompanyController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
            List<Company> ObjCompanyList = _unitofwork.Company.GetAll().ToList();
            return View(ObjCompanyList);
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

            if (id == null| id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company Companyobj = _unitofwork.Company.Get(u => u.Id == id);
                return View(Companyobj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company Companyobj)
        {

                if (ModelState.IsValid)
                {

                    if (Companyobj.Id == 0)
                    {
                        _unitofwork.Company.Add(Companyobj);
                    }
                    else
                    {
                        _unitofwork.Company.update(Companyobj);
                    }
                    _unitofwork.Save();
                    return RedirectToAction("Index");

                }
                else
                {
                return View(Companyobj);

            }

        }
   

        [HttpPost, ActionName("Delete")]

        public IActionResult Delete(int id)
        {
            Company? obj = _unitofwork.Company.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitofwork.Company.Remove(obj);
            _unitofwork.Save();

            return RedirectToAction("Index");
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> ObjCompanyList = _unitofwork.Company.GetAll().ToList();
            return Json(new {data = ObjCompanyList});
        }

        public IActionResult Delet(int? id)
        {
            var obj = _unitofwork.Company.Get(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "error" });
            }


            _unitofwork.Company.Remove(obj);
            _unitofwork.Save();

            return Json(new {success = true,message = "success"});  
        }
        #endregion

    }
}
