using Microsoft.EntityFrameworkCore;
using RetailRocketTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetailRocketTest.Repository
{
    class ShopRepository
    {
        private readonly AppDBContent appDBContent;

        public ShopRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public void AddProducts(IEnumerable<Product> products)
        {
            appDBContent.Products.AddRange(products);
            appDBContent.SaveChanges();
        }

        public IEnumerable<Product> GetProducts(string shopId)
        {
            return appDBContent.Products.Where(p => p.Shop.ShopId == shopId).Include(p => p.Shop);
        }

        public Shop GetShop(string shopId)
        {
            Shop shop = appDBContent.Shops.FirstOrDefault(s => s.ShopId.Equals(shopId)) ?? new Shop() { ShopId = shopId };
            return shop;
        }

        public IEnumerable<Shop> Shops => appDBContent.Shops;

        public IEnumerable<Product> Products => appDBContent.Products.Include(p => p.Shop);



    }
}
