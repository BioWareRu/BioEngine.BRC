using System;
using System.Reflection;
using System.Security.Claims;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Blocks;
using BioEngine.BRC.Core.Policies;
using BioEngine.BRC.Core.Properties;
using BioEngine.BRC.Core.Search;
using BioEngine.BRC.Core.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Sitko.Core.App.Logging;
using Sitko.Core.App.Web;
using Sitko.Core.Db.Postgres;
using Sitko.Core.ElasticStack;
using Sitko.Core.Repository.EntityFrameworkCore;
using Sitko.Core.Search;
using Sitko.Core.Search.ElasticSearch;
using Sitko.Core.Storage;
using Sitko.Core.Storage.S3;

namespace BioEngine.BRC.Core
{
    public abstract class BRCApplication : WebApplication<BRCApplication>
    {
        public static readonly string AdminRoleName = "admin";

        public static readonly AuthorizationPolicy AdminPolicy = new AuthorizationPolicyBuilder()
            .RequireClaim(ClaimTypes.Role, AdminRoleName)
            .Build();

        public static readonly string SiteTeamRoleName = "siteTeam";

        public static readonly AuthorizationPolicy SiteTeamPolicy = new AuthorizationPolicyBuilder()
            .RequireClaim(ClaimTypes.Role, SiteTeamRoleName)
            .Build();

        protected BRCApplication(string[] args) : base(args)
        {
            ConfigureServices(services =>
            {
                services.AddScoped<PropertiesProvider>();
                services.RegisterSearchProvider<DevelopersSearchProvider, Developer, Guid>();
                services.RegisterSearchProvider<GamesSearchProvider, Game, Guid>();
                services.RegisterSearchProvider<TopicsSearchProvider, Topic, Guid>();

                var registrar = BRCEntitiesRegistrar.Instance(services);
                registrar.RegisterSection<Developer, DeveloperData>();
                registrar.RegisterSection<Game, GameData>();
                registrar.RegisterSection<Topic, TopicData>();

                registrar.RegisterContentBlock<CutBlock, CutBlockData>();
                registrar.RegisterContentBlock<FileBlock, FileBlockData>();
                registrar.RegisterContentBlock<GalleryBlock, GalleryBlockData>();
                registrar.RegisterContentBlock<IframeBlock, IframeBlockData>();
                registrar.RegisterContentBlock<PictureBlock, PictureBlockData>();
                registrar.RegisterContentBlock<QuoteBlock, QuoteBlockData>();
                registrar.RegisterContentBlock<TextBlock, TextBlockData>();
                registrar.RegisterContentBlock<TwitterBlock, TwitterBlockData>();
                registrar.RegisterContentBlock<YoutubeBlock, YoutubeBlockData>();
                registrar.RegisterContentBlock<TwitchBlock, TwitchBlockData>();

                services.AddAuthorization(options =>
                {
                    options.AddPolicy(BioPolicies.Admin, AdminPolicy);
                    options.AddPolicy(BrcPolicies.SiteTeam, SiteTeamPolicy);
                    // sections
                    options.AddPolicy(BioPolicies.Sections, SiteTeamPolicy);
                    options.AddPolicy(BioPolicies.SectionsAdd, AdminPolicy);
                    options.AddPolicy(BioPolicies.SectionsEdit, AdminPolicy);
                    options.AddPolicy(BioPolicies.SectionsPublish, AdminPolicy);
                    options.AddPolicy(BioPolicies.SectionsDelete, AdminPolicy);
                });
            });
        }

        protected override void ConfigureLogging(LoggerConfiguration loggerConfiguration, LogLevelSwitcher logLevelSwitcher)
        {
            base.ConfigureLogging(loggerConfiguration, logLevelSwitcher);
            ConfigureLogLevel("Microsoft.AspNetCore.Components", LogEventLevel.Warning);
            ConfigureLogLevel("Microsoft.AspNetCore.SignalR", LogEventLevel.Warning);
            ConfigureLogLevel("Sitko.Core.Search.ElasticSearch.ElasticSearcher", LogEventLevel.Warning);
        }

        protected override bool LoggingEnableConsole
        {
            get
            {
                return Environment.IsDevelopment() ||
                       !string.IsNullOrEmpty(Configuration["BE_LOGS_CONSOLE"]) &&
                       bool.TryParse(Configuration["BE_LOGS_CONSOLE"],
                           out var forceConsoleLogging) && forceConsoleLogging;
            }
        }

        public BRCApplication AddElasticStack()
        {
            if (!string.IsNullOrEmpty(Configuration["BE_ELASTIC_ES_URI"]) &&
                !string.IsNullOrEmpty(Configuration["BE_ELASTIC_APM_URI"]))
            {
                AddModule<ElasticStackModule, ElasticStackModuleConfig>((configuration, environment, moduleConfig) =>
                    moduleConfig.EnableLogging(new Uri(configuration["BE_ELASTIC_ES_URI"]))
                        .EnableApm(new Uri(configuration["BE_ELASTIC_APM_URI"])));
            }

            return this;
        }


        public BRCApplication AddPostgresDb(bool enablePooling = true, Assembly? migrationsAssembly = null)
        {
            return AddModule<PostgresModule<BioContext>, PostgresDatabaseModuleConfig<BioContext>>(
                    (configuration, env, moduleConfig) =>
                    {
                        moduleConfig.Host = configuration["BE_POSTGRES_HOST"];
                        moduleConfig.Port = int.Parse(configuration["BE_POSTGRES_PORT"]);
                        moduleConfig.Database = configuration["BE_POSTGRES_DATABASE"];
                        moduleConfig.Username = configuration["BE_POSTGRES_USERNAME"];
                        moduleConfig.Password = configuration["BE_POSTGRES_PASSWORD"];
                        moduleConfig.MigrationsAssembly = migrationsAssembly;
                        moduleConfig.EnableNpgsqlPooling = env.IsDevelopment();
                    }
                )
                .AddModule<EFRepositoriesModule<BRCApplication>, EFRepositoriesModuleConfig>();
        }

        public BRCApplication AddElasticSearch()
        {
            return AddModule<ElasticSearchModule, ElasticSearchModuleConfig>((configuration, env, moduleConfig) =>
            {
                moduleConfig.Prefix = configuration["BE_ELASTICSEARCH_PREFIX"];
                moduleConfig.Url = configuration["BE_ELASTICSEARCH_URI"];
                moduleConfig.EnableClientLogging = env.IsDevelopment();
            });
        }

        public BRCApplication AddS3Storage()
        {
            return AddModule<S3StorageModule<BRCStorageConfig>, BRCStorageConfig>((configuration, env, moduleConfig) =>
            {
                var uri = configuration["BE_STORAGE_PUBLIC_URI"];
                var success = Uri.TryCreate(uri, UriKind.Absolute, out var publicUri);
                if (!success)
                {
                    throw new ArgumentException($"URI {uri} is not proper URI");
                }

                var serverUriStr = configuration["BE_STORAGE_S3_SERVER_URI"];
                success = Uri.TryCreate(serverUriStr, UriKind.Absolute, out var serverUri);
                if (!success || serverUri is null)
                {
                    throw new ArgumentException($"S3 server URI {serverUriStr} is not proper URI");
                }

                moduleConfig.PublicUri = publicUri;
                moduleConfig.Server = serverUri;
                moduleConfig.Bucket = configuration["BE_STORAGE_S3_BUCKET"];
                moduleConfig.SecretKey = configuration["BE_STORAGE_S3_SECRET_KEY"];
                moduleConfig.AccessKey = configuration["BE_STORAGE_S3_ACCESS_KEY"];
            });
        }
    }

    public class BRCStorageConfig : StorageOptions, IS3StorageOptions
    {
        public Uri Server { get; set; } = new Uri("http://localhost");
        public string Bucket { get; set; } = "brc";
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = String.Empty;
    }
}
