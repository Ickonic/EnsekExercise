using EnsekDAL;
using EnsekService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EnsekTests
{
    [TestClass]
    public class MeterReadingTests
    {
        [TestMethod]
        public void GetAllValidMeterReadingsTest()
        {
            var meterReadingDataText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Assets\Meter_Reading.csv");
            var customerDataText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Assets\Test_Accounts.csv");

            MockCustomerData mockCustomerData = new MockCustomerData();
            mockCustomerData.AddDummyData(customerDataText);
            ICustomerData customerData = mockCustomerData;
            
            CustomerService customerService = new CustomerService(customerData);
            IMeterReadingData meterReadingData = new MockMeterReadingData();
            MeterReadingService meterReadingService = new MeterReadingService(customerData, meterReadingData);
            var result = meterReadingService.Parse(meterReadingDataText);

            int valid = result.Where(d => d.IsValid == true).Count();
            int invalid = result.Where(d => d.IsValid == false).Count();

            Assert.IsTrue(valid == 23);
            Assert.IsTrue(invalid == 14);
        }

        [TestMethod]
        public void UploadMeterReadingsTest()
        {
            var meterReadingDataText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Assets\Meter_Reading.csv");
            var customerDataText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Assets\Test_Accounts.csv");

            MockCustomerData mockCustomerData = new MockCustomerData();
            mockCustomerData.AddDummyData(customerDataText);
            ICustomerData customerData = mockCustomerData;

            CustomerService customerService = new CustomerService(customerData);
            IMeterReadingData meterReadingData = new MockMeterReadingData();
            MeterReadingService meterReadingService = new MeterReadingService(customerData, meterReadingData);
            var result = meterReadingService.UploadData(meterReadingDataText);

            int valid = result.Where(d => d.IsValid == true).Count();
            int invalid = result.Where(d => d.IsValid == false).Count();

            Assert.IsTrue(valid == 23);
            Assert.IsTrue(invalid == 14);
        }

        [TestMethod]
        public void UploadMeterReadingsTest_Live()
        {
            var meterReadingDataText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Assets\Meter_Reading.csv");
            var customerDataText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Assets\Test_Accounts.csv");

            ICustomerData customerData = new SqlCustomerData();
            CustomerService customerService = new CustomerService(customerData);
            customerService.UploadData(customerDataText);

            IMeterReadingData meterReadingData = new SqlMeterReadingData();
            MeterReadingService meterReadingService = new MeterReadingService(customerData, meterReadingData);
            var result = meterReadingService.UploadData(meterReadingDataText);

            int valid = result.Where(d => d.IsValid == true).Count();
            int invalid = result.Where(d => d.IsValid == false).Count();

            Assert.IsTrue(valid == 23);
            Assert.IsTrue(invalid == 14);
        }

        [TestMethod]
        public void DeleteAllTest_Live()
        {
            ICustomerData customerData = new SqlCustomerData();
            IMeterReadingData meterReadingData = new SqlMeterReadingData();
            MeterReadingService meterReadingService = new MeterReadingService(customerData, meterReadingData);

            meterReadingService.DeleteAll();

            var result = meterReadingService.Get();

            Assert.IsTrue(result.Count == 0);
        }
    }
}
