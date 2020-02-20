using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyTutorial.Models;

namespace VidlyTutorial.Controllers.Api
{
    public class CustomersController : ApiController
    {
        public ApplicationDbContext context;
        public CustomersController()
        {
            context = new ApplicationDbContext();
        }
        /// <summary>
        /// Get /api/customers
        /// </summary>
        public IEnumerable<Customer> GetCustomer()
        {
            return context.customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            var Customer = context.customers.SingleOrDefault(x => x.Id == id);
            if (Customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                return Customer;
            }
        }
        //post /api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            context.customers.Add(customer);
            context.SaveChanges();
            return customer;
        }

        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            var customerInDb = context.customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            customerInDb.Name = customer.Name;
            customerInDb.Birth = customer.Birth;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = context.customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            context.customers.Remove(customerInDb);
            context.SaveChanges();
        }
    }
}
