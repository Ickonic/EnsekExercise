using EnsekDAL;
using EnsekDAL.Models;
using EnsekService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace EnsekAPI.Controllers
{
    public class HomeController : ApiController
    {
        ICustomerData _customerData;
        CustomerService _customerService;
        IMeterReadingData _meterReadingData;
        MeterReadingService _meterReadingService;

        public HomeController(ICustomerData customerData, IMeterReadingData meterReadingData)
        {
            _customerData = customerData;
            _customerService = new CustomerService(_customerData);
            _meterReadingData = meterReadingData;
            _meterReadingService = new MeterReadingService(_customerData, _meterReadingData);
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        public JsonResult<List<MeterReading>> Get(string data)
        {
            var meterReadings = _meterReadingService.UploadData(data);
            return Json(meterReadings);
        }

        public List<MeterReading> Get()
        {
            return _meterReadingService.Get();
        }
    }
}
