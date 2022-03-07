using System.Runtime.Serialization;

namespace HR.KvkConnector.Model.Errors
{
    [DataContract]
    internal class HttpError
    {
        [DataMember(Name = "status")]
        public int? StatusCode { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "requestId")]
        public string RequestId { get; set; }

        /// <inheritdoc/>
        public override string ToString() => $"HTTP {StatusCode}: {Message} ({RequestId})";

        public ApiError ToApiError() => new ApiError
        {
            Code = $"HTTP {StatusCode}",
            Message = $"{Message} ({RequestId})"
        };
    }
}
