using System;
using System.Collections.Generic;
using System.Text;

namespace RetailRocketTest.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Shop Shop { get; set; }
    }
}
