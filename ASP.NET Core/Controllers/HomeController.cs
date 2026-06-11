using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_NET_Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core.Controllers;
public class HomeController: Controller {
    public IActionResult Index() {
        return View(new Customer { CustomerID = 1 });
    }

    [HttpPost]
    public IActionResult PostCustomer(Customer c) {
        if (ModelState.IsValid)
            return View("Success", c);
        return View("Index", c);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View();
    }
}
