using Microsoft.AspNetCore.Mvc;
using net105_sd18320.Models;

namespace net105_sd18320.Controllers
{
    public class AccountController : Controller
    {
        AppDbContext _context;
        public AccountController()
        {
            _context = new AppDbContext(); // phải có context mới trỏ được tới db 
        }
        public IActionResult Login(string username , string password)
        {
            if(username == null && password == null)
            {
                return View();
            }
            else
            {
                // kiểm tra dữ liệu đăng nhập và trả về kết quả 
                var data = _context.Account.FirstOrDefault(p=>p.Username == username && p.Password == password);
                if (data == null)
                {
                    return Content("Đăng nhập thất bại");
                }
                else
                {
                    HttpContext.Session.SetString("account", username);// gán dữ liệu đăng nhập vào session 
                    return RedirectToAction("Index", "Home"); // nếu thành công thì điều hướng về Index của Home
                }    
                
            } 
                
        }
        public IActionResult SignUp () // thằng này để mở giao diện
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Account account) // thằng này tạo mới
        {
            try
            {
                _context.Account.Add(account);
                Cart cart = new Cart() // sau khi có 1 account được tạo thì tự tạo luôn 1 cái cart 
                {
                    Username = account.Username,
                    Status = 1
                };
                _context.Cart.Add(cart);
                _context.SaveChanges();
                TempData["Status"] = "Đăng ký thành công";
                return RedirectToAction("Login", "Account");
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
