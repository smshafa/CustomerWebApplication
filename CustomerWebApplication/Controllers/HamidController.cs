using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customer.ServiceLayer.ViewModels;

namespace CustomerWebApplication.Controllers
{
    public class HamidController : Controller
    {
        [HttpGet]
        // GET: NewCustomer
        public ActionResult CusForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CusForm(CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid == true)
            {
                string name = customerViewModel.FirstName;
            }

            return View();
        }
    }
}