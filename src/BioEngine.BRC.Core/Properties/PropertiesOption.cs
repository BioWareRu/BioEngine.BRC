using JetBrains.Annotations;

namespace BioEngine.BRC.Core.Properties
{
    [PublicAPI]
    public class PropertiesOption
    {
        public PropertiesOption(string title, object value, string? group)
        {
            Title = title;
            Value = value;
            Group = group;
        }

        public string Title { get; }
        public object Value { get; }
        public string? Group { get; }
    }
}
