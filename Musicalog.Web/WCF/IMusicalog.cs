using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Musicalog.Web.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMusicalog" in both code and config file together.
    [ServiceContract]
    public interface IMusicalog
    {
        [OperationContract]
        IEnumerable<Data.Model.Album> SelectAllAlbums();

        [OperationContract]
        Data.Model.Album SelectAlbumById(int id);

        [OperationContract]
        Data.Model.Artist SelectArtistById(int id);

        [OperationContract]
        Data.Model.RecordLabel SelectLabelById(int id);

        [OperationContract]
        Data.Model.MediaType SelectMediaTypeById(int id);

        [OperationContract]
        IEnumerable<Data.Model.MediaType> SelectAllMediaTypes();

        [OperationContract]
        IEnumerable<Data.Model.RecordLabel> SelectAllLabels();

        [OperationContract]
        IEnumerable<Data.Model.Artist> SelectAllArtists();
    }
}
