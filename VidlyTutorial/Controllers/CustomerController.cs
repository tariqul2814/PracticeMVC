using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyTutorial.Models;

namespace VidlyTutorial.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext context;
        public CustomerController()
        {
            context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }
        // GET: Customer
        public ActionResult Index()
        {
            var customers = context.customers.Include(c=> c.MembershipType).ToList();
            return View(customers);
        }
        
        public ActionResult Details(int id)
        {
            var customer = context.customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
    }
}