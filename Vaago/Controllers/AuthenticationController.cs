using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers
{
    public class AuthenticationController : Controller
    {
        VaagoProjectEntities DB = new VaagoProjectEntities();

        // GET: Authentication
        public ActionResult index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult login_Account(Login user)
        {
            if (ModelState.IsValid)
            {

                var userDetails = DB.Accounts.Where(m => m.email == user.Email && m.pass == user.Password && m.account_type == 2).FirstOrDefault();
                if (userDetails == null)
                {
                    TempData["Msg"] = "Email/Password is invalid!";
                    return RedirectToAction("index");
                }
                else
                {
                    Session["userID"] = userDetails;
                    return RedirectToAction("Index", "UProfile");
                }
            }
            else
            {
                TempData["Msg"] = "Email/Password is invalid!";
                return RedirectToAction("index");
            }
        }


        public ActionResult Logout_Account()
        {
            Session.Abandon();
            return RedirectToAction("index", "Authentication");
        }


        // GET: Authentication
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register_Account(Account user)
        {
            if (ModelState.IsValid)
            {
                Account obj = new Account();
                obj.account_type = 2;
                obj.name = user.name;
                obj.email = user.email;
                obj.pass = user.pass;
                obj.phone = Equals(user.phone) ? user.phone : null;
                obj.location = Equals(user.location) ? user.location : null;

                DB.Accounts.Add(obj);

                DB.SaveChanges();
                Session["userID"] = obj;
                return RedirectToAction("Index", "UProfile");
            }
            else
            {
                Console.WriteLine("Error", "Enter email or password.");
                return RedirectToAction("CreateAccount");
            }
        }
    }
}