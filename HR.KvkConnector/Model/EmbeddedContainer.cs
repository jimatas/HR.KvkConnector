using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class EmbeddedContainer
    {
        [DataMember(Name = "hoofdvestiging")]
        public Vestiging Hoofdvestiging { get; set; }

        [DataMember(Name = "eigenaar")]
        public Eigenaar Eigenaar { get; set; }
    }
}
