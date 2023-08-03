using bookstore.DataAccess.Data;
using bookstore.DataAccess.Repository.IRepository;
using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private ApplicationDBcontext _db;
        public ProductRepository(ApplicationDBcontext db): base(db) 
        {

            _db = db;

        }

        public void update(Product product)
        {
            _db.Update<Product>(product);   
        }
    }
}
