using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace HR.KvkConnector.Infrastructure
{
    internal static class EnumExtensions
    {
        /// <summary>
        /// Returns the string value that has been associated with the enum member through the <see cref="EnumMemberAttribute"/> attribute, 
        /// or if that attribute is not present, the result of calling <see cref="Enum.ToString()"/> on it.
        /// </summary>
        /// <remarks>This method handles <see cref="FlagsAttribute"/> enums correctly.</remarks>
        /// <param name="enumMember">The enum member to get the string value of.</param>
        /// <returns>The string value of the enum member.</returns>
        public static string GetStringValue(this Enum enumMember)
        {
            return string.Join(", ", enumMember.ToString().Split(',').Select(value => value.Trim())
                .Select(value => enumMember.GetType().GetField(value).GetCustomAttribute<EnumMemberAttribute>()?.Value ?? value));
        }
    }
}
