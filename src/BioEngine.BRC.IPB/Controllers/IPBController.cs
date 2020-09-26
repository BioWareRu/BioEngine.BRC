using System.Threading.Tasks;
using BioEngine.BRC.Core.Users;
using BioEngine.BRC.Core.Web;
using BioEngine.BRC.IPB.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.IPB.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v1/ipb/[controller]")]
    public abstract class IPBController : BaseController
    {
        private readonly IPBApiClientFactory _factory;
        private readonly ICurrentUserProvider _currentUserProvider;

        protected IPBController(BaseControllerContext context, IPBApiClientFactory factory,
            ICurrentUserProvider currentUserProvider) : base(context)
        {
            _factory = factory;
            _currentUserProvider = currentUserProvider;
        }

        protected async Task<IPBApiClient> GetClientAsync()
        {
            return _factory.GetClient(await _currentUserProvider.GetAccessTokenAsync());
        }

        protected IPBApiClient ReadOnlyClient => _factory.GetReadOnlyClient();
    }
}
