using System.Reflection;
using Toolbox.Inspectors;

namespace Toolbox.Inspections
{
    /// <summary>
    /// Information about an inspection.
    /// </summary>
    public class Inspection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Inspection"/> class.
        /// </summary>
        /// <param name="key">The key of this inspection</param>
        public Inspection(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Get the key of this inspection.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Separator for keys.
        /// </summary>
        public const char KeySeparator = '/';

        /// <summary>
        /// Get the values for this inspection.
        /// </summary>
        public Dictionary<string, object?> Values { get; } = new Dictionary<string, object?>();

        public List<Inspection> Inspections { get; } = new List<Inspection>();

        protected void ParseProperties(object? obj, IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                if (NoInspectionAttribute.IsSet(property)) continue;

                var propertyValue = property.GetValue(obj);

                if (!property.PropertyType.IsValueType && property.PropertyType != typeof(string) && propertyValue!=null) // so it is a refrence type ;-)
                {
                    var inspector = new WeakInspector(property.Name, propertyValue);
                    Inspections.AddRange(inspector.Inspect());
                }
                else
                {
                    Values.Add(property.Name, propertyValue);
                }
            }
        }
    }
}