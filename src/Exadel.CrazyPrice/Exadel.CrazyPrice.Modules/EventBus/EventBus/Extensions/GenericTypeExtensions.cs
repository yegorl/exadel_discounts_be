using System;
using System.Linq;

namespace Exadel.CrazyPrice.Modules.EventBus.Extensions
{
    public static class GenericTypeExtensions
    {
        public static string GetGenericTypeName(this Type type) =>
            type.IsGenericType
                ? $"{type.Name.Remove(type.Name.IndexOf('`'))}<{string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray())}>"
                : type.Name;

        public static string GetGenericTypeName(this object @object) =>
            @object.GetType().GetGenericTypeName();
    }
}
