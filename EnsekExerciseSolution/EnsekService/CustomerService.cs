using EnsekDAL;
using EnsekDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekService
{
    public class CustomerService
    {
        ICustomerData _customerData;
        public CustomerService(ICustomerData customerData)
        {
            _customerData = customerData;
        }

        public List<Customer> UploadData(string customerData)
        {
            var result = Parse(customerData);

            _customerData.Upload(result);

            return result;
        }

        public List<Customer> Parse(string data)
        {
            List<Customer> customers = new List<Customer>();

            foreach (var v in data.Replace("\r", "").Split('\n'))
            {
                var split = v.Split(',');

                int accountId;
                bool isNumber = int.TryParse(split[0], out accountId);

                if (isNumber)
                {
                    customers.Add(new Customer { AccountId = accountId, FirstName = split[1], LastName = split[2] });
                }
            }

            return customers;
        }

        public Customer GetByAccountId(int accountId)
        {
            return _customerData.GetByAccountId(accountId);
        }

        public List<Customer> Get()
        {
            return _customerData.Get();
        }

        public Customer Get(int id)
        {
            return _customerData.Get(id);
        }

        public bool DoesExist(int accountId)
        {
            return _customerData.DoesExist(accountId);
        }

        public void Delete(int id)
        {
            _customerData.Delete(id);
        }

        public void Add(Customer customer)
        {
            _customerData.Add(customer);
        }

        public void Edit(Customer customer)
        {
            _customerData.Edit(customer);
        }
    }
}
