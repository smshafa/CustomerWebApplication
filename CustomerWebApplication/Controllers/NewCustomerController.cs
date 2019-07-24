using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Customer.BusinessLayer;
using Customer.ServiceLayer.ViewModels;

namespace CustomerWebApplication.Controllers
{
    public class NewCustomerController : Controller
    {
        [HttpGet]
        // GET: NewCustomer
        public ActionResult CusForm()
        {

            ViewBag.ProvinceList = ToSelectListProvince();
            ViewBag.CityList = ToSelectListCity();
            return View();
        }


        //https://www.c-sharpcorner.com/article/dropdownlist-in-asp-net-mvc/
        [NonAction]
        public SelectList ToSelectListProvince()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            var provinces = unitOfWork.GetRepoInstance<Customer.DataLayer.Province>().GetAll();
            var q = from c in provinces
                    select new { c.ProvinceID, c.ProvinceName };

            foreach(var p in q)
            {
                list.Add(new SelectListItem()
                {
                    Text = p.ProvinceName,
                    //Value = p.ProvinceID.ToString()
                    Value = p.ProvinceName
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        [NonAction]
        public SelectList ToSelectListCity()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            var cities = unitOfWork.GetRepoInstance<Customer.DataLayer.City>().GetAll();
            var q = from c in cities
                    select new { c.CityID, c.CityName };

            foreach (var p in q)
            {
                list.Add(new SelectListItem()
                {
                    Text = p.CityName,
                    Value = p.CityName
                    //Value = p.CityID.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        [HttpPost]
        public ActionResult CusForm(CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid == true)
            {
                string name = customerViewModel.FirstName;
                string porvince = customerViewModel.ProvinceName;
                string city = customerViewModel.CityName;
            }

            // very important when we want to be in view after post.
            ViewBag.ProvinceList = ToSelectListProvince();
            ViewBag.CityList = ToSelectListCity();
            return View();
        }

        public ActionResult SelectCategory()
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Action", Value = "0" });

            items.Add(new SelectListItem { Text = "Drama", Value = "1" });

            items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });

            items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });

            ViewBag.MovieType = items;

            return View();

        }


    }
}