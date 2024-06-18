using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net105_sd18320.Models;

namespace net105_sd18320.Controllers
{
    public class BillDetailsController : Controller
    {
        AppDbContext _context;
        public BillDetailsController()
        {
            _context = new AppDbContext();
        }
        public IActionResult Index(Guid id)
        {
            var billDetail = _context.BillDetails.Where(p=>p.BillId==id).ToList();
            return View(billDetail);
        }
    }
}
