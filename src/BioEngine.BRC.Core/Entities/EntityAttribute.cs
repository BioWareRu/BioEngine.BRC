using System;

namespace BioEngine.BRC.Core.Entities
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
        public EntityAttribute(string key, string title)
        {
            Key = key;
            Title = title;
        }

        public string Key { get; }
        public string Title { get; }
    }
}
