using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class GeoData
    {
        /// <summary>
        /// Unieke BAG id
        /// </summary>
        [DataMember(Name = "addresseerbaarObjectId")]
        public string AddresseerbaarObjectId { get; set; }

        /// <summary>
        /// Unieke BAG nummeraanduiding id.
        /// </summary>
        [DataMember(Name = "nummerAanduidingId")]
        public string NummerAanduidingId { get; set; }

        /// <summary>
        /// Lengtegraad.
        /// </summary>
        [DataMember(Name = "gpsLatitude")]
        public double? GpsLatitude { get; set; }

        /// <summary>
        /// Breedtegraad.
        /// </summary>
        [DataMember(Name = "gpsLongitude")]
        public double? GpsLongitude { get; set; }

        /// <summary>
        /// Rijksdriehoek X-coördinaat.
        /// </summary>
        [DataMember(Name = "rijksdriehoekX")]
        public double? RijksdriehoekX { get; set; }

        /// <summary>
        /// Rijksdriehoek Y-coördinaat.
        /// </summary>
        [DataMember(Name = "rijksdriehoekY")]
        public double? RijksdriehoekY { get; set; }

        /// <summary>
        /// Rijksdriehoek Z-coördinaat.
        /// </summary>
        [DataMember(Name = "rijksdriehoekZ")]
        public double? RijksdriehoekZ { get; set; }
    }
}
