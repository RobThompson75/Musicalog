using Musicalog.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Musicalog.Web.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Musicalog" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Musicalog.svc or Musicalog.svc.cs at the Solution Explorer and start debugging.
    public class Musicalog : IMusicalog
    {
        Data.Context context;

        public Musicalog()
        {
            this.context = new Data.Context();
        }

        public Album SelectAlbumById(int id)
        {
            return this.context.Albums.Find(id);
        }

        public IEnumerable<Album> SelectAllAlbums()
        {
            return this.context.Albums.ToList();
        }

        public IEnumerable<Artist> SelectAllArtists()
        {
            return this.context.Artists.ToList();
        }

        public IEnumerable<RecordLabel> SelectAllLabels()
        {
            return this.context.RecordLabels.ToList();
        }

        public IEnumerable<MediaType> SelectAllMediaTypes()
        {
            return this.context.MediaTypes.ToList();
        }

        public Artist SelectArtistById(int id)
        {
            return this.context.Artists.Find(id);
        }

        public RecordLabel SelectLabelById(int id)
        {
            return this.context.RecordLabels.Find(id);
        }

        public MediaType SelectMediaTypeById(int id)
        {
            return this.context.MediaTypes.Find(id);
        }
    }
}
