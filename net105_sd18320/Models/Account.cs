using System.ComponentModel.DataAnnotations;

namespace net105_sd18320.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        // Data anotation validation được sử dụng để thực hiện các validate dữ liệu ngay ơ model 
        [Required] // validate bắt buộc phải có 
        [StringLength(256 , MinimumLength =6 , ErrorMessage ="Tên đăng nhập chứa 6 đến 256 kí tự")]
        public string Username { get; set; }


        [Required] 
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Password chứa 6 đến 256 kí tự ")]
        public string Password { get; set; }


        [Required] 
        [EmailAddress (ErrorMessage = "Vui lòng nhập đúng định dạng của Email ")]
        public string Email { get; set; }

        [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$",
            ErrorMessage = "Số điện thoại phải đúng định dạng xxx-xxx-xxxx")]
        public string Phone { get; set; }
        [Required(ErrorMessage ="Nhập lại vai trò")]
        [Range(1,2,ErrorMessage ="Role chỉ có thể là 1 (admin) hoặc 2 (customer)")]
        public int Role { get; set; } // 1 admin , 2 customer 
        public string Address { get; set; }
        public virtual List<Bill> Bill { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
