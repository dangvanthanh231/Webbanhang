﻿using tuan3.Models;

namespace WebsiteBanHang.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
