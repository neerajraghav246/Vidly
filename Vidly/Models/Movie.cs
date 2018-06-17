using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name= "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Required]
        [Display(Name = "Stock")]
        [Range(1,20)]
        public short NumberInStock { get; set; }       
        
        public Genere AssociatedGenere { get; set; }

        [Display(Name = "Genere")]
        [Required]
        public byte GenereId { get; set; }
    }
}