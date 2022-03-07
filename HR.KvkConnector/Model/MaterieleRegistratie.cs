using System;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class MaterieleRegistratie
    {
        /// <summary>
        /// Startdatum onderneming.
        /// </summary>
        [DataMember(Name = "datumAanvang")]
        public DateTime? DatumAanvang { get; set; }

        /// <summary>
        /// Einddatum onderneming.
        /// </summary>
        [DataMember(Name = "datumEinde")]
        public DateTime? DatumEinde { get; set; }
    }
}
