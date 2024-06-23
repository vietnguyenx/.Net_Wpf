using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{

    public class AccountLogin
    {
        public int? Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public AccountLogin(string role, string email, string password)
        {
            Role = role;
            Email = email;
            Password = password;
        }
    }

    public class CustomerObject
    {
        private readonly ICustomerRepository _customerRepository;
        public static AccountLogin? AccountLogin;

        public CustomerObject()
        {
            _customerRepository = new CustomerRepository();
        }

        public bool Login(string email, string password)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var projectDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..\\..\\..\\.."));
            var appSettingsPath = Path.Combine(projectDirectory, "DataAccess");

            var builder = new ConfigurationBuilder()
                .SetBasePath(appSettingsPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            string adminEmail = configuration.GetSection("AdminAccount:Email").Value;
            string adminPassword = configuration.GetSection("AdminAccount:Password").Value;

            if (adminEmail.Equals(email) || adminPassword.Equals(password))
            {
                AccountLogin = new AccountLogin("admin", email, password);
                return true;
            };




            Customer customer = _customerRepository.FindCustomer(email, password);
            if (customer == null)
            {
                return false;
            }

            AccountLogin = new AccountLogin("customer", email, password)
            {
                Id = customer.CustomerId
            };

            return true;
        }

        public List<Customer> GetCustomers() => _customerRepository.GetAll();
        public void AddCustomer(Customer cus) => _customerRepository.InsertCustomer(cus);
        public void UpdateCustomer(Customer cus) => _customerRepository.UpdateCustomer(cus);
        public void DeleteCustomer(int customerId) => _customerRepository.DeleteCustomer(customerId);
        public Customer GetCustomer(int customerId) => _customerRepository.GetById(customerId);
        public List<Customer> SearchCustomers(string searchCriteria) => _customerRepository.SearchCustomers(searchCriteria);


    }
}
