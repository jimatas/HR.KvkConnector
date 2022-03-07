using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class VestigingList
    {
        /// <summary>
        /// Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.
        /// </summary>
        [DataMember(Name = "kvkNummer")]
        public string KvkNummer { get; set; }

        [DataMember(Name = "aantalCommercieleVestigingen")]
        public int? AantalCommercieleVestigingen { get; set; }

        [DataMember(Name = "aantalNietCommercieleVestigingen")]
        public int? AantalNietCommercieleVestigingen { get; set; }

        [DataMember(Name = "totaalAantalVestigingen")]
        public int? TotaalAantalVestigingen { get; set; }

        [DataMember(Name = "vestigingen")]
        public IEnumerable<VestigingBasis> Vestigingen { get; set; } = Enumerable.Empty<VestigingBasis>();

        [DataMember(Name = "links")]
        public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();

        [OnDeserializing]
        protected void OnDeserializing(StreamingContext context)
        {
            Vestigingen = Enumerable.Empty<VestigingBasis>();
            Links = Enumerable.Empty<Link>();
        }
    }
}
