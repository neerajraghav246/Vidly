using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie movie { get; set; }
        public IEnumerable<Genere> generes { get; set; }
        public string FormTitle => (movie != null && movie.Id != 0) ? "Edit" : "Create";
    }
}