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
            ViewBag.Title = "Abone Kaydı";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Subscriber s, bool accepted)
        {
            ActionResult result = View();

            s.RegisterDate = DateTime.Today;
            this.Validate(s);

            if (ModelState.IsValid)
                result = this.DoWork(s, accepted);

            return result;
        }

        private void Validate(Subscriber s)
        {
            if (string.IsNullOrEmpty(s.EMail))
                ModelState.AddModelError("EMail", "EMail boş geçilemez.");

            if (string.IsNullOrEmpty(s.Name))
                ModelState.AddModelError("Name", "Name boş geçilemez.");

        }

        private ActionResult DoWork(Subscriber s,bool accepted)
        {
            ActionResult result = View();
            SubscriberDAO subscriberDAO = new SubscriberDAO();

            if(!accepted)
            {
                ModelState.AddModelError("accept", string.Format("Sayın {0} kabul etmeden abonelik yapamazsınız."));
                return View();
            }

            if(subscriberDAO.Insert(s))
            {
                ViewBag.Message = string.Format("Sayın {0}, {1} tarihinde aboneliğiniz başlamıştır.", s.Name, s.RegisterDate);
                result = View(s);
            }
            else
            {
                ViewBag.Message = string.Format("Sayın {0}, {1} tarihinde zaten aboneliğiniz vardır.", s.Name, s.RegisterDate);
                result = View();
            }

            ViewBag.Subscriptions = subscriberDAO.GetSubscribersByEmail(s.EMail);
            return result;
        }

        public ActionResult SearchByName()
        {
            ViewBag.Title = "Abonelik arama sayfası";

            return View();
        }

        [HttpPost]
        public ActionResult SearchByName(string email)
        {
            ActionResult result = View();
            try
            {
                if (string.IsNullOrEmpty(email))
                    return result;

                SubscriberDAO subscriberDAO = new SubscriberDAO();
                result = View(subscriberDAO.GetSubscribersByEmail(email));
            }
            catch {}

            return result;
        }
    }
}
