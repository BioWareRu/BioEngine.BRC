using System.Threading.Tasks;

namespace BioEngine.BRC.Core.Users
{
    public interface ICurrentUserProvider
    {
        IUser? CurrentUser { get; }
        Task<string> GetAccessTokenAsync();
    }
}
