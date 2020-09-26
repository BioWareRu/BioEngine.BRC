using System.Collections.Generic;

namespace BioEngine.BRC.Core.Properties
{
    public class PropertiesEntry
    {
        public PropertiesEntry(string key)
        {
            Key = key;
        }

        public string Key { get; }

        public List<PropertiesValue> Properties { get; } = new List<PropertiesValue>();
    }
}
