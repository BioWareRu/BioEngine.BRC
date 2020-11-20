using System.Collections.Generic;
using System.Linq;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Users;
using BioEngine.BRC.IPB.Entities;

namespace BioEngine.BRC.IPB.Auth
{
    [Entity("ipbuser", "IPB Юзер")]
    public class User : IUser
    {
        public string Id { get; set; }
        public string Name { get; set; } = "";
        public string PhotoUrl { get; set; } = "";
        public string ProfileUrl { get; set; } = "";
        public Group PrimaryGroup { get; set; } = new Group();
        public Group[]? SecondaryGroups { get; set; } = new Group[0];

        public int[] GetGroupIds()
        {
            var groupIds = new List<int> {PrimaryGroup.Id};
            groupIds.AddRange(SecondaryGroups.Select(x => x.Id));
            return groupIds.ToArray();
        }

        public object GetId()
        {
            return Id;
        }
    }
}
