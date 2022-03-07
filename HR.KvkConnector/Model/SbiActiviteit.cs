using HR.KvkConnector.Infrastructure;

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class SbiActiviteit
    {
        [DataMember(Name = "sbiCode")]
        public string Code { get; set; }

        [DataMember(Name = "sbiOmschrijving")]
        public string Omschrijving { get; set; }

        public JaNeeIndicatie? IndHoofdactiviteit { get; set; }

        [DataMember(Name = "indHoofdactiviteit")]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string IndHoofdactiviteitString
        {
            get => IndHoofdactiviteit?.GetStringValue();
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    IndHoofdactiviteit = null;
                }
                else
                {
                    IndHoofdactiviteit = Enum.GetValues(typeof(JaNeeIndicatie))
                        .Cast<JaNeeIndicatie?>()
                        .FirstOrDefault(e => e.GetStringValue().Equals(value, StringComparison.OrdinalIgnoreCase));
                }
            }
        }
    }
}
