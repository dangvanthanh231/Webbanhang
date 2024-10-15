using tuan3.Models;

namespace WebsiteBanHang.ViewModels
{
    public class HomeViewModel
    {
        public List<Menu> Menus { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Product> LaptopProds { get; set; }
        public List<Product> DesktopProds { get; set; }
        public Catology LaptopCateProds { get; set; }
        public Catology DesktopCateProds { get; set; }
    }
}

