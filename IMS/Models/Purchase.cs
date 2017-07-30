using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IMS.Models
{
    [MetadataType(typeof(PurchaseMetaData))]
    public class Purchase
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsVatTaken { get; set; }
    }

    // when purchase qty is -ve or o, validation error should raise
    class PurchaseMetaData
    {
        [Remote("IsValidQty", "Purchases", ErrorMessage = "You have entered incorrect value!")]
        public int Qty { get; set; }
    }
}