using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers
{
    public class UProfileController : Controller
    {
        // GET: UProfile
        public ActionResult Index()
        {
            var cur = Session["userID"];

            if (cur != null)
            {
                using (var context = new VaagoProjectEntities())
                {
                    var currentFromDB = context.Accounts.Where(x => x.account_ID == ((Vaago.Models.Account)cur).account_ID).First<Account>();
                    return View(currentFromDB);
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Update_Profile(Account user)
        {
            using (var context = new VaagoProjectEntities())
            {
                var cur = Session["userID"];
                if (ModelState.IsValid)
                {
                    var obj = context.Accounts.Where(x => x.account_ID == ((Vaago.Models.Account)cur).account_ID).First<Account>();

                    obj.name = user.name;
                    obj.phone = user.phone;
                    obj.location = user.location;
                    context.SaveChanges();
                }
            }
           

            return RedirectToAction("Index","UProfile");
        }



    }
}