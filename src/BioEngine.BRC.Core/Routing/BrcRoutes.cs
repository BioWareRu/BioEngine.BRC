using Microsoft.AspNetCore.Routing;

namespace BioEngine.BRC.Core.Routing
{
    public static class BrcRoutes
    {
        public static IEndpointRouteBuilder AddBrcRoutes(this IEndpointRouteBuilder endpoints)
        {
            return endpoints
                .MapRoute(BrcDomainRoutes.DeveloperPublic, "/developers/{url}/about.html", "Developers", "Show")
                .MapRoute(BrcDomainRoutes.GamePublic, "/games/{url}/about.html", "Games", "Show")
                .MapRoute(BrcDomainRoutes.TopicPublic, "/topics/{url}/about.html", "Topics", "Show");
        }
    }
}
