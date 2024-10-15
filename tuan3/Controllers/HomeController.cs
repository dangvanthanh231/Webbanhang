using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using tuan3.Models;
using WebsiteBanHang.ViewModels;

namespace tuan3.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebsiteBanHangContext _context;
        public HomeController(WebsiteBanHangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m =>m.Order).Take(2).ToListAsync();
            var slides = await _context.Sliders.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var laptop_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 2).OrderBy(m => m.Order).Take(3).ToListAsync();
            var laptop_cate_prods = await _context.Catologies.Where(m => m.IdCat ==2).FirstOrDefaultAsync();
            var desktop_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 1).OrderBy(m => m.Order).Take(3).ToListAsync();
            var desktop_cate_prods = await _context.Catologies.Where(m => m.IdCat ==1).FirstOrDefaultAsync();
            var viewModel = new HomeViewModel
            {
                Menus = menus,
                Blogs = blogs,
                Sliders = slides,
                LaptopProds = laptop_prods,
                DesktopProds = desktop_prods,
                LaptopCateProds = laptop_cate_prods,
                DesktopCateProds = desktop_cate_prods,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Blogs()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var viewModel = new BlogViewModel
            {
                Menus = menus,
                Blogs = blogs,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Contacts()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m => m.Order).Take(2).ToListAsync();
            var viewModel = new ContactViewModel
            {
                Menus = menus,
                Blogs = blogs,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }

        public async Task<IActionResult> _SlidePartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _ProductPartial()
        {
            return PartialView();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
