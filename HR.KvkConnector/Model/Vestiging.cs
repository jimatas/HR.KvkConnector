using HR.KvkConnector.Infrastructure;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class Vestiging
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
        /// Rechtspersonen Samenwerkingsverbanden Informatie Nummer.
        /// </summary>
        [DataMember(Name = "rsin")]
        public string Rsin { get; set; }

        /// <summary>
        /// Hiermee geeft de onderneming aan geen ongevraagde reclame per post of verkoop aan de deur te willen ontvangen.
        /// </summary>
        public JaNeeIndicatie? IndNonMailing { get; set; }
        
        [DataMember(Name = "indNonMailing")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string IndNonMailingString
        {
            get => IndNonMailing?.GetStringValue();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    IndNonMailing = null;
                }
                else
                {
                    IndNonMailing = Enum.GetValues(typeof(JaNeeIndicatie))
                        .Cast<JaNeeIndicatie?>()
                        .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        /// <summary>
        /// Registratiedatum onderneming in HR.
        /// </summary>
        [DataMember(Name = "formeleRegistratiedatum")]
        public DateTime? FormeleRegistratiedatum { get; set; }

        [DataMember(Name = "materieleRegistratie")]
        public MaterieleRegistratie MaterieleRegistratie { get; set; }

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

        /// <summary>
        /// Aantal voltijd werkzame personen.
        /// </summary>
        [DataMember(Name = "voltijdWerkzamePersonen")]
        public int? VoltijdWerkzamePersonen { get; set; }

        /// <summary>
        /// Totaal aantal werkzame personen.
        /// </summary>
        [DataMember(Name = "totaalWerkzamePersonen")]
        public int? TotaalWerkzamePersonen { get; set; }

        /// <summary>
        /// Aantal deeltijd werkzame personen.
        /// </summary>
        [DataMember(Name = "deeltijdWerkzamePersonen")]
        public int? DeeltijdWerkzamePersonen { get; set; }

        [DataMember(Name = "adressen")]
        public IEnumerable<Adres> Adressen { get; set; } = Enumerable.Empty<Adres>();

        [DataMember(Name = "websites")]
        public IEnumerable<string> Websites { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// Code beschrijving van SBI activiteiten conform SBI 2008 (Standard Industrial Classification). 
        /// Er wordt geen maximering toegepast in de resultaten. Zie ook KVK.nl/sbi
        /// </summary>
        [DataMember(Name = "sbiActiviteiten")]
        public IEnumerable<SbiActiviteit> SbiActiviteiten { get; set; } = Enumerable.Empty<SbiActiviteit>();

        [DataMember(Name = "links")]
        public IEnumerable<Link> Links { get; set; } = Enumerable.Empty<Link>();

        [OnDeserializing]
        protected void OnDeserializing(StreamingContext context)
        {
            Adressen = Enumerable.Empty<Adres>();
            Websites = Enumerable.Empty<string>();
            SbiActiviteiten = Enumerable.Empty<SbiActiviteit>();
            Links = Enumerable.Empty<Link>();
        }
    }
}
