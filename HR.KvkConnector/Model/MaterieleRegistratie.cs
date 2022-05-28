using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class MaterieleRegistratie
    {
        /// <summary>
        /// Startdatum onderneming.
        /// </summary>
        public DateTime? DatumAanvang { get; set; }

        /// <summary>
        /// The API seems to be returning invalidly formatted dates for the <see cref="DatumAanvang"/> property from time to time (e.g., "19290000").
        /// Parsing the date manually in this shadow property to prevent a runtime exception from being thrown.
        /// </summary>
        [DataMember(Name = "datumAanvang")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string DatumAanvangString
        {
            get => DatumAanvang?.ToString("yyyyMMdd");
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    DatumAanvang = null;
                }
                else
                {
                    DateTime.TryParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);
                    DatumAanvang = result;
                }
            }
        }

        /// <summary>
        /// Einddatum onderneming.
        /// </summary>
        [DataMember(Name = "datumEinde")]
        public DateTime? DatumEinde { get; set; }
    }
}
