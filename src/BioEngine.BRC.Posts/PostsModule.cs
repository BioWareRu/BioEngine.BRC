using System;
using BioEngine.BRC.Core;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Routing;
using BioEngine.BRC.Posts.Entities;
using BioEngine.BRC.Posts.Policies;
using BioEngine.BRC.Posts.Search;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;
using Sitko.Core.App.Web;
using Sitko.Core.Db.Postgres;
using Sitko.Core.Repository.EntityFrameworkCore;
using Sitko.Core.Search;

namespace BioEngine.BRC.Posts
{
    public class PostsModule<T> : BaseApplicationModule<T>, IWebApplicationModule where T : PostsModuleConfig, new()
    {
        public PostsModule(T config, Application application) : base(config, application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            services.RegisterSearchProvider<PostsSearchProvider, Post, Guid>();
            services.Scan(s =>
                s.FromAssemblyOf<Post>().AddClasses(classes => classes.AssignableTo(typeof(EFRepository<,,>)))
                    .AsSelfWithInterfaces().WithScopedLifetime());
            var registrar = BRCEntitiesRegistrar.Instance(services);
            registrar.RegisterContentItem<Post>();
            registrar.RegisterEntity<PostVersion>();
            registrar.RegisterEntity<PostTemplate>();
            registrar.ConfigureModelBuilder(builder =>
            {
                builder.RegisterJsonConversion<PostTemplate, PostTemplateData>(t => t.Data, nameof(PostTemplate.Data));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PostsPolicies.Posts, BRCApplication.SiteTeamPolicy);
                options.AddPolicy(PostsPolicies.PostsAdd, BRCApplication.SiteTeamPolicy);
                options.AddPolicy(PostsPolicies.PostsEdit, BRCApplication.SiteTeamPolicy);
                options.AddPolicy(PostsPolicies.PostsDelete, BRCApplication.SiteTeamPolicy);
                options.AddPolicy(PostsPolicies.PostsPublish, BRCApplication.SiteTeamPolicy);
            });
        }

        public void ConfigureEndpoints(IConfiguration configuration, IHostEnvironment environment,
            IApplicationBuilder appBuilder,
            IEndpointRouteBuilder endpoints)
        {
            endpoints
                .MapRoute("root", "/", "Posts", "List")
                .MapRoute(BioEnginePostsRoutes.Post, "/posts/{url}.html", "Posts", "Show")
                .MapRoute(BioEnginePostsRoutes.PostsPage, "page/{page:int}.html", "Posts", "ListPage")
                .MapRoute(BioEnginePostsRoutes.PostsByTags, "posts/tags/{tagNames}.html", "Posts", "ListByTag")
                .MapRoute(BioEnginePostsRoutes.PostsByTagsPage, "posts/tags/{tagNames}/page/{page:int}.html", "Posts",
                    "ListByTagPage")
                .MapRoute(BrcDomainRoutes.DeveloperPosts, "/developers/{url}/posts.html", "Developers", "Posts")
                .MapRoute(BrcDomainRoutes.GamePostsPage, "/developers/{url}/posts/page/{page:int}.html", "Developers",
                    "PostsPage")
                .MapRoute(BrcDomainRoutes.GamePosts, "/games/{url}/posts.html", "Games", "Posts")
                .MapRoute(BrcDomainRoutes.GamePostsPage, "/games/{url}/posts/page/{page:int}.html", "Games",
                    "PostsPage")
                .MapRoute(BrcDomainRoutes.TopicPosts, "/topics/{url}/posts.html", "Topics", "Posts")
                .MapRoute(BrcDomainRoutes.TopicPostsPage, "/topics/{url}/posts/page/{page:int}.html", "Topics",
                    "PostsPage");
        }
    }

    public abstract class PostsModuleConfig
    {
    }
}
