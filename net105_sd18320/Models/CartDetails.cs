namespace net105_sd18320.Models
{
    public class CartDetails
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Username { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; } // sử dụng để khi thanh toán có cái muốn thanh toán có cái không  
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
