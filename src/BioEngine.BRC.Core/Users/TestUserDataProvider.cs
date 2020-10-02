using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;

namespace BioEngine.BRC.Core.Users
{
    public class TestUserDataProvider : IUserDataProvider
    {
        public Task<List<IUser>> GetDataAsync(string[] userIds)
        {
            var users = userIds
                .Select(userId =>
                    new TestUser
                    {
                        Id = userId, Name = $"User{userId.ToString()}", PhotoUrl = "", ProfileUrl = ""
                    } as IUser)
                .ToList();

            return Task.FromResult(users);
        }
    }

    [Entity("testuser")]
    public class TestUser : IUser
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;


        public object GetId()
        {
            return Id;
        }
    }
}
