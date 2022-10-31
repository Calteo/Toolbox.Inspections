using Toolbox.Inspections;

namespace Toolbox.Inspectors
{
    public class TypeInspector : Inspector
    {
        public TypeInspector(string key, Type target) : base(key)
        {
            Target = target;
        }

        public Type Target { get; private set; }

        public override bool IsAlive => true;

        protected override IEnumerable<Inspection> GetInspections()
        {
            var inspections = base.GetInspections();

            return inspections
                    .Append(new TypeInspection(Key, Target));            
        }
    }
}
