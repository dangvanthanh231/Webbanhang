using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using tuan3.Models;
using Microsoft.EntityFrameworkCore;
using WebsiteBanHang.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebsiteBanHang.Controllers
{
    public class AdminController : Controller
    {
        private readonly WebsiteBanHangContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(WebsiteBanHangContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var adminViewModel = new AdminViewModel
            {
                Prods = products
            };

            return View(adminViewModel);
        }




        // GET: Hiển thị form thêm sản phẩm
        public async Task<IActionResult> CreateAsync()
        {
            var categories = await _context.Catologies.ToListAsync();
            ViewBag.CategoryList = new SelectList(categories, "IdCat", "NameCat");

            return View(new Product());
        }

        // POST: Thêm sản phẩm vào cơ sở dữ liệu
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Thêm sản phẩm vào cơ sở dữ liệu
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }




        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CurrentImageName = product.Img1;
            var categories = await _context.Catologies.ToListAsync();
            ViewBag.CategoryList = new SelectList(categories, "IdCat", "NameCat", product.IdCat);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPro, NamePro, Price, Order,Img1, Link, Hide, IdCat")] Product product)
        {
            if (id != product.IdPro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lưu chỉnh sửa vào cơ sở dữ liệu
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdPro))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(product);
        }



        // GET: Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.IdPro == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Xác nhận xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Kiểm tra sự tồn tại của sản phẩm
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.IdPro == id);
        }
    }
}
