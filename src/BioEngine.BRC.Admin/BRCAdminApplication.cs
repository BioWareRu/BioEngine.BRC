using System;
using BioEngine.BRC.Core;
using BioEngine.BRC.Facebook;
using BioEngine.BRC.IPB;
using BioEngine.BRC.IPB.Admin;
using BioEngine.BRC.IPB.Auth;
using BioEngine.BRC.Posts.Admin;
using BioEngine.BRC.Seo;
using BioEngine.BRC.Twitter;

namespace BioEngine.BRC.Admin
{
    public class BRCAdminApplication : BRCApplication
    {
        protected override string ConsoleLogFormat =>
            "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}]{NewLine}\t{Message:lj}{NewLine}{Exception}";

        public BRCAdminApplication(string[] args) : base(args)
        {
            AddPostgresDb(true, typeof(BRCAdminApplication).Assembly)
                .AddElasticSearch()
                .AddElasticStack()
                .AddS3Storage()
                .AddModule<PostsAdminModule, PostsAdminModuleConfig>()
                .AddModule<SeoModule>()
                .AddModule<IPBAdminModule, IPBAdminModuleConfig>((configuration, env, moduleConfig) =>
                {
                    if (!Uri.TryCreate(configuration["BE_IPB_URL"], UriKind.Absolute, out var ipbUrl))
                    {
                        throw new ArgumentException($"Can't parse IPB url; {configuration["BE_IPB_URL"]}");
                    }

                    moduleConfig.Url = ipbUrl;


                    moduleConfig.ApiReadonlyKey = configuration["BE_IPB_API_READONLY_KEY"];
                    moduleConfig.ApiPublishKey = configuration["BE_IPB_API_PUBLISH_KEY"];
                })
                .AddModule<TwitterModule>()
                .AddModule<FacebookModule>()
                .AddIpbUsers<IPBSiteUsersModule, IPBSiteUsersModuleConfig, IPBSiteCurrentUserProvider>(true);
        }
    }
}
