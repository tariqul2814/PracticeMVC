using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyTutorial.Models;
using VidlyTutorial.Models.ViewModel;

namespace VidlyTutorial.Controllers
{
    public class CustomerController : Controller
    {
        ApplicationDbContext context;
        public CustomerController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Create(NewCustomerViewModel New)
        {
            context.customers.Add(New.customer);
            context.SaveChanges();
            return RedirectToAction("Index","Customer");
        }

        public ActionResult Edit(int Id)
        {
            var NewCustomerViewModel = new NewCustomerViewModel
            {
                customer = context.customers.FirstOrDefault(x=> x.Id==Id),
                membershipType = context.membershiptype.ToList()
            };
            return View(NewCustomerViewModel);
        }

        public ActionResult EditCustomer(NewCustomerViewModel newCustomer)
        {
            newCustomer.membershipType = context.membershiptype.ToList();
            context.customers.AddOrUpdate(newCustomer.customer);
            context.SaveChanges();
            return RedirectToAction("Edit","Customer", new { Id = newCustomer.customer.Id });
        }

        public ActionResult CreateCustomer()
        {
            var MemberTypeList = context.membershiptype.ToList();
            NewCustomerViewModel newVModel = new NewCustomerViewModel()
            {
                membershipType = MemberTypeList
            };
            return View(newVModel);
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