using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class AdminCustomersController : Controller
    {
        VaagoProjectEntities DB = new VaagoProjectEntities();
        // GET: AdminCustomers
        public ActionResult Index()
        {
            List<Account> customersList = DB.Accounts.Where(x => x.account_type == 2).ToList();
            return View("~/Views/Admin/AdminCustomers.cshtml", customersList);
            //}
        }
    }
}