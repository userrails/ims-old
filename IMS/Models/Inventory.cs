using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int Qty { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}