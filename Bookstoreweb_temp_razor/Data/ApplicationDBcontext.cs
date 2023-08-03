using Microsoft.EntityFrameworkCore;

namespace Bookstoreweb_temp_razor.Models;

public class ApplicationDBcontext : DbContext
{
    public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options) 
    {
        
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Sci-fi", DisplayOrder = 1 },
            new Category { CategoryId = 2, Name = "Horror", DisplayOrder = 2 },
            new Category { CategoryId = 3, Name = "Adventure", DisplayOrder = 3 },
            new Category { CategoryId = 4, Name = "Mystery", DisplayOrder = 4 }
        );
    }

}
