using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShopCMS.Models
{
    public class ProductCategoryView
    {
        public Product Product { get; set; }
        public Categories Category { get; set; }
    }
}