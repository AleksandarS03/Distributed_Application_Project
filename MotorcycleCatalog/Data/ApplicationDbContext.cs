using Microsoft.EntityFrameworkCore;
using MotorcycleCatalog.Models;

namespace MotorcycleCatalog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Review> Reviews { get; set; }       
    }
}
