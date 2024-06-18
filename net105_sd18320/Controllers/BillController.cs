using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net105_sd18320.Models;

namespace net105_sd18320.Controllers
{
    public class BillController : Controller
    {
        AppDbContext _context;
        public BillController()
        {
            _context = new AppDbContext();
        }
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString( "account" );
            var bill = _context.Bill.Where(B=>B.Username == username).ToList(); // Lấy ra thằng Bill của thằng username
            return View( bill );
        }
        [HttpPost]
        public IActionResult Cancel(Guid id )
        {
            var bill = _context.Bill.Include(p=>p.BillDetails).FirstOrDefault(p=>p.Id==id);
            if (bill == null)
            {
                return NotFound();
            }
            foreach(var item in  bill.BillDetails)
            {
                var product = _context.Product.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.Quantity += item.Quantity;
                }    
            }
            bill.Status = 100;
            _context.SaveChanges();
            TempData["Message"] = "Hủy đơn hàng thành công";
            return RedirectToAction("Index","Bill");
        }
        [HttpPost]
        public IActionResult Reorder(Guid id) // làm giống hệt thằng AddToCart
        {
           var bill = _context.Bill.Include(p=>p.BillDetails).FirstOrDefault( p=>p.Id==id);
            if (bill == null)
            {
                return BadRequest();
            }
            // lấy ra thằng username 
            var username = HttpContext.Session.GetString("account");
            // nếu trong Cart có SP thì xóa SP đi , thêm lại thằng SP ở Bill vào Cart 
            var removeProduct = _context.CartDetails.Where(p => p.Username == username).ToList();
            _context.CartDetails.RemoveRange(removeProduct); 
            _context.SaveChanges();

            // tạo mới thằng Cart từ Bill 
            foreach ( var item in bill.BillDetails)
            {
                // check số lượng thông qua item 
                var product = _context.Product.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null || product.Quantity < item.Quantity)
                {
                    TempData["Error"] = $"Số lượng trong kho không đủ cho'{product.Name}'";
                    return RedirectToAction("Index");
                }
                var cartDetail = new CartDetails()
                {
                    Id = Guid.NewGuid(),
                    Username = username,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                };
                _context.CartDetails.Add(cartDetail);
            }    
            _context.SaveChanges();
            TempData["Message"] = "Mua lại đơn hàng thành công";
            return RedirectToAction("Index","Cart");

        }


    }
}
