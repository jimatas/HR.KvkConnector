using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HR.KvkConnector.Infrastructure
{
    /// <summary>
    /// Static wrapper around the <see cref="DataContractJsonSerializer"/> class for easily converting .NET objects to and from JSON string data.
    /// </summary>
    internal static class JsonSerializer
    {
        /// <summary>
        /// Serialize an object of type <typeparamref name="T"/> to a JSON string.
        /// </summary>
        /// <typeparam name="T">The target object's type.</typeparam>
        /// <param name="objectGraph">The object to serialize.</param>
        /// <param name="encoding">The character encoding to use. Defaults to UTF-8 if not specified.</param>
        /// <returns>The resulting JSON string.</returns>
        public static string Serialize<T>(T objectGraph, Encoding encoding = null)
            where T : class
        {
            if (objectGraph is null)
            {
                return string.Empty;
            }

            using (var memoryStream = new MemoryStream())
            {
                CreateDataContractJsonSerializer<T>().WriteObject(memoryStream, objectGraph);

                return (encoding ?? Encoding.UTF8).GetString(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Deserialize a JSON string to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The target object's type.</typeparam>
        /// <param name="jsonString">The JSON string to deserialize.</param>
        /// <param name="encoding">The character encoding to use. Defaults to UTF-8 if not specified.</param>
        /// <returns>The resulting object.</returns>
        public static T Deserialize<T>(string jsonString, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return default;
            }

            using (var memoryStream = new MemoryStream((encoding ?? Encoding.UTF8).GetBytes(jsonString)))
            {
                return (T)CreateDataContractJsonSerializer<T>().ReadObject(memoryStream);
            }
        }

        private static DataContractJsonSerializer CreateDataContractJsonSerializer<T>()
        {
            return new DataContractJsonSerializer(typeof(T),
                new DataContractJsonSerializerSettings()
                {
                    UseSimpleDictionaryFormat = true,
                    DateTimeFormat = new DateTimeFormat("yyyyMMdd")
                });
        }
    }
}
