using System.Data.Entity;

namespace Product.Domain.Concrete
{
    public class EFProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
