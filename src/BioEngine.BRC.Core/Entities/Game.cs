using System.ComponentModel.DataAnnotations.Schema;
using BioEngine.BRC.Core.Routing;

namespace BioEngine.BRC.Core.Entities
{
    [Entity("gamesection", "Игра")]
    public class Game : BrcSection<GameData>
    {
        [NotMapped] public override string PublicRouteName { get; set; } = BrcDomainRoutes.GamePublic;
    }

    public class GameData : BrcSectionData
    {
        public Platform[] Platforms { get; set; } = new Platform[0];
    }

    public enum Platform
    {
        PC,
        Xbox,
        Xbox360,
        XboxOne,
        PSOne,
        PSTwo,
        PSThree,
        PSFour,
        Android,
        // ReSharper disable once InconsistentNaming
        iOS,
        MacOS,
        Linux
    }
}
