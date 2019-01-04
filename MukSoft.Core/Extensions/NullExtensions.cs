using System;
using System.Diagnostics.CodeAnalysis;

namespace MukSoft.Core.Extensions
{
    public static class NullExtensions
    {
        [ExcludeFromCodeCoverage]
        public static void ThrowIfNull<T>(this T obj, string parameterName)
            where T : class
        {
            if (obj == null) throw new ArgumentNullException(parameterName);
        }

        [ExcludeFromCodeCoverage]
        public static void ThrowIfNullOrEmpty(this string obj, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(obj)) throw new ArgumentException(parameterName);
        }
        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Checks if the argument is null.
        /// </summary>
        public static void CheckArgumentIsNull(this object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }

    }
}
