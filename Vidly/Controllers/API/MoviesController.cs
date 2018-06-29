using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetMovies() => Ok(_context.Movies.Include(m=>m.AssociatedGenere).ToList().Select(Mapper.Map<Movie, MovieDto>));

        public IHttpActionResult GetMovie(int id)
        {
            var _movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (_movie == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No Movie found with id={id}"),
                    ReasonPhrase = "Movie Not Found"
                };

                throw new HttpResponseException(response);
                // throw new Exception("Error");
            }
            return Ok(Mapper.Map<Movie, MovieDto>(_movie));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Movie with Name={movieDto.Name} has invalid data", System.Text.Encoding.UTF8, "text/plain"),
                    ReasonPhrase = "Invalid Movie data"
                };
                HttpResponseException responseException = new HttpResponseException(response);
                ModelState.AddModelError("ErrorMessage", responseException);
                throw responseException;
            }
            try
            {
                var movie = Mapper.Map<MovieDto, Movie>(movieDto);
                _context.Movies.Add(movie);
                _context.SaveChanges();
                movieDto.Id = movie.Id;
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error while saving movie.Error Message: {ex.Message}"),
                    ReasonPhrase = "Unexpected error occured"
                };
                throw new HttpResponseException(response);
            }
            return Created(Request.RequestUri + "/" + movieDto.Id, movieDto);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Movie with ID={id} has invalid data", System.Text.Encoding.UTF8, "text/plain"),
                    ReasonPhrase = "Invalid Movie data"
                };
                throw new HttpResponseException(response);
            }

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"Movie not found with ID={id} in our database", System.Text.Encoding.UTF8, "text/plain"),
                    ReasonPhrase = "Movie Not Found",

                };
                throw new HttpResponseException(response);
            }
            try
            {

                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<MovieDto, Movie>().ForMember(src => src.Id, opt => opt.Ignore());
                //});

                //IMapper mapper = config.CreateMapper();
                Mapper.Map(movieDto, movieInDb);
                _context.SaveChanges();
                return Ok(movieDto);
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
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
           
                var movieInDB = _context.Movies.SingleOrDefault(c => c.Id == id);
                if (movieInDB == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent($"Movie not found with ID={id} in our database"),
                        ReasonPhrase = "Movie Not Found"
                    };
                    throw new HttpResponseException(response);
                }
            try
            {
                _context.Movies.Remove(movieInDB);
                _context.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error while deleting movie with ID={id}.Error Message: {ex.Message}"),
                    ReasonPhrase = "Unexpected error occured"
                };
                throw new HttpResponseException(response);
            }
        }
    }
}
