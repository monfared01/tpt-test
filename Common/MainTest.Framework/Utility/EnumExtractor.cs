using System.ComponentModel;
using System.Reflection;

namespace MainTest.Framework.Utility
{
    public class EnumExtractor
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute =
                (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}