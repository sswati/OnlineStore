using Microsoft.AspNet.Identity.Owin;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace OnlineStore.Controllers
{

    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (var context = new OnlineStoreContext())
                {
                    var id = User.Identity.GetUserId();
                    var billingAddresses = context.BillingAddress.Where(p => p.CustomerId == id).ToList();

                }
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}