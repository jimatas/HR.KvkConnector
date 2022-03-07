using System;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model.Zoeken
{
    [Flags]
    public enum Vestigingstype
    {
        [EnumMember(Value = "hoofdvestiging")]
        Hoofdvestiging = 1 << 0,

        [EnumMember(Value = "nevenvestiging")]
        Nevenvestiging = 1 << 1,

        [EnumMember(Value = "rechtspersoon")]
        Rechtspersoon = 1 << 2
    }
}
