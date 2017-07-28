using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
// using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}