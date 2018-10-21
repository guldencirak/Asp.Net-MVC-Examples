using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MyOrganizationApp.Models;
using MyOrganizationApp.DB;

namespace MyOrganizationApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Subscriber subscriber, bool accepted)
        {
            ActionResult result=null;

            try
            {
                SubscriberDAO subscriberDAO = new SubscriberDAO();

                result = subscriberDAO.Insert(subscriber) ?
                                      View("SubscriberResultView", subscriber) : View("~/Views/Errors/RegisterDuplicateError.cshtml");
                
            }
            catch {}

            return result;
        }
    }
}
