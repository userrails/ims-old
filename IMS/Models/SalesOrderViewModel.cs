using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class SalesOrderViewModel
    {
        //public IEnumerable<IMS.Models.Order> Orders { get; set; }
        //public IEnumerable<IMS.Models.Sales> Sales { get; set; }

        public Order Order { get; set; }
        public Sales Sales { get; set; }

        public List<Order> Orders { get; set; }
        public List<Sales> SellEntries { get; set; }
    }
}