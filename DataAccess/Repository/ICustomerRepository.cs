using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        public List<Customer> GetAll();
        public Customer GetById(int id);
        public void DeleteCustomer(int id);
        public void UpdateCustomer(Customer c);
        public void InsertCustomer(Customer c);
        public Customer FindCustomer(string email, string password);
        public List<Customer> SearchCustomers(string searchCriteria);


    }
}
