using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musicalog.Web.Models
{
    public class DataPage<T>
    {
        public List<T> Data { get; set; }

        public bool HasPrevious { get; set; }

        public bool HasNext { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }
    }
}