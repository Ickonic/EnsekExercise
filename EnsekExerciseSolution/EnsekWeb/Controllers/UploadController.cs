using EnsekDAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EnsekWeb.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase File1)
        {
            if (File1 == null)
            {
                // Error Handling Goes Here
            }
            else
            {
                StreamReader reader = new StreamReader(File1.InputStream);
                string data = reader.ReadToEnd();
                reader.Close();

                string apiUrl = ConfigurationManager.AppSettings["ApiUrl"];

                var url = apiUrl + "/meter-reading-uploads?data=" + data;
                var parameters = new Dictionary<string, string> { { "data", data } };
                var encodedContent = new FormUrlEncodedContent(parameters);

                var httpclient = new HttpClient();

                HttpResponseMessage response = await httpclient.PostAsync(url, encodedContent).ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync();
                    var meterReadings = JsonConvert.DeserializeObject<List<MeterReading>>(result.Result);

                    TempData["MeterReadings"] = meterReadings;
                }
            }

            return RedirectToAction("index");
        }
    }
}