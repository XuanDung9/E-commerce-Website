namespace net105_sd18320.Models
{
    public class Cart // thằng này là bảng trung gian để khi load dữ liệu của account sẽ dựa vào thằng này để đỡ phải load nhiều dữ liệu 
    {

        public string Username { get; set; }
        public int Status { get; set; }
        public virtual List<CartDetails> Details { get; set; } 
        public Account Account { get; set; }
    }
}
