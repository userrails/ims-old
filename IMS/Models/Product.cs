using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IMS.Models
{
    [MetadataType(typeof(ProductMetaData))]

    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public Decimal SellingPrice { get; set; }
    }

    class ProductMetaData
    {
        [Remote("IsNameExists", "Products", ErrorMessage = "Already Exists")]
        public string Name { get; set; }
    }
}