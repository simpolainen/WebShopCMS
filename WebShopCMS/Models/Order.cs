using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShopCMS.Models
{
    public class Order
    {
        //public List<Product> Products { get; set; }
        [Key]
        public Guid OrderId { get; set; }
        public string Comment { get; set; }

    }
}