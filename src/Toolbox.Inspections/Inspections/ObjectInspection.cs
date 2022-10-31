using System.ComponentModel;
using System.Reflection;

namespace Toolbox.Inspections
{
    /// <summary>
    /// Represents the inspection of an <see cref="object"/>.
    /// </summary>
    /// <remarks>
    /// The properties of the object get inspected.
    /// </remarks>
    public class ObjectInspection : Inspection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectInspection"/> class.
        /// </summary>
        /// <param name="key">Key of the inspection.</param>
        /// <param name="obj">Object to inspect.</param><
        public ObjectInspection(string key, object obj) : base(key)
        {
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            ParseProperties(obj, properties);
        }
    }
}
