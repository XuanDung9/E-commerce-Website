namespace net105_sd18320.Models
{
    public class BillDetails // 1 billdetail chỉ quan hệ với 1 sản phẩm , chứa thông tin về một sản phẩm trong hóa đơn(bill) có gì
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid BillId { get; set; }
        public int Quantity { get; set; }   
        public decimal Prince { get; set; } // giá tại thời điểm mua , bán 
        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; } 
    }
}
