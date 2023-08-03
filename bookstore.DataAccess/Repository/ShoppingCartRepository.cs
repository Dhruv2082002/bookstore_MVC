using bookstore.DataAccess.Data;
using bookstore.DataAccess.Repository.IRepository;
using bookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bookstore.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository

    {

        private ApplicationDBcontext _db;

        public ShoppingCartRepository(ApplicationDBcontext db) : base(db) 
        {
            _db = db;
        }
        

        public void update(ShoppingCart obj)
        {
            _db.ShoppingCarts.Update(obj);
        }
    }
}
