using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IMS.Models
{
    [MetadataType(typeof(SalesMetaData))]
    public class Sales
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
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }

    // when stock qty is less than selling qty, validation error should raise
    // call IsStockAvailable(Qty, ProductId)
    class SalesMetaData
    {
        public int ProductId { get; set; }

        [Remote("IsStockAvailable", "Sales", AdditionalFields="ProductId", ErrorMessage = "The quantity of the product you are selling is not available in Stock or you have entered incorrect value!")]
        public int Qty { get; set; }

    }
}