using System.Reflection;

namespace Toolbox.Inspections
{
    public class TypeInspection : Inspection
    {
        public TypeInspection(string key, Type type) : base(key)
        {
            var properties = type.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            ParseProperties(null, properties);
        }
    }
}
