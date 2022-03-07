using HR.KvkConnector.Infrastructure;

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class Adres
    {
        /// <summary>
        /// Correspondentieadres en/of bezoekadres.
        /// </summary>
        public Adrestype? Type { get; set; }

        [DataMember(Name = "type")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string TypeString
        {
            get => Type?.GetStringValue();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Type = null;
                }
                else
                {
                    Type = Enum.GetValues(typeof(Adrestype))
                        .Cast<Adrestype?>()
                        .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        /// <summary>
        /// Indicatie of het adres is afgeschermd.
        /// </summary>
        public JaNeeIndicatie? IndAfgeschermd { get; set; }

        [DataMember(Name = "indAfgeschermd")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string IndAfgeschermdString
        {
            get => IndAfgeschermd?.GetStringValue();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    IndAfgeschermd = null;
                }
                else
                {
                    IndAfgeschermd = Enum.GetValues(typeof(JaNeeIndicatie))
                        .Cast<JaNeeIndicatie?>()
                        .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        [DataMember(Name = "volledigAdres")]
        public string VolledigAdres { get; set; }

        [DataMember(Name = "straatnaam")]
        public string Straatnaam { get; set; }

        [DataMember(Name = "huisnummer")]
        public int? Huisnummer { get; set; }

        [DataMember(Name = "huisnummerToevoeging")]
        public string HuisnummerToevoeging { get; set; }

        [DataMember(Name = "huisletter")]
        public string Huisletter { get; set; }

        [DataMember(Name = "aanduidingBijHuisnummer")]
        public string AanduidingBijHuisnummer { get; set; }

        [DataMember(Name = "toevoegingAdres")]
        public string ToevoegingAdres { get; set; }

        [DataMember(Name = "postcode")]
        public string Postcode { get; set; }

        [DataMember(Name = "postbusnummer")]
        public int? Postbusnummer { get; set; }

        [DataMember(Name = "plaats")]
        public string Plaats { get; set; }

        [DataMember(Name = "straatHuisnummer")]
        public string StraatHuisnummer { get; set; }

        [DataMember(Name = "postcodeWoonplaats")]
        public string PostcodeWoonplaats { get; set; }

        [DataMember(Name = "regio")]
        public string Regio { get; set; }

        [DataMember(Name = "land")]
        public string Land { get; set; }

        /// <summary>
        /// Basisregistratie Adressen en Gebouwen gegevens uit het kadaster.
        /// </summary>
        [DataMember(Name = "geoData")]
        public GeoData GeoData { get; set; }
    }
}
