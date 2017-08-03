using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class SalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Product).Include(s => s.Vendor).Include(s => s.Order);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VendorId,ProductId,Qty,Price,DiscountPercentage,DiscountAmount,IsVatTaken")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                
               

                Order so = new Order();
                // create salesorder and save it on session
                if (Session["order_id"] == null)
                {
                    using (var salesorderDbContext = new ApplicationDbContext())
                    {
                        so.OrderDate = DateTime.Now;
                        salesorderDbContext.Order.Add(so);
                        salesorderDbContext.SaveChanges();
                        // add SalesOrder_id on session
                        Session["order_id"] = so.Id;
                    }
                }

                Product product;
                // Get stock from database
                using (var context = new ApplicationDbContext())
                {
                    product = context.Products.Where(p => p.Id == sales.ProductId).FirstOrDefault<Product>();
                }

                // update modified entities using new context
                using (var dbContext = new ApplicationDbContext())
                {
                    // edit stock object with new values out of context scope in disconnected mode
                    if (product != null)
                    {
                        product.StockQty = product.StockQty - sales.Qty;
                        // mark entity as modified
                        dbContext.Entry(product).State = System.Data.Entity.EntityState.Modified;
                        // call SaveChanges
                        dbContext.SaveChanges();
                    }
                    else
                    {
                       // Display error message saying product doesnot exists so you cannot create sales entry or validate frm model
                    }
                }
                sales.OrderId = so.Id;
                db.Sales.Add(sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sales.ProductId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", sales.VendorId);
            return View(sales);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sales.ProductId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", sales.VendorId);
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VendorId,ProductId,Qty,Price,DiscountPercentage,DiscountAmount,IsVatTaken")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sales.ProductId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "Name", sales.VendorId);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = db.Sales.Find(id);
            db.Sales.Remove(sales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // check if stock is there to sell the product
        public JsonResult IsStockAvailable(int Qty, int ProductId)
        {
            Product product;
           using (var context = new ApplicationDbContext())
           {
             product = db.Products.Where(p => p.Id.Equals(ProductId)).FirstOrDefault<Product>();
           }
           var u_can_sell = ((product.StockQty < Qty) || (product.StockQty <= 0) || (Qty <= 0));
           return Json(!u_can_sell, JsonRequestBehavior.AllowGet);
            //return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}
