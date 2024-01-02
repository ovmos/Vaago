using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers
{
    public class CartController : Controller
    {
        VaagoProjectEntities DB = new VaagoProjectEntities();
        // GET: Cart
        public ActionResult Index()
        {
            var cur = Session["userID"];
            var num = Session["non-userID"];

            if (cur != null)
            {
                var accID = ((Vaago.Models.Account)cur).account_ID;

                var cartNitem = from ca in DB.Carts
                                join m in DB.Menus on ca.itemID equals m.itemID
                                where ca.account_ID == accID
                                select new CartItem
                                {
                                    menuItem = m,
                                    cartItem = ca
                                };

                return View(cartNitem);

            }
            else if (num != null)
            {
                int innum = Convert.ToInt32(num);
                var cartNitem = from ca in DB.Carts
                                join m in DB.Menus on ca.itemID equals m.itemID
                                where ca.account_ID == innum
                                select new CartItem
                                {
                                    menuItem = m,
                                    cartItem = ca
                                };

                return View(cartNitem);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateCart(string totalBill)
        {
            var cur = Session["userID"];
            int finalVal;
            if (cur != null)
            {
                var accID = ((Vaago.Models.Account)cur).account_ID;
                finalVal = accID;
            }
            else
            {
                var num = Session["non-userID"];
                int innum = Convert.ToInt32(num);
                finalVal = innum;
            }

            List<Cart> obj = DB.Carts.Where(x => x.account_ID == finalVal).ToList();
            foreach (var item in obj)
            {
                item.totalAmount = Convert.ToInt32(totalBill);

            }
            DB.SaveChanges();

            return RedirectToAction("Index", "Checkout");
        }
    }

}