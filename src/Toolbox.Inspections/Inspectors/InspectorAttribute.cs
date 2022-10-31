namespace Toolbox.Inspectors
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class InspectorAttribute : Attribute
    {
        public InspectorAttribute(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (!typeof(Inspector).IsAssignableFrom(type)) 
                throw new ArgumentException($"{type.FullName} does not inherit from {typeof(Inspector)}.", nameof(type));
        }
    }
}
