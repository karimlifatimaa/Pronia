using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;
using System.Diagnostics;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.Include(x=>x.ProductPhotos).ToListAsync();
            return View(products);
        }


    }
}
