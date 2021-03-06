﻿using System;
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
    public class MovieController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Movie
        [HttpGet]
        public ActionResult Random()
        {
            var movie = new Movie()
            {
                Id = 1,
                Name = "Shrek!"
            };
            var customers = new List<Customer> 
            {
                new Customer {Name="Customer 1"},
                new Customer {Name ="Customer 2"}
            };
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

        public ActionResult Movie()
        {
            var Movie = context.movies.Include(c=> c.Genre).ToList();
            //var customers = context.customers.Include(c=> c.MembershipType).ToList();
            return View(Movie);
        }

        public ActionResult EditMovie (int Id)
        {
            if(Id==0)
            {
                var MoviesVModel = new MovieVModel
                {
                    genres = context.genres.ToList()
                };
                return View(MoviesVModel);
            }
            else
            {
                var MoviesVModel = new MovieVModel
                {
                    movie = context.movies.FirstOrDefault(c => c.Id == Id),
                    genres = context.genres.ToList()
                };
                return View(MoviesVModel);
            }
            
        }

        public ActionResult Edit(Movie Movie)
        {
            Movie.ReleaseDate = Movie.ReleaseDate.Date;
            if(Movie.Id==0)
            {
                context.movies.Add(Movie);
                context.SaveChanges();

                var MoviesVModel = new MovieVModel
                {
                    movie = Movie,
                    genres = context.genres.ToList()
                };
                return View("EditMovie", MoviesVModel);
            }
            else
            {
                context.movies.AddOrUpdate(Movie);
                context.SaveChanges();

                var MoviesVModel = new MovieVModel
                {
                    movie = Movie,
                    genres = context.genres.ToList()
                };
                return View("EditMovie", MoviesVModel);
            }
            
        }
        public ActionResult Index (int? pageIndex, string sortBy)
        {
            if(!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(string.Format("pageIndex = {0} & sortBy={1}",pageIndex,sortBy));
        }

        [Route("movie/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        //[Route("movie/released/{year}/{month}")]
        //[Route(“books/{isbn?}”)]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year+"/"+month);
        }
    }
}