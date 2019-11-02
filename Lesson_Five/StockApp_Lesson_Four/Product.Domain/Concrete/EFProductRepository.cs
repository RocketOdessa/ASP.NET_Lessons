using Product.Domain.Abstract;
using System.Collections.Generic;

namespace Product.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFProductContext context = new EFProductContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ID);
                if (dbEntry != null)
                {
                    dbEntry.PartNumber = product.PartNumber;
                    dbEntry.Description = product.Description;
                    dbEntry.PriceUAN = product.PriceUAN;
                    dbEntry.PriceUSD = product.PriceUSD;
                    dbEntry.WholesalePrice = product.WholesalePrice;
                    dbEntry.ProductCode = product.ProductCode;
                    dbEntry.ProductName = product.ProductName;
                    dbEntry.Guarantee = product.Guarantee;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
