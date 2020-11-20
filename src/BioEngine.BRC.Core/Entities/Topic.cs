using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Routing;

namespace BioEngine.BRC.Core.Entities
{
    [Entity("topicsection", "Тема")]
    public class Topic : BrcSection<TopicData>
    {
        [NotMapped] public override string PublicRouteName { get; set; } = BrcDomainRoutes.TopicPublic;
    }

    public class TopicData : BrcSectionData
    {
    }
}
