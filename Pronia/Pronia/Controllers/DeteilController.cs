using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;

namespace Pronia.Controllers
{
    public class DeteilController : Controller
    {
        AppDbContext _context;
        public DeteilController(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.Include(x => x.ProductPhotos).ToListAsync();
            return View(products);
        }
    }
}
