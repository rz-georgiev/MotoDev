using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MotoDev.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (field != null)
            {
                var attribute = field.GetCustomAttribute<DisplayAttribute>();
                if (attribute != null)
                {
                    return attribute?.Name ?? string.Empty;
                }
            }
            return enumValue.ToString();
        }
    }
}