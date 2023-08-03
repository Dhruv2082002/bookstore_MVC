using bookstore.DataAccess.Data;
using bookstore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDBcontext _db;
        public ICategoryRepository Category { get; set; }

        public IProductRepository Product { get; set; }

        public ICompanyRepository Company { get; set; }

        public IShoppingCartRepository ShoppingCart { get; set; }

        public IApplicationUserRepository ApplicationUser { get; set; } 

        public IOrderHeaderRepository OrderHeader { get; set; }

        public IOrderDetailRepository OrderDetail { get; set; }


        public UnitOfWork(ApplicationDBcontext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);  
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
