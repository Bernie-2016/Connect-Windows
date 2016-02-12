using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BernieApp.Common.Helpers
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
