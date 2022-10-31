using Toolbox.Inspections;

namespace Toolbox.Inspectors
{
    /// <summary>
    /// Inspects an <see cref="object"/> as long it is alive.
    /// </summary>
    /// <remarks>
    /// The object is referenced by a <see cref="WeakReference"/>, so the inspector is alive
    /// als long as the object is.
    /// </remarks>
    /// <seealso cref="WeakReference.IsAlive"/>
    public class WeakInspector : Inspector 
    {
        public WeakInspector(string key, object obj) : base(key)
        {
            Object = new WeakReference(obj ?? throw new ArgumentNullException(nameof(obj)));
        }

        protected WeakReference Object { get; }

        public override bool IsAlive => Object.IsAlive;

        protected override IEnumerable<Inspection> GetInspections()
        {
            var inspections = base.GetInspections();

            var target = Object.Target;

            if (target != null)
            {
                return inspections
                    .Append(new ObjectInspection(Key, target));
            }

            return inspections;
        }
    }
}
