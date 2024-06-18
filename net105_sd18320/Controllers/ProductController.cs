using Microsoft.AspNetCore.Mvc;
using net105_sd18320.Models;

namespace net105_sd18320.Controllers
{
    public class ProductController : Controller
    {
        public AppDbContext _context;
        public ProductController()
        {
            _context = new AppDbContext();
        }
        // GET : ProductController
        public IActionResult Index()
        {
            var allProduct = _context.Product.ToList();
            return View(allProduct);
        }
        // Get ProductController/Details
        public IActionResult Details(Guid id)
        {
            var product = _context.Product.Find(id); // find là phương thức chỉ áp dụng cho thằng FK
            return View(product);
        }
        public IActionResult Create()
        {
            Product fakeData = new Product() // tạo thông tin để điền sẵn sang 
            {
                Id = Guid.NewGuid(),
                Name = "Sản phẩm mẫu",
                Description = "Tưởi ngon tròn vị",
                Prince = new Random().Next(10000,1000000),
                Status = 1
            };
            return View(fakeData);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                _context.Product.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");   
            }
            catch
            {
                return Content("Có lỗi nào đó ");
            }
        }
        public IActionResult Edit(Guid id )
        {
            // lấy được thông tin thằng sửa lên form sửa 
            var editData = _context.Product.Find(id);
            return View(editData);

        }
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            try
            {
                var editData = _context.Product.Find(product.Id);
                editData.Name = product.Name;
                editData.Description = product.Description;
                editData.Prince = product.Prince;
                editData.Quantity= product.Quantity;
                _context.Product.Update(editData);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Có lỗi nào đó ");
            }
        }
        public IActionResult Delete(Guid id)
        {
            var deleteData = _context.Product.Find(id);
            _context.Product.Remove(deleteData);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // action thêm sản phẩm vào giỏ hàng 
        public IActionResult AddToCart(Guid id , int quantity)
        {
            // check xem đã đăng nhập chưa, nếu chưa thì bắt đăng nhập 
            var check = HttpContext.Session.GetString("account");
            if (string.IsNullOrEmpty(check))
            {
                TempData["AddToCart"] = "Đăng nhập trước khi thêm sản phẩm vào giỏ hàng";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var product = _context.Product.Find(id);
                if (product == null)
                {
                    TempData["product"] = "Sản phẩm không tồn tại";
                    return RedirectToAction("Index", "Product");
                }

                if (product.Quantity < quantity)
                {
                    TempData["productLow"] = "Số lượng sản phẩm trong kho không đủ";
                    return RedirectToAction("Index", "Product");
                }

                // xem trong giỏ hàng của user đó đã có sản phẩm có id này hay chưa 
                var cartItrem =_context.CartDetails.FirstOrDefault(p=>p.ProductId==id && p.Username==check);
                if (cartItrem == null) // nếu sản phẩm chưa tồn tại trong giỏ hàng
                {
                    CartDetails cartDetails = new CartDetails()
                    {
                        Id = Guid.NewGuid(),
                        Username = check,
                        ProductId = id,
                        Quantity = quantity,
                        Status = 1
                    };
                    _context.CartDetails.Add(cartDetails);
                    _context.SaveChanges();
                }
                else
                {
                    if (product.Quantity < cartItrem.Quantity+quantity)
                    {
                        TempData["productLow"] = $"Số lượng sản phẩm '{product.Name}' trong kho không đủ";
                        return RedirectToAction("Index", "Product");
                    }
                    cartItrem.Quantity = cartItrem.Quantity + quantity; // cộng số lượng nếu đã có item trong cart 
                    _context.CartDetails.Update(cartItrem);
                    _context.SaveChanges();
                }    
                return RedirectToAction("Index","Product"); // quay lại trang danh sách sản phẩm 
            }    
        }
    }
}
