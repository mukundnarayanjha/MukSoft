using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MukSoft.Core.Extensions
{

    public static class EnumExtensions
    {
        [ExcludeFromCodeCoverage]
        public static string GetDisplayName(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Name : enu.ToString();
        }
        [ExcludeFromCodeCoverage]
        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            var type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"Type {type} is not an enum");
            }
            var field = type.GetField(value.ToString());
            return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
        }
    }

}