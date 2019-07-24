using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Customer.BusinessLayer;
using Customer.ServiceLayer.ViewModels;

namespace CustomerWebApplication.Controllers
{
    public class UpdateCustomerController : Controller
    {
        // GET: UpdateCustomer
        [HttpGet]
        public ActionResult CusForm(int ID = 1)
        {

            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            Customer.DataLayer.Customer customers =
                unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>().GetById(ID);

            IEnumerable<CustomerViewModel> customerViewModel = new List<CustomerViewModel>();

            CustomerViewModel result = Mapper.Map<Customer.DataLayer.Customer, CustomerViewModel>(customers);

            ViewBag.ProvinceList = ToSelectListProvince();
            ViewBag.CityList = ToSelectListCity();

            return View(result);
        }


        [NonAction]
        public SelectList ToSelectListProvince()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            var provinces = unitOfWork.GetRepoInstance<Customer.DataLayer.Province>().GetAll();
            var q = from c in provinces
                select new { c.ProvinceID, c.ProvinceName };

            foreach (var p in q)
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
    }
}

