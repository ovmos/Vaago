using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers
{
    public class ContactController : Controller
    {

        VaagoProjectEntities DB = new VaagoProjectEntities();

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(SiteMessage msg)
        {
            if (ModelState.IsValid)
            {

                SiteMessage obj = new SiteMessage();
                obj.msgName = msg.msgName;
                obj.msgEmail = msg.msgEmail;
                obj.msgSubject = msg.msgSubject;
                obj.msgBody = msg.msgBody;

                DB.SiteMessages.Add(obj);
                DB.SaveChanges();
            }
            ModelState.Clear();

            return View("Index");
        }
    }
}