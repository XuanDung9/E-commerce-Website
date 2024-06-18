namespace net105_sd18320.Models
{
    public class Cart // thằng này là bảng trung gian để khi load dữ liệu của account sẽ dựa vào thằng này để đỡ phải load nhiều dữ liệu 
    {
        // thằng này chỉ là thằng lưu username từ thằng account. Các Product 
        // khi Add sẽ được thêm vào CartDetails
        public string Username { get; set; }
        public int Status { get; set; }
        public virtual List<CartDetails> Details { get; set; } // bên thằng config thì cứ gọi thằng Details vì đặt tên quan hệ 
        public Account Account { get; set; }
    }
}
