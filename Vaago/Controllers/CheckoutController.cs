using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers
{
    public class CheckoutController : Controller
    {
        VaagoProjectEntities DB = new VaagoProjectEntities();
        // GET: Checkout
        public ActionResult Index()
        {
            var cur = Session["userID"];
            if (cur != null)
            {

                var accID = ((Vaago.Models.Account)cur).account_ID;

                var checkoutDetails = from ca in DB.Carts
                                      join m in DB.Menus on ca.itemID equals m.itemID
                                      join u in DB.Accounts on ca.account_ID equals u.account_ID
                                      where ca.account_ID == accID
                                      select new CheckoutModel
                                      {
                                          menuItem = m,
                                          cartItem = ca,
                                          user = u,
                                      };

                return View(checkoutDetails);
            }

            return RedirectToAction("Index", "Authentication");
        }

        public ActionResult Place_Order(string checkoutName, string checkoutEmail, int itemsCount, string checkoutPhone, string checkoutCity, string checkoutAddress, string checkoutMessage, int totalBill)
        {

            var cur = Session["userID"];
            var accID = ((Vaago.Models.Account)cur).account_ID;

            DateTime now = DateTime.Now;

            var user = DB.Accounts.Where(x => x.account_ID == accID).First<Account>();
            user.name = checkoutName;
            user.phone = checkoutPhone;
            user.location = checkoutAddress;
            DB.SaveChanges();

            Order new_order = new Order();

            new_order.CustomerID = accID;
            new_order.orderTime = now.ToLongTimeString();
            new_order.orderDate = now.ToLongDateString();
            new_order.orderStatus = "Pending";
            new_order.numberOfItems = itemsCount.ToString();
            new_order.Amount = totalBill.ToString();
            new_order.deliveryCharges = "50";
            new_order.City = checkoutCity;
            new_order.notes = checkoutMessage;


            DB.Orders.Add(new_order);
            DB.SaveChanges();
            int id = new_order.orderID;

            List<Cart> items = DB.Carts.Where(x => x.account_ID == accID).ToList();
            foreach (var i in items)
            {
                Order_Details det = new Order_Details();
                det.orderID = id;
                det.itemID = i.itemID;
                det.itemQuantity = 1;
                DB.Order_Details.Add(det);
                DB.Carts.Remove(i);
                DB.SaveChanges();
            }

            //var itemsTodel = DB.Carts.Where(x => x.account_ID == accID). ;
            //DB.SaveChanges();

            return View("Index", "Home");
        }

    }
}