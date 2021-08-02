using EnsekDAL.DAL;
using EnsekDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekDAL
{
    public interface IMeterReadingData
    {
        void DeleteAll();
        void Delete(int id);
        void Edit(MeterReading meterReading);
        List<MeterReading> Get();
        MeterReading Get(int id);
        void Upload(List<MeterReading> meterReadings);
        void Upload(MeterReading meterReading);
    }
    public class SqlMeterReadingData : IMeterReadingData
    {
        ContentContext _context;

        public SqlMeterReadingData()
        {
            _context = new ContentContext();
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE MeterReading");
        }

        public void Delete(int id)
        {
            var meterReading = _context.MeterReadings.FirstOrDefault(m => m.Id == id);

            if (meterReading != null)
            {
                _context.MeterReadings.Remove(meterReading);
                _context.SaveChanges();
            }
        }

        public void Edit(MeterReading meterReading)
        {
            var mr = _context.MeterReadings.FirstOrDefault(m => m.Id == meterReading.Id);

            if (mr != null)
            {
                mr.AccountId = meterReading.AccountId;
                mr.Reading = meterReading.Reading;
                mr.Taken = meterReading.Taken;

                _context.SaveChanges();
            }
        }

        public List<MeterReading> Get()
        {
            return _context.MeterReadings.ToList();
        }

        public MeterReading Get(int id)
        {
            return _context.MeterReadings.FirstOrDefault(m => m.Id == id);
        }

        public void Upload(List<MeterReading> meterReadings)
        {
            foreach(var meterReading in meterReadings)
            {
                Upload(meterReading);
            }
        }

        public void Upload(MeterReading meterReading)
        {
            var result = _context.MeterReadings.FirstOrDefault(m => m.Reading == meterReading.Reading && m.AccountId == meterReading.AccountId);

            if(result == null)
            {
                if(meterReading.Taken < DateTime.Now.AddYears(-100))
                {
                    meterReading.Taken = DateTime.Parse("2001-01-01");
                }

                _context.MeterReadings.Add(meterReading);
                _context.SaveChanges();
            }
        }
    }

    public class MockMeterReadingData : IMeterReadingData
    {
        List<MeterReading> _myMeterReadings = new List<MeterReading>();

        public void Delete(int id)
        {
            var result = _myMeterReadings.FirstOrDefault(m => m.Id == id);

            if(result != null)
            {
                _myMeterReadings.Remove(result);
            }
        }

        public void DeleteAll()
        {
            _myMeterReadings = new List<MeterReading>();
        }

        public void Edit(MeterReading meterReading)
        {
            var result = _myMeterReadings.FirstOrDefault(m => m.Id == meterReading.Id);

            if(result != null)
            {
                result.AccountId = meterReading.AccountId;
                result.Reading = meterReading.Reading;
                result.Taken = meterReading.Taken;
            }
        }

        public List<MeterReading> Get()
        {
            return _myMeterReadings;
        }

        public MeterReading Get(int id)
        {
            return _myMeterReadings.FirstOrDefault(m => m.Id == id);
        }

        public void Upload(List<MeterReading> meterReadings)
        {
            foreach (var meterReading in meterReadings)
            {
                Upload(meterReading);
            }
        }

        public void Upload(MeterReading meterReading)
        {
            var result = _myMeterReadings.FirstOrDefault(m => m.Reading == meterReading.Reading && m.AccountId == meterReading.AccountId);

            if (result == null)
            {
                _myMeterReadings.Add(meterReading);
            }
        }
    }
}
