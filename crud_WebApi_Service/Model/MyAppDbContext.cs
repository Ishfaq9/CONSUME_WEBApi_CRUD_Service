using Microsoft.EntityFrameworkCore;

namespace Crud_with_webApi.Model
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Brand> brands { get; set; }    
    }
}
