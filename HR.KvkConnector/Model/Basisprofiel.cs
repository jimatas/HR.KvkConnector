using HR.KvkConnector.Infrastructure;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class Basisprofiel
    {
        /// <summary>
        /// Nederlands Kamer van Koophandel nummer: bestaat uit 8 cijfers.
        /// </summary>
        [DataMember(Name = "kvkNummer")]
        public string KvkNummer { get; set; }

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
        /// Naam onder Maatschappelijke Activiteit.
        /// </summary>
        [DataMember(Name = "naam")]
        public string Naam { get; set; }

        /// <summary>
        /// Registratiedatum onderneming in HR.
        /// </summary>
        [DataMember(Name = "formeleRegistratiedatum")]
        public DateTime? FormeleRegistratiedatum { get; set; }

        [DataMember(Name = "materieleRegistratie")]
        public MaterieleRegistratie MaterieleRegistratie { get; set; }

        /// <summary>
        /// Totaal aantal werkzame personen.
        /// </summary>
        [DataMember(Name = "totaalWerkzamePersonen")]
        public int? TotaalWerkzamePersonen { get; set; }

        /// <summary>
        /// De naam van de onderneming wanneer er statuten geregistreerd zijn.
        /// </summary>
        [DataMember(Name = "statutaireNaam")]
        public string StatutaireNaam { get; set; }

        /// <summary>
        /// Alle namen waaronder een onderneming of vestiging handelt (op volgorde van registreren).
        /// </summary>
        [DataMember(Name = "handelsnamen")]
        public IEnumerable<Handelsnaam> Handelsnamen { get; set; } = Enumerable.Empty<Handelsnaam>();

        /// <summary>
        /// Code beschrijving van SBI activiteiten conform SBI 2008 (Standard Industrial Classification). 
        /// Er wordt geen maximering toegepast in de resultaten. Zie ook KVK.nl/sbi
        /// </summary>
        [DataMember(Name = "sbiActiviteiten")]
        public IEnumerable<SbiActiviteit> SbiActiviteiten { get; set; } = Enumerable.Empty<SbiActiviteit>();

        [DataMember(Name = "_embedded")]
        public EmbeddedContainer Embedded { get; set; }

        [OnDeserializing]
        protected void OnDeserializing(StreamingContext context)
        {
            Handelsnamen = Enumerable.Empty<Handelsnaam>();
            SbiActiviteiten = Enumerable.Empty<SbiActiviteit>();
        }
    }
}
