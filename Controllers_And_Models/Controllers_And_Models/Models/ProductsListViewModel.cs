using External_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Controllers_And_Models.HtmlHelpers;

namespace Controllers_And_Models.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}