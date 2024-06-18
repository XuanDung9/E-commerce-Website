using System.ComponentModel.DataAnnotations;

namespace net105_sd18320.Models
{
    public class Account
    {
        // Data anotation validation được sử dụng để thực hiện các validate dữ liệu ngay ơ model 
        [Required] // validate bắt buộc phải có 
        [StringLength(256 , MinimumLength =6 , ErrorMessage ="Username phải nằm trong khoảng từ 6-256")]
        public string Username { get; set; }


        [Required] // validate bắt buộc phải có 
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Password phải nằm trong khoảng từ 6-256")]
        public string Password { get; set; }


        [Required] // validate bắt buộc phải có 
        [EmailAddress (ErrorMessage = "Email không đúng định dạng ")]
        public string Email { get; set; }

        [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$",
            ErrorMessage = "Số điện thoại phải đúng định dạng xxx-xxx-xxxx")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual List<Bill> Bill { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
