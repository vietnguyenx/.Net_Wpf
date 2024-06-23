using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public void DeleteCustomer(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                Customer? deleteCustomer = context.Customers.FirstOrDefault(c => c.CustomerId == id) ?? throw new Exception("No customer was found");
                context.Customers.Remove(deleteCustomer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer FindCustomer(string email, string password)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.Customers.FirstOrDefault(c => c.EmailAddress == email && c.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex. Message);
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer GetById(int id)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.Customers.FirstOrDefault(c => c.CustomerId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertCustomer(Customer c)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                context.Customers.Add(c);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                var existingCustomer = context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
                if (existingCustomer == null)
                {
                    throw new Exception("Customer not found");
                }

                existingCustomer.CustomerFullName = customer.CustomerFullName;
                existingCustomer.Telephone = customer.Telephone;
                existingCustomer.EmailAddress = customer.EmailAddress;
                existingCustomer.CustomerBirthday = customer.CustomerBirthday;
                existingCustomer.CustomerStatus = customer.CustomerStatus;
                existingCustomer.Password = customer.Password;

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Customer> SearchCustomers(string searchTerm)
        {
            try
            {
                using var context = new FuminiHotelManagementContext();
                return context.Customers
                    .Where(c => c.CustomerFullName.Contains(searchTerm) ||
                                c.EmailAddress.Contains(searchTerm) ||
                                c.Telephone.Contains(searchTerm))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
