using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers
{
    public class MenuController : Controller
    {
        VaagoProjectEntities DB = new VaagoProjectEntities();
        // GET: Menu
        public ActionResult Index()
        {

            //Querying with LINQ to Entities 
            List<Menu> items = DB.Menus.ToList();
            return View(items);
        }

        [HttpPost]
        public ActionResult Add_ToCart(string addToCart)
        {
            Cart itemSelected = new Cart();
            itemSelected.itemID = Convert.ToInt32(addToCart);
            itemSelected.itemQuantity = 1;

            var cur = Session["userID"];

            if (cur != null)
            {
                itemSelected.account_ID = ((Vaago.Models.Account)cur).account_ID;

            }
            else
            {
                if (Session["non-userID"] == null)
                {
                    Random r = new Random();
                    int num = r.Next();
                    Session["non-userID"] = num;
                    itemSelected.account_ID = num;
                }
                else
                {
                    var num = Session["non-userID"];
                    itemSelected.account_ID = Convert.ToInt32(num);
                }
            }

            DB.Carts.Add(itemSelected);
            DB.SaveChanges();
            return RedirectToActionPermanent("Index", "Menu");
        }

    }
}