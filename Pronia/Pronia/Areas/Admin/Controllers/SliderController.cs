using Microsoft.AspNetCore.Mvc;
using Pronia.DAL;
using Pronia.Models;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        AppDbContext _context;
        IWebHostEnvironment _environment;
        public SliderController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            List<Slider> sliders=_context.Sliders.ToList();
            return View(sliders) ;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!slider.Photofile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("PhotoFile","Format duzgun deyil! Image daxil edin");
                return View();
            }

            string filename=slider.Photofile.FileName;
            string path = _environment.WebRootPath+@"\Upload\Slider"+filename;
            using (FileStream fileStream = new FileStream(path+filename, FileMode.Create))
            {
                slider.Photofile.CopyTo(fileStream);
            }
            slider.ImgUrl= filename;

                if (!ModelState.IsValid)
                {
                    return View();
                }
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            string path = _environment.WebRootPath + @"\Upload\Slider\" + slider.ImgUrl;

            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //public IActionResult Update(int id)
        //{
        //    var slider = _context.Sliders.FirstOrDefault(X => X.Id == id);
        //    if (slider == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(slider);
        //}
        //[HttpPost]
        //public IActionResult Update(Slider slider)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    var oldSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
        //    if (oldSlider == null) { return RedirectToAction("Index"); }
        //    oldSlider.Title = slider.Title;
        //    oldSlider.SubTitle = slider.SubTitle;
        //    oldSlider.Photofile = slider.Photofile;
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
