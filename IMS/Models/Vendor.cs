using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IMS.Models
{
    [MetadataType(typeof(VendorMetaData))]
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Panno { get; set; }
        public string Remarks { get; set; }
    }

    class VendorMetaData
    {
        [Remote("IsNameExists", "Vendors", ErrorMessage="Already Exists")]
        public string Name { get; set; }
        [Remote("IsPanExists", "Vendors", ErrorMessage="Already Exists")]
        public string Panno { get; set; }
    }
}