using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    [DataContract]
    public class Link
    {
        [DataMember(Name = "rel")]
        public string Rel { get; set; }

        [DataMember(Name = "href")]
        public string Href { get; set; }

        [DataMember(Name = "hreflang")]
        public string Hreflang { get; set; }

        [DataMember(Name = "media")]
        public string Media { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "deprecation")]
        public string Deprecation { get; set; }

        [DataMember(Name = "profile")]
        public string Profile { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
