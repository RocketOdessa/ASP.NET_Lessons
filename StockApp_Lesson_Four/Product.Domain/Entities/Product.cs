namespace Product.Domain
{
    public class Product
    {
        public int ID { get; set; }

        public int ProductCode { get; set; }

        public string ProductName { get; set; }

        public string PartNumber { get; set; }

        public string Description { get; set; }

        public decimal PriceUAN { get; set; }

        public decimal PriceUSD { get; set; }

        public decimal WholesalePrice { get; set; }

        public int Guarantee { get; set; }
    }

}
