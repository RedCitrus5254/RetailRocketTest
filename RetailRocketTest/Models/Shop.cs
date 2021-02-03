using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RetailRocketTest.Models
{
    public class Shop
    {
        [Key]
        public string ShopId { get; set; }
    }
}
