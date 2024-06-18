using Microsoft.AspNetCore.Mvc;
using net105_sd18320.Models;
using System.Diagnostics;

namespace net105_sd18320.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var sessionData = HttpContext.Session.GetString("account"); // lấy dữ liệu từ session theo key account 
            if ( sessionData == null)
            {
                ViewData["login"] = "Bạn chưa đăng nhập";
            }
            else
            {
                ViewData["login"] = "Xin chào " + sessionData;
            }    
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
