using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Panno { get; set; }
        public string Remarks { get; set; }
    }
}