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
        [Display(Name = "Atrikelnummer")]
        public string Article_Number { get; set; }
        [Display(Name = "Namn")]
        public string Product_Name { get; set; }
        [Display(Name = "Pris")]
        public decimal Product_Price { get; set; }
        [Display(Name = "Kommentar")]
        public string Product_Comment { get; set; }
        [Display(Name = "Kategori")]
        public string Product_Category { get; set; }
        [Display(Name = "Information")]
        public string Product_Information { get; set; }
        [Display(Name = "Betyg")]
        public string Product_Rating { get; set; }
        [Display(Name = "Längd")]
        public string Measurements_Lenght { get; set; }
        [Display(Name = "Bredd")]
        public string Measurements_Width { get; set; }
        [Display(Name = "Vikt")]
        public string Measurements_Weight { get; set; }
        [Display(Name = "I lager")]
        public bool In_Stock { get; set; }
        [Display(Name = "Antal i lager")]
        public int Ammount_In_Stock { get; set; }
        [Display(Name = "Garanti")]
        public int Factory_Warranty { get; set; }

    }
}