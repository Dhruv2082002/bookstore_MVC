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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {

        private ApplicationDBcontext _db;

        public CompanyRepository(ApplicationDBcontext db) : base(db) 
        {
            _db = db;
        }
        

        public void update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
