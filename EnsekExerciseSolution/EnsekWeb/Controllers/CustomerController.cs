using EnsekDAL;
using EnsekDAL.Models;
using EnsekService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnsekWeb.Controllers
{
    public class CustomerController : Controller
    {
        CustomerService _customerService;
        public CustomerController(ICustomerData customerData)
        {
            _customerService = new CustomerService(customerData);
        }

        public ActionResult Index()
        {
            var model = _customerService.Get();

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = _customerService.Get(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Customer Customer)
        {
            _customerService.Edit(Customer);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = _customerService.Get(id);

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _customerService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var model = new Customer();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer Customer)
        {
            _customerService.Add(Customer);

            return RedirectToAction("Index");
        }
    }
}