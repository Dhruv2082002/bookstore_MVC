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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private ApplicationDBcontext _db;

        public CategoryRepository(ApplicationDBcontext db) : base(db) 
        {
            _db = db;
        }
        

        public void update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
