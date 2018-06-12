﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(
                        "MovieForm", new MovieViewModel
                        {
                            generes = _context.Generes
                        });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(
                "MovieForm" ,new MovieViewModel
                {
                    movie=movie,
                    generes=_context.Generes
                });
        }

        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Avenger" };
            var customers = new List<Customer>() {
                new Customer{Name="Customer 1"},
                new Customer{Name="Customer 2"},
                 new Customer{Name="Customer 3"},
                new Customer{Name="Customer 4"},
                 new Customer{Name="Customer 5"},
                new Customer{Name="Customer 6"}
            };
            var randomMovieVM = new RandomMovieViewModel()
            {
                movie = movie,
                customers = customers
            };
            var viewR = new ViewResult();
            viewR.ViewData.Model = randomMovieVM;
            // return new EmptyResult();
            return viewR;
            //return View(movie);
            //return Content("Hello world");
        }
      
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(g => g.AssociatedGenere);
            var viewResult = new ViewResult();
            viewResult.ViewData.Model = movies;

            return viewResult;
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(mv => mv.AssociatedGenere).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(0,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"year: {year}, month: {month}");
        }
    }
}