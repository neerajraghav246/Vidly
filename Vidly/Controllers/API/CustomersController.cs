using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }
        
        public Customer GetCustomer(int id)
        {
            var _customer= _context.Customers.SingleOrDefault(c => c.Id == id);
            if (_customer == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content=new StringContent($"No Employee found with id={id}"),
                    ReasonPhrase= "Customer Not Found"
                };
                throw new HttpResponseException(response);
            }
            return _customer;
        }

        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"Customer with Name={customer.Name} has invalid data"),
                    ReasonPhrase = "Invalid Customer data"
                };
                throw new HttpResponseException(response);
            }
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error while saving customer.Error Message: {ex.Message}"),
                    ReasonPhrase = "Unexpected error occured"
                };
                throw new HttpResponseException(response);
            }
            return customer;
        }

        [HttpPut]
        public Customer UpdateCustomer(int id,Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"Customer with ID={id} has invalid data"),
                    ReasonPhrase = "Invalid Customer data"
                };
                throw new HttpResponseException(response);
            }
           
                var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
                if (customerInDB == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent($"Customer not found with ID={id} in our database"),
                        ReasonPhrase = "Customer Not Found",
                        
                    };
                    //response.Headers.Add("ContentType", "application/json");
                    throw new HttpResponseException(response);
                }
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Customer, Customer> ().ForMember(src=>src.Id,opt=>opt.Ignore());
                });

                IMapper mapper = config.CreateMapper();
                mapper.Map(customer, customerInDB);
                _context.SaveChanges();
                return customer;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error while updating customer.Error Message: {ex.Message}"),
                    ReasonPhrase = "Unexpected error occured"
                };
                throw new HttpResponseException(response);
            }
        }

        [HttpDelete]
        public bool DeleteCustomer(int id)
        {
            try
            {
                var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
                if (customerInDB == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent($"Customer not found with ID={id} in our database"),
                        ReasonPhrase = "Customer Not Found"
                    };
                    throw new HttpResponseException(response);
                }

               _context.Customers.Remove(customerInDB);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error while deleting customer with ID={id}.Error Message: {ex.Message}"),
                    ReasonPhrase = "Unexpected error occured"
                };
                throw new HttpResponseException(response);
            }
        }

    }
}
