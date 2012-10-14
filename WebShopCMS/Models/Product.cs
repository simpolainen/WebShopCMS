using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShopCMS.Models
{
    public class Product
    {
        [Key]
        public Guid ProductKey { get; set; }
        //public string Description { get; set; }
        //public string Name { get; set; }
        //public string Test { get; set; }
        public decimal Product_Price { get; set; }
        public string Product_Name { get; set; }
        public List<Order> Orders { get; set; }
        
    }
}