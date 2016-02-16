using System;
using System.Reflection;

namespace BernieApp.Portable.Helpers
{
    public static class AttributeHelpers
    {
        public static TAttribute GetEnumAttribute<TAttribute>(this object val) where TAttribute : Attribute
        {
            var enumTypeInfo = val.GetType().GetTypeInfo();
            var field = enumTypeInfo.GetDeclaredField(val.ToString());
            return field.GetCustomAttribute<TAttribute>();
        }
    }
}
