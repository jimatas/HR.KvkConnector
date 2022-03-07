using HR.KvkConnector.Infrastructure;

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace HR.KvkConnector.Model.Zoeken
{
    [DataContract]
    public class Parameters
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

        [DataMember(Name = "plaats")]
        public string Plaats { get; set; }

        /// <summary>
        /// Mag alleen in combinatie met Huisnummer gezocht worden.
        /// </summary>
        [DataMember(Name = "postcode")]
        public string Postcode { get; set; }

        /// <summary>
        /// Mag alleen in combinatie met Postcode gezocht worden.
        /// </summary>
        [DataMember(Name = "huisnummer")]
        public string Huisnummer { get; set; }

        /// <summary>
        /// Filter op type: hoofdvestiging, nevenvestiging en/of rechtspersoon.
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
        /// Inclusief inactieve registraties: true, false.
        /// </summary>
        [DataMember(Name = "inclusiefInactieveRegistraties")]
        public bool? InclusiefInactieveRegistraties { get; set; }

        /// <summary>
        /// Paginanummer, minimaal 1 en maximaal 1000.
        /// Default value: 1
        /// </summary>
        [DataMember(Name = "pagina")]
        public int? Pagina { get; set; }

        /// <summary>
        /// Kies het aantal resultaten per pagina, minimaal 1 en maximaal 100.
        /// Default value: 10
        /// </summary>
        [DataMember(Name = "aantal")]
        public int? Aantal { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var queryStringBuilder = new StringBuilder();
            char separator = '?';

            foreach (var property in GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var dataMember = property.GetCustomAttribute<DataMemberAttribute>();
                if (dataMember != null)
                {
                    var propertyName = dataMember.Name;
                    var propertyValue = property.GetValue(this);

                    if (propertyValue != null && propertyValue.ToString().Length != 0)
                    {
                        queryStringBuilder.Append(separator).Append(propertyName).Append('=').Append(Uri.EscapeDataString(propertyValue.ToString()));
                        separator = '&';
                    }
                }
            }

            return queryStringBuilder.ToString();
        }
    }
}
