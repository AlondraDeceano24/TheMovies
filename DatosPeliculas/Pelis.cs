using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pelis
    {
        public int page { get; set; }

        public List<DatosPelicula> results { get; set; }

    }

    public class DatosPelicula
    {
        public bool adult { get; set; }

        public string backdrop_path { get; set; }

        public List<int> genre_ids { get; set; }

        public int id { get; set; }

        public string original_title { get; set; }

        public string overview { get; set; }

        public string poster_path { get; set; }
    }
}



