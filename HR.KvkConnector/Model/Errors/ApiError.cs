using System.Runtime.Serialization;

namespace HR.KvkConnector.Model.Errors
{
    [DataContract]
    public class ApiError
    {
        /// <summary>
        /// A unique error code.
        /// </summary>
        [DataMember(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// A short explanation of what went wrong.
        /// </summary>
        [DataMember(Name = "omschrijving")]
        public string Message { get; set; }

        /// <inheritdoc/>
        public override string ToString() => $"{Code}: {Message}";
    }
}
