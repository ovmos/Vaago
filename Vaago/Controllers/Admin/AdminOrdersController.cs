using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class AdminOrdersController : Controller
    {
        VaagoProjectEntities DB = new VaagoProjectEntities();
        // GET: AdminOrders
        public ActionResult Index()
        {
            List<Order> ordersList = DB.Orders.ToList();

            return View("~/Views/Admin/AdminOrders.cshtml", ordersList);
            //}
        }



        public ActionResult ViewOrder(int orderID)
        {
            var orderHistory = from ol in DB.Orders
                               join od in DB.Order_Details on ol.orderID equals od.orderID
                               join cust in DB.Accounts on ol.CustomerID equals cust.account_ID
                               join item in DB.Menus on od.itemID equals item.itemID
                               where ol.orderID == orderID
                               select new ViewModel
                               {
                                   order = ol,
                                   order_Details = od,
                                   customer = cust,
                                   menuItem = item
                               };

            return View("~/Views/Admin/ViewOrder.cshtml", orderHistory);
        }

        public ActionResult UpdateStatus(int orderID, string orderStatus)
        {
            var cur = Session["Admin"];
            if (ModelState.IsValid)
            {
                var obj = DB.Orders.Where(x => x.orderID == orderID).First<Order>();

                obj.orderStatus = orderStatus;
                DB.SaveChanges();
            }

            return RedirectToAction("Index", "AdminOrder");
        }

    }
}