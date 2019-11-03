using System.Collections.Generic;

namespace StockApp_Lesson_Four.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product.Domain.Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}