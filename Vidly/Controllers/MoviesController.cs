using System;
using System.Collections.Generic;
using System.Data.Entity; // Include method
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            this._context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            // BAD: ViewData["RandomMovie"] = movie;
            // BAD: ViewBag.Movie = movie;

            // BAD: return View();
            // NOT SO BAD WITHOUT VIEWMODELS return View(movie);
            return View(viewModel);
            // BAD: return new ViewResult();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        // movies
        public ViewResult Index()
        {
            //var movies = _context.Movies.Include(movie => movie.Genre).ToList();
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List");
            }
                
            return View("ReadOnlyList");
        }

        // Attribute routing
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2})}:range(1, 12)")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(movieLambda => movieLambda.Genre).
                SingleOrDefault(movieLambda => movieLambda.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieDetailsViewModel
            {
                Movie = movie
            };

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            var movieId = Convert.ToInt32(Session["MovieId"]);

            movie.DateAdded = DateTime.Now;

            if (movieId == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movieId);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }
    }
}