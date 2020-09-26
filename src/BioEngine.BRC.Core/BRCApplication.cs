using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Policies;
using BioEngine.BRC.Core.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;
using Sitko.Core.Db.Postgres;
using Sitko.Core.ElasticStack;
using Sitko.Core.Repository.EntityFrameworkCore;
using Sitko.Core.Search.ElasticSearch;
using Sitko.Core.Storage;
using Sitko.Core.Storage.S3;

namespace BioEngine.BRC.Core
{
    public abstract class BRCApplication : Application<BRCApplication>
    {
        protected BRCApplication(string[] args) : base(args)
        {
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


        public BRCApplication AddPostgresDb(bool enablePooling = true)
        {
            return AddModule<PostgresModule<BioContext>, PostgresDatabaseModuleConfig<BioContext>>(
                    (configuration, env, moduleConfig) =>
                    {
                        moduleConfig.Host = configuration["BE_POSTGRES_HOST"];
                        moduleConfig.Port = int.Parse(configuration["BE_POSTGRES_PORT"]);
                        moduleConfig.Database = configuration["BE_POSTGRES_DATABASE"];
                        moduleConfig.Username = configuration["BE_POSTGRES_USERNAME"];
                        moduleConfig.Password = configuration["BE_POSTGRES_PASSWORD"];
                        moduleConfig.MigrationsAssembly = typeof(BioContext).Assembly;
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
                if (!success)
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
        public Uri Server { get; set; }
        public string Bucket { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
