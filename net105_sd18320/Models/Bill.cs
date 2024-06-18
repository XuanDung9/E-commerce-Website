namespace net105_sd18320.Models
{
    public class Bill
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Username { get; set; }
        public int Status { get; set; }
        // thể hiện quan hệ thông qua navigation 
        // bill sẽ quan hệ với thằng Account và BillDetails , không quan hệ với thằng Product 
        public virtual Account Account { get; set; }
        public virtual List<BillDetails> BillDetails { get; set; }
    }
}
