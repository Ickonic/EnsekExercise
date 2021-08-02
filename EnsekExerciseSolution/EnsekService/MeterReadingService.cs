using EnsekDAL;
using EnsekDAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekService
{
    public class MeterReadingService
    {
        CustomerService _customerService;
        IMeterReadingData _meterReadingData;

        public MeterReadingService(ICustomerData customerData, IMeterReadingData meterReadingData)
        {
            _customerService = new CustomerService(customerData);
            _meterReadingData = meterReadingData;
        }

        public List<MeterReading> UploadData(string meterReadingData)
        {
            var result = Parse(meterReadingData);

            _meterReadingData.Upload(result.Where(m=>m.IsValid == true).ToList());

            return result;
        }

        public List<MeterReading> Parse(string meterReadingData)
        {
            List<MeterReading> meterReadings = new List<MeterReading>();

            foreach (var v in meterReadingData.Replace("\r", "").Split('\n'))
            {
                var sections = v.Split(',');

                bool isValid = true;

                if(sections.Length != 3)
                {
                    isValid = false;
                }

                if (isValid)
                {
                    int accountId;
                    bool isInt = int.TryParse(sections[0], out accountId);

                    var accountIsValid = false;

                    if (isInt)
                    {
                        accountIsValid =  _customerService.DoesExist(accountId);
                    }

                    int readingId;
                    int.TryParse(sections[2], out readingId);

                    if (accountIsValid)
                    {
                        if (readingId != 0)
                        {
                            accountIsValid = Utility.IsValidMeterReading(readingId.ToString());
                        }
                    }

                    DateTime taken;
                    DateTime.TryParse(sections[1], out taken);

                    if(readingId == 0 || taken == DateTime.Parse("0001-01-01 00:00:00"))
                    {
                        accountIsValid = false;
                    }

                    meterReadings.Add(new MeterReading
                    {
                        AccountId = accountId,
                        Reading = readingId,
                        Taken = taken,
                        IsValid = accountIsValid
                    });

                    if (accountIsValid)
                    {
                        Debug.WriteLine(accountId, " # " + readingId + " # " + taken + " # " + isValid  );
                    }
                }
                else
                {
                    meterReadings.Add(new MeterReading { Id = 0, AccountId = 0, Reading = 0, Taken = DateTime.Now, IsValid = false });
                }
            }

            return meterReadings;
        }

        public void DeleteAll()
        {
            _meterReadingData.DeleteAll();
        }

        public List<MeterReading> Get()
        {
            return _meterReadingData.Get();
        }

        public MeterReading Get(int id)
        {
            return _meterReadingData.Get(id);
        }

        public void Edit(MeterReading meterReading)
        {
            _meterReadingData.Edit(meterReading);
        }

        public void Delete(int id)
        {
            _meterReadingData.Delete(id);
        }

        public void Add(MeterReading meterReading)
        {
            bool isValid = Utility.IsValidMeterReading(meterReading.Reading.ToString());

            Customer customer = _customerService.GetByAccountId(meterReading.AccountId);

            if (isValid && customer != null)
            {
                _meterReadingData.Upload(meterReading);
            }
        }

        public void Upload(MeterReading meterReading)
        {
            bool isValid = Utility.IsValidMeterReading(meterReading.Reading.ToString());

            Customer customer = _customerService.GetByAccountId(meterReading.AccountId);

            if (isValid && customer != null)
            { 
                _meterReadingData.Upload(meterReading);
            }
        }
    }
}
