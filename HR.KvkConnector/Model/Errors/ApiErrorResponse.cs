using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Model.Errors
{
    [DataContract]
    internal class ApiErrorResponse : IEnumerable<ApiError>
    {
        /// <summary>
        /// The individual errors that were returned by the API.
        /// </summary>
        [DataMember(Name = "fout")]
        public IEnumerable<ApiError> Errors { get; set; } = Enumerable.Empty<ApiError>();

        /// <inheritdoc/>
        public IEnumerator<ApiError> GetEnumerator() => Errors.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Errors).GetEnumerator();

        /// <inheritdoc/>
        public override string ToString() => string.Join(Environment.NewLine, Errors.Select(e => e.ToString()));

        [OnDeserializing]
        protected void OnDeserializing(StreamingContext context)
        {
            Errors = Enumerable.Empty<ApiError>();
        }
    }
}
