using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using Toolbox.Inspections;
using static System.Net.Mime.MediaTypeNames;

namespace Toolbox.Serialization
{
    public class JsonInspectionFormatter : InspectionFormatter
    {
        private JsonArray? Root { get; set; }
        private Stack<JsonObject> Stack { get; } = new Stack<JsonObject>();
        private Stack<JsonArray> ParentStack { get; } = new Stack<JsonArray>();
        private JsonObject Current => Stack.Peek();
        
        private const string InspectionsName = "inspections";

        protected override void BeginSerialize(Stream stream, IEnumerable<Inspection> inspections)
        {
            Root = new JsonArray();
            Stack.Clear();
            ParentStack.Clear();

            ParentStack.Push(Root);
        }

        protected override void EndSerialize(Stream stream, IEnumerable<Inspection> inspections)
        {            
            using var writer = new Utf8JsonWriter(stream);
            Root?.WriteTo(writer);

            Stack.Clear();
        }

        protected override void WriteHeader(Stream stream, Inspection inspection)
        {
            Stack.Push(new JsonObject
            {
                { "key", inspection.Key }
            });
        }

        protected override void WriteContent(Stream stream, Inspection inspection)
        {
            if (inspection.Values.Count > 0)
            {
                var valuesObject = new JsonObject();
                Current.Add("values", valuesObject);

                foreach (var kvp in inspection.Values)
                {
                    switch (kvp.Value)
                    {
                        case bool boolean:
                            valuesObject.Add(kvp.Key, boolean);
                            break;
                        case string text:
                            valuesObject.Add(kvp.Key, text);
                            break;
                        case int number:
                            valuesObject.Add(kvp.Key, number);
                            break;
                        default:
                            valuesObject.Add(kvp.Key, kvp.Value?.ToString() ?? null);
                            break;
                    }
                }
            }

            if (inspection.Inspections.Count > 0)
            {
                var array = new JsonArray();
                
                ParentStack.Push(array);

                foreach (var childInspection in inspection.Inspections)
                {
                    Write(stream, childInspection);
                }

                ParentStack.Pop();

                Current.Add(InspectionsName, array);
            }
        }

        protected override void WriteFooter(Stream stream, Inspection inspection)
        {
            ParentStack.Peek().Add(Stack.Pop());
        }
    }
}
