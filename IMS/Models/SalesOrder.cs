using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

       // public virtual ICollection<Sales> Sales { get; set; }
    }
}