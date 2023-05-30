﻿namespace SquidShopWebApp.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
