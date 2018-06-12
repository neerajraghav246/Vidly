using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieViewModel
    {
        public Movie movie{ get; set; }
        public IEnumerable<Genere> generes { get; set; }
    }
}