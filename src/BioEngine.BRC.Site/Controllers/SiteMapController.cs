using System.Collections.Generic;
using System.Threading.Tasks;
using cloudscribe.Web.SiteMap;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BioEngine.BRC.Site.Controllers
{
    public class SiteMapController : cloudscribe.Web.SiteMap.Controllers.SiteMapController
    {
        public SiteMapController(ILogger<SiteMapController> logger,
            IEnumerable<ISiteMapNodeService> nodeProviders = null) : base(logger, nodeProviders)
        {
        }

        [HttpGet("/sitemap.xml")]
        [ResponseCache(CacheProfileName = "SiteMapCacheProfile")]
        public Task<IActionResult> IndexAsync()
        {
            // https://github.com/aspnet/AspNetCore/issues/7644 may be fixed in next cloudscribe release
            var syncIoFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIoFeature != null)
            {
                syncIoFeature.AllowSynchronousIO = true;
            }

            return base.Index();
        }
    }
}
