using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            var cur = Session["Admin"];
            if (cur != null)
            {
                using (var context = new VaagoProjectEntities())
                {
                    var currentFromDB = context.Accounts.Where(x => x.account_type == 1).First<Account>();
                    return View("~/Views/Admin/Settings.cshtml", currentFromDB);
                }
            }
            else
            {
                return View("~/Views/Admin/Settings.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Update_Admin(Account item)
        {
            using (var context = new VaagoProjectEntities())
            {
                var obj = context.Accounts.Where(x => x.account_type == 1).First<Account>();
                obj.name = item.name;
                obj.pass = item.pass;
                obj.phone = item.phone;
                obj.location = item.location;
                context.SaveChanges();
            }


            return RedirectToAction("Index", "Setting");
        }

    }
}