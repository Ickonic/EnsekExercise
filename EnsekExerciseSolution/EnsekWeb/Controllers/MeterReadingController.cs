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
    public class MeterReadingController : Controller
    {
        MeterReadingService _meterReadingService;
        public MeterReadingController(IMeterReadingData meterReadingData, ICustomerData customerData)
        {
            _meterReadingService = new MeterReadingService(customerData, meterReadingData);
        }

        public ActionResult Index()
        {
            var model = _meterReadingService.Get();

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = _meterReadingService.Get(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MeterReading meterReading)
        {
            _meterReadingService.Edit(meterReading);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = _meterReadingService.Get(id);

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _meterReadingService.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var model = new MeterReading();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MeterReading meterReading)
        {
            _meterReadingService.Upload(meterReading);

            return RedirectToAction("Index");
        }
    }
}