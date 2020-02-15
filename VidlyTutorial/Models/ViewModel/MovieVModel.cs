using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VidlyTutorial.Models.ViewModel
{
    public class MovieVModel
    {
        public Movie movie { get; set; }
        public IEnumerable<Genre> genres { get; set; }
    }
}