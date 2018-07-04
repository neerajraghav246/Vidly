using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Web.UI;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
        // GET: Customers
        public ActionResult Index()
        {


            var viewResult = new ViewResult();
           // viewResult.ViewData.Model = _context.Customers.Include(x => x.AssociatedMembershipType);
            return viewResult;
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(x => x.AssociatedMembershipType).SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View("CustomerForm", new CustomerFormViewModel
            {
                customer=new Customer(),
                membershipTypes = _context.MembershipTypes
            });
        }


        // GET: Customers/Edit/5
        [OutputCache(Duration =50,Location =OutputCacheLocation.Server,VaryByParam ="id")]
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View("CustomerForm", new CustomerFormViewModel
            {
                membershipTypes = _context.MembershipTypes,
                customer = customer
            });
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("CustomerForm", new CustomerFormViewModel
                {
                    membershipTypes = _context.MembershipTypes,
                    customer = customer
                });
            }
            try
            {
                if (customer.Id == 0)
                {
                    _context.Customers.Add(customer);
                }
                else
                {
                    var _customer = _context.Customers.Single(c => c.Id == customer.Id);
                    if (_customer == null)
                        return HttpNotFound();

                    _customer.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                    _customer.Name = customer.Name;
                    _customer.Birthdate = customer.Birthdate;
                    _customer.MembershipTypeId = customer.MembershipTypeId;
                    _context.Entry(_customer).State = EntityState.Modified;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CustomerForm", new CustomerFormViewModel
                {
                    membershipTypes = _context.MembershipTypes,
                    customer = customer
                });
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
