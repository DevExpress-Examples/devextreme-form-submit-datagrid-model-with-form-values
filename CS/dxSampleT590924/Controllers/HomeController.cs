using dxSampleT590924.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dxSampleT590924.Controllers {
    public class HomeController : Controller {
        public ActionResult Index()
        {
            return View(new Customer() { CustomerID = 1 });
        }
        [HttpPost]
        public ActionResult PostCustomer(Customer c)
        {
            if (ModelState.IsValid)
                return View("Success", c);
            else
                return View("Index", c);
        }
    }
}
