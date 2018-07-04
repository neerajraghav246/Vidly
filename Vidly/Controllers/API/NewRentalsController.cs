using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            //if (newRentalDto.MovieIds.Count == 0)
            //{
            //    return BadRequest("No Movie Ids have been given");
            //}

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRentalDto.CustomerId);

            //if (customer == null)
            //{
            //    return BadRequest($"Customer Id:{newRentalDto.CustomerId} is not valid");
            //}
           
            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id));

            //if (movies.Count() < newRentalDto.MovieIds.Count())
            //{
            //    return BadRequest("One or more movie Ids are invalid");
            //}
            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                {
                    return BadRequest($"Movie with name :{movie.Name} not available");
                }
                movie.NumberAvailable--;
                var rental = new Rental
                {
                    Customer=customer,
                    Movie=movie,
                    RentedOn=DateTime.Now
                };
                _context.Rentals.Add(rental);
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var errors =string.Join("<br/>", e.EntityValidationErrors.SelectMany(s => s.ValidationErrors).Select(x => $"Propery Name:{x.PropertyName}, Validation Message: {x.ErrorMessage}"));
                throw new Exception(errors);
            }
            
            return Ok();
        }
    }
}
