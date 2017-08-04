using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMS.Models;
using System.Dynamic;

namespace IMS.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Orders
        public ActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.SellEntries = db.Sales.ToList();
            model.Orders = db.Order.ToList();
            // model.NewOrders = db.Order.ToList();
            // model.NewSalesEntries = db.Sales.ToList();
            // SalesOrderViewModel model = new SalesOrderViewModel();
            // model.SellEntries = db.Sales.ToList();
            // model.Orders = db.Order.ToList();
            return View(model);
        }
    }
}