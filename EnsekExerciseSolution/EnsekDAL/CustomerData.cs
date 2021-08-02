using EnsekDAL.DAL;
using EnsekDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekDAL
{
    public interface ICustomerData
    {
        Customer GetByAccountId(int accountId);
        List<Customer> Get();
        bool DoesExist(int accountId);
        void Upload(List<Customer> customers);
        Customer Get(int id);
        void Edit(Customer customer);
        void Delete(int id);
        void Add(Customer customer);

    }

    public class SqlCustomerData : ICustomerData
    {
        ContentContext _context = new ContentContext();

        public void Add(Customer customer)
        {
            var cust = _context.Customers.FirstOrDefault(c => c.AccountId == customer.AccountId);

            if (cust == null)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var cust = _context.Customers.FirstOrDefault(c => c.Id == id);

            if (cust != null)
            {
                _context.Customers.Remove(cust);
                _context.SaveChanges();
            }
        }

        public bool DoesExist(int accountId)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.AccountId == accountId);

            if (customer != null)
            {
                return true;
            }
            return false;
        }

        public void Edit(Customer customer)
        {
            var cust = _context.Customers.FirstOrDefault(c => c.Id == customer.Id);

            if (cust != null)
            {
                cust.LastName = customer.LastName;
                cust.FirstName = customer.FirstName;
                cust.AccountId = customer.AccountId;
                _context.SaveChanges();
            }
        }

        public List<Customer> Get()
        {
            return _context.Customers.ToList();
        }

        public Customer Get(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer GetByAccountId(int accountId)
        {
            return _context.Customers.FirstOrDefault(c=>c.AccountId == accountId);
        }

        public List<Customer> GetByAccountId()
        {
            return _context.Customers.ToList();
        }

        public void Upload(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Upload(customer);
            }
        }

        public void Upload(Customer customer)
        {
            var result = _context.Customers.FirstOrDefault(c => c.AccountId == customer.AccountId);

            if (result == null)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
        }
    }

    public class MockCustomerData : ICustomerData
    {
        List<Customer> _myCustomers = new List<Customer>();

        public MockCustomerData()
        {
            _myCustomers.Add(new Customer
            {
                Id = 1,
                AccountId = 12345,
                FirstName = "Arren",
                LastName = "Almonroy"
            });
            _myCustomers.Add(new Customer
            {
                Id = 2,
                AccountId = 23456,
                FirstName = "Barron",
                LastName = "Bomeroy"
            });
            _myCustomers.Add(new Customer
            {
                Id = 3,
                AccountId = 34567,
                FirstName = "Carron",
                LastName = "Comeroy"
            });
            _myCustomers.Add(new Customer
            {
                Id = 4,
                AccountId = 45678,
                FirstName = "Darren",
                LastName = "Conroy"
            });
        }

        public Customer GetByAccountId(int accountId)
        {
            return _myCustomers.FirstOrDefault(c => c.AccountId == accountId);
        }

        public List<Customer> Get()
        {
            return _myCustomers;
        }

        public bool DoesExist(int accountId)
        {
            var customer = _myCustomers.FirstOrDefault(c => c.AccountId == accountId);

            if(customer != null)
            {
                return true;
            }
            return false;
        }

        public void AddDummyData(string data)
        {
            _myCustomers = new List<Customer>();

            foreach (var v in data.Replace("\r", "").Split('\n'))
            {
                var split = v.Split(',');

                int accountId;
                bool isNumber = int.TryParse(split[0], out accountId);

                if (isNumber)
                {
                    _myCustomers.Add(new Customer { AccountId = accountId, FirstName = split[1], LastName = split[2] });
                }
            }
        }

        public void Upload(List<Customer> customers)
        {
            foreach(var customer in customers)
            {
                Upload(customer);
            }
        }

        public void Upload(Customer customer)
        {
            var result = _myCustomers.FirstOrDefault(c => c.AccountId == customer.AccountId);

            if(result != null)
            {
                _myCustomers.Add(customer);
            }
        }

        public Customer Get(int id)
        {
            return _myCustomers.FirstOrDefault(c => c.Id == id);
        }

        public void Edit(Customer customer)
        {
            var cust = _myCustomers.FirstOrDefault(c => c.Id == customer.Id);

            if (cust != null)
            {
                cust.LastName = customer.LastName;
                cust.FirstName = customer.FirstName;
                cust.AccountId = customer.AccountId;
            }
        }

        public void Delete(int id)
        {
            var cust = _myCustomers.FirstOrDefault(c => c.Id == id);

            if (cust != null)
            {
                _myCustomers.Remove(cust);
            }
        }

        public void Add(Customer customer)
        {
            var cust = _myCustomers.FirstOrDefault(c => c.AccountId == customer.AccountId);

            if (cust == null)
            {
                _myCustomers.Add(customer);
            }
        }
    }
}
