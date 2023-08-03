using bookstore.DataAccess.Repository.IRepository;
using bookstore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace bookstoreweb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;   

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(productList);
        }

        public IActionResult Details(int Productid)
        {
            //Product product = _unitOfWork.Product.Get(u => u.ProductId == Productid ,includeProperties: "Category");
            //return View(product);

            //make a shopping cart object and populate it with the product and count of 1

            ShoppingCart cartObj = new ShoppingCart()
            {
                Product = _unitOfWork.Product.Get(u => u.ProductId == Productid, includeProperties: "Category"),
                ProductId = Productid
            };
            return View(cartObj);

        }

        //POST - Details Action Method, add to shopping cart functionality. if the order exist add product to existing cart else
        //or if not create a new cart and add product to it.

        [HttpPost]
        [Authorize]

        public IActionResult Details(ShoppingCart ShoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCart.ApplicationUserId = userId;

            //get cart
            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == ShoppingCart.ApplicationUserId && u.ProductId == ShoppingCart.ProductId);

            if (cartFromDb != null)
            {
                cartFromDb.Count += ShoppingCart.Count;
                _unitOfWork.ShoppingCart.update(ShoppingCart); 
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(ShoppingCart);
            }

            _unitOfWork.Save();

            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}