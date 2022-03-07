using HR.KvkConnector.Infrastructure;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model.Zoeken
{
    [DataContract]
    public class ResultaatItem
    {
        /// <summary>
        /// Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.
        /// </summary>
        [DataMember(Name = "kvkNummer")]
        public string KvkNummer { get; set; }

        /// <summary>
        /// Rechtspersonen Samenwerkingsverbanden Informatie Nummer.
        /// </summary>
        [DataMember(Name = "rsin")]
        public string Rsin { get; set; }

        /// <summary>
        /// Vestigingsnummer: uniek nummer dat bestaat uit 12 cijfers.
        /// </summary>
        [DataMember(Name = "vestigingsnummer")]
        public string Vestigingsnummer { get; set; }

        /// <summary>
        /// De naam waaronder een vestiging of rechtspersoon handelt.
        /// </summary>
        [DataMember(Name = "handelsnaam")]
        public string Handelsnaam { get; set; }

        [DataMember(Name = "straatnaam")]
        public string Straatnaam { get; set; }

        [DataMember(Name = "huisnummer")]
        public int? Huisnummer { get; set; }

        [DataMember(Name = "huisnummerToevoeging")]
        public string HuisnummerToevoeging { get; set; }

        [DataMember(Name = "postcode")]
        public string Postcode { get; set; }

        [DataMember(Name = "plaats")]
        public string Plaats { get; set; }

        /// <summary>
        /// hoofdvestiging/nevenvestiging/rechtspersoon
        /// </summary>
        public Vestigingstype? Type { get; set; }
        
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
                    Type = Enum.GetValues(typeof(Vestigingstype))
                        .Cast<Vestigingstype?>()
                        .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        /// <summary>
        /// Indicatie of inschrijving actief is.
        /// </summary>
        public JaNeeIndicatie? Actief { get; set; }

        [DataMember(Name = "actief")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string ActiefString
        {
            get => Actief?.GetStringValue();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Actief = null;
                }
                else
                {
                    Actief = Enum.GetValues(typeof(JaNeeIndicatie))
                        .Cast<JaNeeIndicatie?>()
                        .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        /// <summary>
        /// Bevat de vervallen handelsnaam of statutaire naam waar dit zoekresultaat mee gevonden is.
        /// </summary>
        [DataMember(Name = "vervallenNaam")]
        public string VervallenNaam { get; set; }

        [DataMember(Name = "links")]
        public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();

        [OnDeserializing]
        protected void OnDeserializing(StreamingContext context)
        {
            Links = Enumerable.Empty<Link>();
        }
    }
}
