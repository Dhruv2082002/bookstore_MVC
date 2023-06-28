using bookstoreweb.Models;
using Microsoft.EntityFrameworkCore;

namespace bookstoreweb.Data
{
    public class ApplicationDBcontext : DbContext
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "Sci-fi", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "Horror", DisplayOrder = 3 },
                new Category { CategoryId = 4, Name = "Adventure", DisplayOrder = 4 }
                );
        }
    }
}
