using tuan3.Models;

namespace WebsiteBanHang.ViewModels
{
    public class AdminViewModel
    {
        public List<Menu> Menus { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Product> Prods { get; set; }
        public String cateName { get; set; }
        public IEnumerable<Product> Products { get; internal set; }
    }
}
