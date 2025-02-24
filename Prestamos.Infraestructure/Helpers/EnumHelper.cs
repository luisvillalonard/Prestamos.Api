using Prestamos.Core.Attributos;
using System.ComponentModel;
using System.Reflection;

namespace Prestamos.Infraestructure.Helpers
{
    public static class EnumHelper
    {
        public static MenuAttribute? GetMenuAttribute(this Enum value)
        {
            var type = value.GetType();
            if (type == null)
                return null;

            FieldInfo? field = type.GetField(value.ToString());
            if (field == null)
                return null;

            MenuAttribute[] customAttributes = (MenuAttribute[])field.GetCustomAttributes(typeof(MenuAttribute), false);
            if (customAttributes.Length == 0)
                return null;

            return customAttributes[0];
        }

        public static string GetDescripcion(this Enum value)
        {
            var type = value.GetType();
            if (type == null)
                return string.Empty;

            FieldInfo? field = type.GetField(value.ToString());
            if (field == null)
                return string.Empty;

            DescriptionAttribute[] customAttribute = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return customAttribute.Length > 0 ? customAttribute[0].Description : value.ToString();
        }
    }
}
