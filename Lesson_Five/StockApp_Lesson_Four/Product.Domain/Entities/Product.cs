using System.ComponentModel.DataAnnotations;

namespace Product.Domain
{
    public class Product
    {
        public int ID { get; set; }
        [Display(Name = "Product Code")]
        [Required]
        public int ProductCode { get; set; }
        [Display(Name = "Product Name")]
        [Required]
        public string ProductName { get; set; }
        [Display(Name = "Part Number")]
        [Required]
        public string PartNumber { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "PriceUAN")]
        [Required]
        public decimal PriceUAN { get; set; }
        [Display(Name = "PriceUSD")]
        public decimal PriceUSD { get; set; }
        [Display(Name = "Wholesale Price")]
        public decimal WholesalePrice { get; set; }
        [Display(Name = "Guarantee")]
        [Required]
        public int Guarantee { get; set; }
    }

}
