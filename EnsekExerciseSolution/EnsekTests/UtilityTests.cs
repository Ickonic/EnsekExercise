using EnsekService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekTests
{
    [TestClass]
    public class UtilityTests
    {
        [DataTestMethod]
        [DataRow("12345")]
        [DataRow("00001")]
        [DataRow("00532")]
        public void IsValidMeterReadingTest_True(string meterReading)
        {
            var result = Utility.IsValidMeterReading(meterReading);
            Assert.IsTrue(result == true);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("-1")]
        [DataRow("X")]
        [DataRow("999999")]
        [DataRow("0")]
        public void IsValidMeterReadingTest_False(string meterReading)
        {
            var result = Utility.IsValidMeterReading(meterReading);
            Assert.IsTrue(result == false);
        }
    }
}
