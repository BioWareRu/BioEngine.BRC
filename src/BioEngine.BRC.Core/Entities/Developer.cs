using System;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Routing;

namespace BioEngine.BRC.Core.Entities
{
    [Entity("developersection", "Разработчик")]
    public class Developer : BrcSection<DeveloperData>
    {
        [NotMapped] public override string PublicRouteName { get; set; } = BrcDomainRoutes.DeveloperPublic;
    }

    public class DeveloperData : BrcSectionData
    {
        public Person[] Persons { get; set; } = new Person[0];
    }

    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTimeOffset DateStart { get; set; }
        public DateTimeOffset? DateEnd { get; set; }
    }
}
