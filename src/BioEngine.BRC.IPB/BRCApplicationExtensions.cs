using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BioEngine.BRC.Core;
using BioEngine.BRC.Core.Policies;
using BioEngine.BRC.Core.Users;
using Microsoft.AspNetCore.Authorization;

namespace BioEngine.BRC.IPB
{
    public static class BRCApplicationExtensions
    {
        public static BRCApplication
            AddIpbUsers<TUsersModule, TConfig, TCurrentUserProvider>(this BRCApplication application,
                bool isAdmin = false)
            where TUsersModule : IPBUsersModule<TConfig, TCurrentUserProvider>
            where TConfig : IPBUsersModuleConfig, new()
            where TCurrentUserProvider : class, ICurrentUserProvider
        {
            application.AddModule<TUsersModule, TConfig>((configuration, env, moduleConfig) =>
            {
                bool.TryParse(configuration["BE_IPB_API_DEV_MODE"] ?? "", out var devMode);
                int.TryParse(configuration["BE_IPB_API_ADMIN_GROUP_ID"], out var adminGroupId);
                int.TryParse(configuration["BE_IPB_API_SITE_TEAM_GROUP_ID"], out var siteTeamGroupId);

                var additionalGroupIds = new List<int> {siteTeamGroupId};
                if (!string.IsNullOrEmpty(configuration["BE_IPB_API_ADDITIONAL_GROUP_IDS"]))
                {
                    var ids = configuration["BE_IPB_API_ADDITIONAL_GROUP_IDS"].Split(',');
                    foreach (var id in ids)
                    {
                        if (int.TryParse(id, out var parsedId))
                        {
                            additionalGroupIds.Add(parsedId);
                        }
                    }
                }

                moduleConfig.DevMode = devMode;
                moduleConfig.AdminGroupId = adminGroupId;
                moduleConfig.AdditionalGroupIds = additionalGroupIds.Distinct().ToArray();
                moduleConfig.CallbackPath = "/login/ipb";
                if (isAdmin)
                {
                    moduleConfig.ApiClientId = configuration["BE_IPB_ADMIN_OAUTH_CLIENT_ID"];
                    moduleConfig.ApiClientSecret = configuration["BE_IPB_ADMIN_OAUTH_CLIENT_SECRET"];
                }
                else
                {
                    moduleConfig.ApiClientId = configuration["BE_IPB_OAUTH_CLIENT_ID"];
                    moduleConfig.ApiClientSecret = configuration["BE_IPB_OAUTH_CLIENT_SECRET"];
                }

                moduleConfig.AuthorizationEndpoint = configuration["BE_IPB_AUTHORIZATION_ENDPOINT"];
                moduleConfig.TokenEndpoint = configuration["BE_IPB_TOKEN_ENDPOINT"];
                moduleConfig.DataProtectionPath = configuration["BE_IPB_DATA_PROTECTION_PATH"];
            });

            return application;
        }
    }
}
