using System;
using BioEngine.BRC.Ads;
using BioEngine.BRC.Ads.Web;
using BioEngine.BRC.Core;
using BioEngine.BRC.IPB;
using BioEngine.BRC.IPB.Web;
using BioEngine.BRC.IPB.Auth;
using BioEngine.BRC.Seo;

namespace BioEngine.BRC.Site
{
    public class BRCSiteApplication : BRCApplication
    {
        public BRCSiteApplication(string[] args) : base(args)
        {
            AddPostgresDb()
                .AddElasticSearch()
                .AddElasticStack()
                .AddS3Storage()
                .AddModule<SeoModule>()
                .AddModule<IPBSiteModule, IPBSiteModuleConfig>((configuration, env, moduleConfig) =>
                {
                    if (!Uri.TryCreate(configuration["BE_IPB_URL"], UriKind.Absolute, out var ipbUrl))
                    {
                        throw new ArgumentException($"Can't parse IPB url; {configuration["BE_IPB_URL"]}");
                    }

                    moduleConfig.Url = ipbUrl;
                    moduleConfig.ApiReadonlyKey = configuration["BE_IPB_API_READONLY_KEY"];
                })
                .AddIpbUsers<IPBSiteUsersModule, IPBSiteUsersModuleConfig, IPBSiteCurrentUserProvider>()
                .AddModule<BrcSiteModule, BrcSiteModuleConfig>((configuration, env, moduleConfig) =>
                {
                    if (!string.IsNullOrEmpty(configuration["BE_SITE_ID"]))
                    {
                        moduleConfig.SiteId = Guid.Parse(configuration["BE_SITE_ID"]);
                    }

                    if (!string.IsNullOrEmpty(configuration["BE_PATREON_SERVICE_URL"]))
                    {
                        moduleConfig.PatreonServiceUrl = configuration["BE_PATREON_SERVICE_URL"];
                    }
                })
                .AddModule<AdsSiteModule, AdsModuleConfig>();
        }
    }
}
