using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Musicalog.Web.Models
{
    public class AlbumViewModel
    {
        public AlbumViewModel()
        {
            this.MediaTypes = new Dictionary<int, string>();
        }

        public int Id { get; set; }

        public string AlbumName { get; set; }

        public string Artist { get; set; }

        public string MediaType { get; set; }

        public string RecordLabel { get; set; }

        public int Stock { get; set; }

        public Dictionary<int, string> MediaTypes { get; set; }
    }
}