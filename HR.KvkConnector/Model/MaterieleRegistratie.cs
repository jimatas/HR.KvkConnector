using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class MaterieleRegistratie
    {
        /// <summary>
        /// Startdatum onderneming.
        /// </summary>
        public DateTime? DatumAanvang
        {
            get
            {
                DateTime.TryParseExact(DatumAanvraagAsText, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);
                return result;
            }
        }


        [DataMember(Name = "datumAanvang")]
        public string DatumAanvraagAsText { get; set; }

        /// <summary>
        /// Einddatum onderneming.
        /// </summary>
        [DataMember(Name = "datumEinde")]
        public DateTime? DatumEinde { get; set; }
    }
}