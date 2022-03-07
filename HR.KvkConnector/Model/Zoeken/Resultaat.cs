using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model.Zoeken
{
    [DataContract]
    public class Resultaat
    {
        /// <summary>
        /// Geeft aan op welke pagina je bent. Start vanaf pagina 1.
        /// </summary>
        [DataMember(Name = "pagina")]
        public int? Pagina { get; set; }

        /// <summary>
        /// Geeft het aantal zoek resultaten per pagina weer.
        /// </summary>
        [DataMember(Name = "aantal")]
        public int? Aantal { get; set; }

        /// <summary>
        /// Totaal aantal zoekresultaten gevonden. De API Zoeken toont max. 1000 resultaten.
        /// </summary>
        [DataMember(Name = "totaal")]
        public int? Totaal { get; set; }

        /// <summary>
        /// Link naar de vorige pagina indien beschikbaar.
        /// </summary>
        [DataMember(Name = "vorige")]
        public string Vorige { get; set; }

        /// <summary>
        /// Link naar de volgende pagina indien beschikbaar.
        /// </summary>
        [DataMember(Name = "volgende")]
        public string Volgende { get; set; }

        [DataMember(Name = "resultaten")]
        public IEnumerable<ResultaatItem> Resultaten { get; set; } = Enumerable.Empty<ResultaatItem>();

        [DataMember(Name = "links")]
        public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();

        [OnDeserializing]
        protected void OnDeserializing(StreamingContext context)
        {
            Resultaten = Enumerable.Empty<ResultaatItem>();
            Links = Enumerable.Empty<Link>();
        }
    }
}
