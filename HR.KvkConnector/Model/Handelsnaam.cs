using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class Handelsnaam
    {
        [DataMember(Name = "naam")]
        public string Naam { get; set; }

        [DataMember(Name = "volgorde")]
        public int? Volgorde { get; set; }
    }
}
