using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{

    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using(var context = new OnlineStoreContext())
            {
                var wine = context.Wines.ToList();
                var chocolates = context.Chocolates.ToList();
                var customers = context.CustomerDetails.ToList();
                var billingAdd = context.BillingAddress.ToList();
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