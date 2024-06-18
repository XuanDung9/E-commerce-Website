namespace net105_sd18320.Models
{
    public class Product
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Prince { get; set; }
        public int Status { get; set; }
        public int Quantity { get; set; }
        // quan hệ 
        public virtual List<BillDetails> BillDetails { get; set; }
        public virtual List<CartDetails> CartDetails { get; set; }

    }
}
