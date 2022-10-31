using Toolbox.Inspections;

namespace Toolbox.Serialization
{
    public abstract class InspectionFormatter
    {
        public void Serialize(Stream stream, IEnumerable<Inspection> inspections)
        {
            BeginSerialize(stream, inspections);

            foreach (var inspection in inspections)
            {
                Write(stream, inspection);
            }

            EndSerialize(stream, inspections);
        }

        protected void Write(Stream stream, Inspection inspection)
        {
            WriteHeader(stream, inspection);
            WriteContent(stream, inspection);
            WriteFooter(stream, inspection);
        }

        protected abstract void BeginSerialize(Stream stream, IEnumerable<Inspection> inspections);
        protected abstract void EndSerialize(Stream stream, IEnumerable<Inspection> inspections);
        protected abstract void WriteHeader(Stream stream, Inspection inspection);
        protected abstract void WriteContent(Stream stream, Inspection inspection);
        protected abstract void WriteFooter(Stream stream, Inspection inspection);
    }
}
