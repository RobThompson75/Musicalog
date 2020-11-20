using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Musicalog.Data.Model
{
    [DataContract]
    public class MediaType
    {
        public MediaType()
        {
            this.Albums = new List<Album>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ICollection<Album> Albums { get; set; }
    }
}