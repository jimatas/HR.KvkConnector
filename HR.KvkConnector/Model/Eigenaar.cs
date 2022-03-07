using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class Eigenaar
    {
        /// <summary>
        /// Rechtspersonen Samenwerkingsverbanden Informatie Nummer.
        /// </summary>
        [DataMember(Name = "rsin")]
        public string Rsin { get; set; }

        [DataMember(Name = "rechtsvorm")]
        public string Rechtsvorm { get; set; }

        [DataMember(Name = "uitgebreideRechtsvorm")]
        public string UitgebreideRechtsvorm { get; set; }

        [DataMember(Name = "adressen")]
        public IEnumerable<Adres> Adressen { get; set; } = Enumerable.Empty<Adres>();

        [DataMember(Name = "links")]
        public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();

        [OnDeserializing]
        protected void OnDeserializing(StreamingContext context)
        {
            Adressen = Enumerable.Empty<Adres>();
            Links = Enumerable.Empty<Link>();
        }
    }
}
