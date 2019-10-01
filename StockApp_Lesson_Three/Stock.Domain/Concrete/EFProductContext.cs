using System.Data.Entity;

namespace Stock.Domain.Concrete
{
    public class EFProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}