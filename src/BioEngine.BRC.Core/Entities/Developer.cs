using System;
using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Routing;

namespace BioEngine.BRC.Core.Entities
{
    [Entity("developersection")]
    public class Developer : BrcSection<DeveloperData>
    {
        public override string? TypeTitle { get; set; } = "Разработчик";
        [NotMapped] public override string PublicRouteName { get; set; } = BrcDomainRoutes.DeveloperPublic;
    }

    public class DeveloperData : BrcSectionData
    {
        public Person[] Persons { get; set; } = new Person[0];
    }

    public class Person
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTimeOffset DateStart { get; set; }
        public DateTimeOffset? DateEnd { get; set; }
    }
}
