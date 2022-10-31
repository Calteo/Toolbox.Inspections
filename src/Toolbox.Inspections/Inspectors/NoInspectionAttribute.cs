using System.Reflection;

namespace Toolbox.Inspectors
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class NoInspectionAttribute : Attribute
    {
        private static Dictionary<Type, HashSet<string>> GlobalAttributes { get; } = new Dictionary<Type, HashSet<string>>();

        public static void Set(Type type, string propertyName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            if (!GlobalAttributes.TryGetValue(type, out var properties))
            {
                GlobalAttributes[type] = properties = new HashSet<string>();
            }

            properties.Add(propertyName);
        }

        public static void Set<T>(string propertyName)
        {
            Set(typeof(T), propertyName);
        }

        public static void Clear(Type type, string propertyName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            if (GlobalAttributes.TryGetValue(type, out var properties))
            {
                properties.Remove(propertyName);
            }
        }

        public static void Clear<T>(string propertyName)
        {
            Clear(typeof(T), propertyName);
        }

        public static bool IsSet(Type type, string propertyName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            if (GlobalAttributes.TryGetValue(type, out var properties))
            {
                if (properties.Contains(propertyName)) return true;
            }
            return false;
        }

        public static bool IsSet<T>(string propertyName)
        {
            return IsSet(typeof(T), propertyName);
        }

        public static bool IsSet(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            
            if (property.GetCustomAttribute<NoInspectionAttribute>() != null) return true;
            if (property.DeclaringType == null) return false;

            return IsSet(property.DeclaringType, property.Name);
        }
    }
}

