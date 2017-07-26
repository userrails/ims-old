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
            var sales = db.Sales.Include(s => s.Product).Include(s => s.Vendor);
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
                db.Sales.Add(sales);

                Stock stock;
                // Get stock from database
                using (var context = new ApplicationDbContext())
                {
                    stock = context.Stocks.Where(s => s.ProductId == sales.ProductId).FirstOrDefault<Stock>();
                }

                // update modified entities using new context
                using (var dbContext = new ApplicationDbContext())
                {
                    // edit stock object with new values out of context scope in disconnected mode
                    if (stock != null)
                    {
                        stock.Qty = sales.Qty;
                        // mark entity as modified
                        dbContext.Entry(stock).State = System.Data.Entity.EntityState.Modified;
                        // call SaveChanges
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        Stock stok = new Stock();
                        stok.ProductId = sales.ProductId;
                        stok.Qty = sales.Qty;
                        dbContext.Stocks.Add(stok);
                        dbContext.SaveChanges();
                    }
                }
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
    }
}
