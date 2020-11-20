using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Repository;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BioEngine.BRC.Web
{
    [UsedImplicitly]
    public class CurrentSiteMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WebModuleConfig _options;
        private readonly ILogger<CurrentSiteMiddleware> _logger;
        private Core.Entities.Site? _site;

        public CurrentSiteMiddleware(RequestDelegate next, WebModuleConfig options,
            ILogger<CurrentSiteMiddleware> logger)
        {
            _next = next;
            _options = options;
            _logger = logger;
        }

        [UsedImplicitly]
        [SuppressMessage("ReSharper", "VSTHRD200")]
        public async Task Invoke(HttpContext context)
        {
            if (_site == null)
            {
                try
                {
                    var repository = context.RequestServices.GetRequiredService<SitesRepository>();
                    _site = await repository.GetByIdAsync(_options.SiteId!.Value);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in site middleware: {errorText}", ex.ToString());
                }

                if (_site == null)
                {
                    throw new Exception("Site is not configured");
                }
            }


            context.Features.Set(new CurrentSiteFeature(_site));
            await _next.Invoke(context);
        }
    }

}
