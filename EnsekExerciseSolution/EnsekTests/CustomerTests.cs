using EnsekDAL;
using EnsekService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EnsekTests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void GetAllCustomersTests()
        {
            var customerDataText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Assets\Test_Accounts.csv");

            MockCustomerData mockCustomerData = new MockCustomerData();
            mockCustomerData.AddDummyData(customerDataText);

            CustomerService customerService = new CustomerService(mockCustomerData);
            var result = customerService.Get();

            Assert.IsTrue(result.Count == 27);
        }

        [TestMethod]
        public void UploadCustomersTest_Live()
        {
            var customerDataText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Assets\Test_Accounts.csv");

            ICustomerData customerData = new SqlCustomerData();
            CustomerService customerService = new CustomerService(customerData);
            var result = customerService.UploadData(customerDataText);

            Assert.IsTrue(result.Count == 27);
        }
    }
}
