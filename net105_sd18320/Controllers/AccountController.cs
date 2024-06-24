using Microsoft.AspNetCore.Mvc;
using net105_sd18320.Models;

namespace net105_sd18320.Controllers
{
    public class AccountController : Controller
    {
        AppDbContext _context;
        public AccountController()
        {
            _context = new AppDbContext(); 
        }
        public IActionResult Login(string username , string password)
        {
            if(username == null && password == null)
            {
                return View();
            }
            else
            {
      
                var data = _context.Account.FirstOrDefault(p=>p.Username == username && p.Password == password);
                if (data == null)
                {
                    TempData["LoginFaile"] = "Tên đăng nhập hoặc mật khẩu sai";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("account", username);// gán dữ liệu đăng nhập vào session 
                    return RedirectToAction("Index", "Home"); 
                }    
                
            } 
                
        }
        public IActionResult SignUp () // View
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Account account) // action
        {
            try
            {
                _context.Account.Add(account);
                _context.SaveChanges();
                if (account.Role == 2) // customer
                {
                    Cart cart = new Cart() // sau khi có 1 acount customer được tạo thì tự tạo luôn 1 cái cart 
                    {
                        Username = account.Username,
                        Status = 1
                    };
                    _context.Cart.Add(cart);
                    _context.SaveChanges();
                }
                TempData["Status"] = "Đăng ký thành công";
                return RedirectToAction("Login", "Account");
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        // xem account có role gì 
        public  IActionResult Index()
        {
            var username = HttpContext.Session.GetString("account");
            if(username == null)
            {
                return RedirectToAction("Login", "Account");   
            }
            var account = _context.Account.FirstOrDefault(p=>p.Username == username);
            if(account == null)
            {
                return NotFound();
            }
            if(account.Role != 1)
            {
                TempData["checkAccount"] = "Chức năng này không dành cho khách hàng ";
                return RedirectToAction("Index", "Home");
            }
            var accounts = _context.Account.ToList();
            return View (accounts);
        }
        public IActionResult Delete(Guid id)
        {
            try
            {
                var deleteAccount = _context.Account.Find(id);
                if (deleteAccount == null)
                {
                    return NotFound(); // Trả về lỗi 404 nếu không tìm thấy tài khoản
                }

                _context.Account.Remove(deleteAccount);
                _context.SaveChanges();
                TempData["removeSuccess"] = "Xóa thành công ";
                return RedirectToAction("Index", "Account");
            }
            catch (Exception ex)
            {
                // Log lỗi để xem chi tiết nếu cần
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        public IActionResult Details(Guid id)
        {
            var item = _context.Account.Find(id);
            return View(item);
        }
      
    }
}
