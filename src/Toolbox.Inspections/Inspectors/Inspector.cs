using Toolbox.Inspections;

namespace Toolbox.Inspectors
{
    /// <summary>
    /// Inspector to provide <see cref="Inspections.Inspection"/>s.
    /// </summary>
    public class Inspector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Inspector"/> class.
        /// </summary>
        /// <param name="key">Key of this inspector.</param>
        /// <remarks>The <see cref="Key"/> will be prefixed to all <see cref="Inspection"/>s that this inspector creates.</remarks>
        public Inspector(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public string Key { get; }

        public virtual bool IsAlive => false;

        public IEnumerable<Inspection> Inspect()
        {
            if (IsAlive) return GetInspections();

            return Enumerable.Empty<Inspection>();
        }

        protected virtual IEnumerable<Inspection> GetInspections()
        {
            return Enumerable.Empty<Inspection>();
        }
    }
}
