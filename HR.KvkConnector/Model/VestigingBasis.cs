using HR.KvkConnector.Infrastructure;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class VestigingBasis
    {
        /// <summary>
        /// Vestigingsnummer: uniek nummer dat bestaat uit 12 cijfers.
        /// </summary>
        [DataMember(Name = "vestigingsnummer")]
        public string Vestigingsnummer { get; set; }

        /// <summary>
        /// Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.
        /// </summary>
        [DataMember(Name = "kvkNummer")]
        public string KvkNummer { get; set; }

        /// <summary>
        /// De naam waaronder een onderneming of vestiging handelt.
        /// </summary>
        [DataMember(Name = "eersteHandelsnaam")]
        public string EersteHandelsnaam { get; set; }

        /// <summary>
        /// Hoofdvestiging (Ja/Nee)
        /// </summary>
        public JaNeeIndicatie? IndHoofdvestiging { get; set; }

        [DataMember(Name = "indHoofdvestiging")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string IndHoofdvestigingString
        {
            get => IndHoofdvestiging?.GetStringValue();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    IndHoofdvestiging = null;
                }
                else
                {
                    IndHoofdvestiging = Enum.GetValues(typeof(JaNeeIndicatie))
                        .Cast<JaNeeIndicatie?>()
                        .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        /// <summary>
        /// Indicatie of het adres is afgeschermd.
        /// </summary>
        public JaNeeIndicatie? IndAdresAfgeschermd { get; set; }

        [DataMember(Name = "indAdresAfgeschermd")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string IndAdresAfgeschermdString
        {
            get => IndAdresAfgeschermd?.GetStringValue();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    IndAdresAfgeschermd = null;
                }
                else
                {
                    IndAdresAfgeschermd = Enum.GetValues(typeof(JaNeeIndicatie))
                        .Cast<JaNeeIndicatie?>()
                        .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        /// <summary>
        /// Commerciele vestiging (Ja/Nee)
        /// </summary>
        public JaNeeIndicatie? IndCommercieleVestiging { get; set; }

        [DataMember(Name = "indCommercieleVestiging")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string IndCommercieleVestigingString
        {
            get => IndCommercieleVestiging?.GetStringValue();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    IndCommercieleVestiging = null;
                }
                else
                {
                    IndCommercieleVestiging = Enum.GetValues(typeof(JaNeeIndicatie))
                        .Cast<JaNeeIndicatie?>()
                        .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        [DataMember(Name = "volledigAdres")]
        public string VolledigAdres { get; set; }

        [DataMember(Name = "links")]
        public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();

        [OnDeserializing]
        protected void OnDeserializing(StreamingContext context)
        {
            Links = Enumerable.Empty<Link>();
        }
    }
}
