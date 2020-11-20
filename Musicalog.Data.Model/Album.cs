using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Musicalog.Data.Model
{
    [DataContract]
    public class Album
    {
        public Album()
        {
            this.MediaTypes = new List<MediaType>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ArtistId { get; set; }

        [DataMember]
        public int RecordLabelId { get; set; }

        [DataMember]
        public int Stock { get; set; }

        [DataMember]
        public virtual Artist Artist { get; set; }

        [DataMember]
        public virtual RecordLabel Label { get; set; }

        [DataMember]
        public virtual ICollection<MediaType> MediaTypes { get; private set; }
    }
}
