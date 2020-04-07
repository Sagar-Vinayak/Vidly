using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies

        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            var movies = _context.Movies.Include("Genre").ToList();
            return View(movies);
        }
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include("Genre").SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
        public ActionResult Random()
        {
            var movie = new Movie() { Name="Shrek!"};
            var movies = new List<Movie>
            {   new Movie{ Name="Breaking Bad"},
                new Movie{Name="Life of Pie"},
                new Movie{Name="Contagion"}
            };
            var customers = new List<Customer>
            {
                new Customer{Id=1, Name="Customer1"},
                new Customer{Id=2, Name="Customer2"}
            };
            var viewModel = new RandomMovieViewModel()
            {
                Movies = movies,
                Customers = customers
            };
            return View(viewModel);
        }

        private List<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie{Id=1,Name="Breaking Bad"},
                new Movie{Id=2,Name="Life of Pie"},
                new Movie{Id=3,Name="Contagion"}
            };
        }
        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year+" / "+month);
        }
    }
}