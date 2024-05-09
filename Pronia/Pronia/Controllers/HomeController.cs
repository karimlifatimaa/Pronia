using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;
using Pronia.ViewModel;
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
        public IActionResult Index()
        {
            List<Product> products = _context.Products.Include(x=>x.ProductPhotos).ToList();
            HomeVm homeVm = new HomeVm()
            {
                Products = products,
                Sliders = _context.Sliders.ToList()
            };
            return View(homeVm);
        }


    }
}
