using System.Runtime.Serialization;

namespace HR.KvkConnector.Model
{
    public enum Adrestype
    {
        [EnumMember(Value = "correspondentieadres")]
        Correspondentieadres = 1,

        [EnumMember(Value = "bezoekadres")]
        Bezoekadres
    }
}
