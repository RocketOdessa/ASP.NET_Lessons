using System.Collections.Generic;

namespace Product.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);
    }
}
