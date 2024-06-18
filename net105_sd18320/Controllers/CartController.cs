using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using net105_sd18320.Models;

namespace net105_sd18320.Controllers
{
    public class CartController: Controller
    {
        AppDbContext _context;
        public CartController()
        {
            _context = new AppDbContext();
        }
        public IActionResult Index()
        {


            var username = HttpContext.Session.GetString("account");
            if (string.IsNullOrEmpty(username))
            {
                TempData["LoginRequired"] = "Vui lòng đăng nhập ";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var cartDetail = from b in _context.CartDetails
                                 join d in _context.Product on b.ProductId equals d.Id
                                 where b.Username == username
                                 select new CartDetails
                                 {
                                     Id = b.Id,
                                     ProductId = b.ProductId,
                                     Username = b.Username,
                                     Quantity = b.Quantity,
                                     Product = d,
                                     Status = b.Status,
                                 };
                return View(cartDetail.ToList());
            }
        }
        // Nhấn nút thanh toán sẽ tạo mới Bill và BillDetails tương ứng 
        [HttpPost]
        public IActionResult Pay()
        {
            // Lấy ra các sản phẩm có trong giỏ hàng
            var userName = HttpContext.Session.GetString("account");
            if (string.IsNullOrEmpty(userName))
            {
                TempData["Error"] = "Bạn chưa đăng nhập";
                return RedirectToAction("Login");
            }
            // Lấy thông tin giỏ hàng từ cơ sở dữ liệu
            var cartItems = _context.CartDetails.Where(p => p.Username == userName).ToList();
            if (cartItems.Count == 0)
            {
                TempData["Error"] = "Giỏ hàng trống";
                return RedirectToAction("Index");

            }
            foreach (var item in cartItems)
            {
                var product = _context.Product.Find(item.ProductId);
                if (product == null || item.Quantity > product.Quantity)
                {
                    TempData["Error"] = $"Số lượng sản phẩm '{product?.Name}' trong kho không đủ.";
                    return RedirectToAction("Index", "Cart");
                }
            }


            // Đếm số hóa đơn hiện có để tạo mô tả mới
            var totalBills = _context.Bill.Count(p => p.Username == userName);
                string description = $"Đơn hàng thứ {totalBills + 1}";

                Bill bills = new Bill()
                {
                    Id = Guid.NewGuid(),
                    Username = userName,
                    Description = description,
                    CreateDate = DateTime.Now,
                    Status = 1
                };
                _context.Bill.Add(bills);
                _context.SaveChanges();
                // Tạo danh sách BillDetails từ các CartDetails
                foreach (var item in cartItems)
                {
                    var product = _context.Product.Find(item.ProductId);
                    BillDetails billDetails = new BillDetails()
                    {
                        Id = Guid.NewGuid(),
                        BillId = bills.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Prince = product.Prince // giá lấy từ thằng product 
                    };
                    _context.BillDetails.Add(billDetails);
                    product.Quantity -= item.Quantity;
                    _context.SaveChanges();
                }
                _context.CartDetails.RemoveRange(cartItems);
                _context.SaveChanges();
                TempData["Success"] = "Thanh toán thành công";
                return RedirectToAction("Index", "Bill");
        }
        public IActionResult Edit(Guid id)
        {
            var bill = _context.CartDetails.Find(id);
            return View(bill);
        }
        [HttpPost] 
        public IActionResult EditCart(CartDetails cartDetail, Guid id )
        {
            try
            {
                var product = _context.Product.Find(cartDetail.ProductId);
                if (product == null || cartDetail.Quantity > product.Quantity)
                {
                    TempData["Error"] = $"Số lượng sản phẩm '{product?.Name}' trong kho chỉ còn '{product?.Quantity}'";
                    return RedirectToAction("Index");
                }

                var cartItem = _context.CartDetails.Find(id);
                if (cartItem == null)
                {
                    TempData["Error"] = "Không tìm thấy sản phẩm trong giỏ hàng.";
                    return RedirectToAction("Index");
                }

                cartItem.Quantity = cartDetail.Quantity;
                _context.CartDetails.Update(cartItem);
                _context.SaveChanges();

                TempData["Success"] = "Cập nhật giỏ hàng thành công.";
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Có lỗi");
            }
        }
        public IActionResult Delete(Guid id)
        {
            var deleteCart = _context.CartDetails.Find(id);
            _context.CartDetails.Remove(deleteCart);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
    }
