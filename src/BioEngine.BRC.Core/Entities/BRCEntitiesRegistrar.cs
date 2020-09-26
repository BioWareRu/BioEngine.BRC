using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sitko.Core.Repository;

namespace BioEngine.BRC.Core.Entities
{
    public class BRCEntitiesRegistrar
    {
        private readonly IServiceCollection _serviceCollection;

        private static readonly ConcurrentDictionary<string, Type>
            _blockTypes = new ConcurrentDictionary<string, Type>();

        private static readonly ConcurrentDictionary<string, Type> _entities = new ConcurrentDictionary<string, Type>();

        private static bool _requireArrayConversion;
        private static BRCEntitiesRegistrar? _instance;

        public void RequireArrayConversion()
        {
            _requireArrayConversion = true;
        }

        public bool IsArrayConversionRequired()
        {
            return _requireArrayConversion;
        }

        private BRCEntitiesRegistrar(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public static BRCEntitiesRegistrar Instance(IServiceCollection serviceCollection)
        {
            if (_instance == null)
            {
                _instance = new BRCEntitiesRegistrar(serviceCollection);
                serviceCollection.AddSingleton(_instance);
            }

            return _instance;
        }

        public static BRCEntitiesRegistrar Instance()
        {
            if (_instance == null)
            {
                throw new Exception("BRCDomainRegistrar is not instantiated");
            }

            return _instance;
        }

        public ContentBlock? CreateBlock(string key)
        {
            if (_blockTypes.ContainsKey(key))
            {
                return Activator.CreateInstance(_blockTypes[key]) as ContentBlock;
            }

            return null;
        }

        public void RegisterContentBlock<TBlock, TBlockData>()
            where TBlock : ContentBlock<TBlockData> where TBlockData : ContentBlockData, new()
        {
            _modelBuilderConfigurators.Add(modelBuilder =>
            {
                var key = EntityExtensions.GetKey<TBlock>();
                // logger.LogInformation(
                //     "Register content block type {type} - {entityType} ({dataType})", key,
                //     typeof(TBlock),
                //     typeof(TBlockData));
                RegisterDiscriminator<ContentBlock, TBlock>(modelBuilder, key);
                RegisterDataConversion<TBlock, TBlockData>(modelBuilder);
                if (!_blockTypes.ContainsKey(key))
                {
                    _blockTypes.TryAdd(key, typeof(TBlock));
                }
            });
        }

        public void RegisterSection<TSection, TSectionData>()
            where TSection : Section<TSectionData> where TSectionData : ITypedData, new()
        {
            _serviceCollection.AddScoped<IValidator, SiteEntityValidator<TSection>>();
            RegisterEntity<TSection>();
            _modelBuilderConfigurators.Add(modelBuilder =>
            {
                var key = EntityExtensions.GetKey<TSection>();
                // logger.LogInformation("Register section type {type} - {entityType} ({dataType})", key,
                //     typeof(TSection),
                //     typeof(TSectionData));
                RegisterDiscriminator<Section, TSection>(modelBuilder, key);
                RegisterDataConversion<TSection, TSectionData>(modelBuilder);
                if (_requireArrayConversion)
                {
                    RegisterSiteEntityConversions<TSection>(modelBuilder);
                }
            });
        }

        public void RegisterContentItem<TContentItem>()
            where TContentItem : class, IContentItem
        {
            // logger.LogInformation("Register content item type {type} - {entityType}",
            //     EntityExtensions.GetKey<TContentItem>(),
            //     typeof(TContentItem));
            _serviceCollection.AddScoped<IValidator, SiteEntityValidator<TContentItem>>();
            _serviceCollection.AddScoped<IValidator, SectionEntityValidator<TContentItem>>();
            _serviceCollection.AddScoped<IValidator, ContentItemValidator<TContentItem>>();
            RegisterEntity<TContentItem>();
            _modelBuilderConfigurators.Add(modelBuilder =>
            {
                if (_requireArrayConversion)
                {
                    RegisterSiteEntityConversions<TContentItem>(modelBuilder);
                    RegisterSectionEntityConversions<TContentItem>(modelBuilder);
                }

                modelBuilder.Entity<TContentItem>().Property(i => i.Title).IsRequired();
                modelBuilder.Entity<TContentItem>().Property(i => i.Url).IsRequired();
                modelBuilder.Entity<TContentItem>().Ignore(i => i.Blocks);
                modelBuilder.Entity<TContentItem>().Ignore(i => i.Sections);
                modelBuilder.Entity<TContentItem>().Ignore(i => i.Tags);
                modelBuilder.Entity<TContentItem>().Ignore(i => i.PublicRouteName);
                modelBuilder.Entity<TContentItem>().HasIndex(i => i.SiteIds);
                modelBuilder.Entity<TContentItem>().HasIndex(i => i.TagIds);
                modelBuilder.Entity<TContentItem>().HasIndex(i => i.SectionIds);
                modelBuilder.Entity<TContentItem>().HasIndex(i => i.IsPublished);
                modelBuilder.Entity<TContentItem>().HasIndex(i => i.Url).IsUnique();
            });
        }

        public void RegisterEntity<TEntity>() where TEntity : class, IEntity
        {
            _modelBuilderConfigurators.Add(modelBuilder =>
            {
                modelBuilder.Entity<TEntity>();
                var key = EntityExtensions.GetKey<TEntity>();
                if (!_entities.ContainsKey(key))
                {
                    _entities.TryAdd(key, typeof(TEntity));
                }
            });
        }

        private readonly List<Action<ModelBuilder>> _modelBuilderConfigurators = new List<Action<ModelBuilder>>();

        public void RegisterSiteEntity<TSiteEntity>()
            where TSiteEntity : class, ISiteEntity
        {
            _serviceCollection.AddScoped<IValidator, SiteEntityValidator<TSiteEntity>>();
            RegisterEntity<TSiteEntity>();
            _modelBuilderConfigurators.Add(modelBuilder =>
            {
                modelBuilder.Entity<TSiteEntity>().HasIndex(e => e.SiteIds);
                if (_requireArrayConversion)
                {
                    RegisterSiteEntityConversions<TSiteEntity>(modelBuilder);
                }
            });
        }

        private static void RegisterSectionEntityConversions<TEntity>(ModelBuilder modelBuilder)
            where TEntity : class, ISectionEntity
        {
            modelBuilder
                .Entity<TEntity>()
                .Property(s => s.SectionIds)
                .HasColumnType("jsonb");
            modelBuilder
                .Entity<TEntity>()
                .Property(s => s.TagIds)
                .HasColumnType("jsonb");

            if (_requireArrayConversion)
            {
                RegisterJsonStringConversion<TEntity, Guid[]>(modelBuilder, e => e.SectionIds);
                RegisterJsonStringConversion<TEntity, Guid[]>(modelBuilder, e => e.TagIds);
            }
        }

        private static void RegisterSiteEntityConversions<TEntity>(ModelBuilder modelBuilder)
            where TEntity : class, ISiteEntity
        {
            modelBuilder
                .Entity<TEntity>()
                .Property(s => s.SiteIds)
                .HasColumnType("jsonb");
            if (_requireArrayConversion)
            {
                RegisterJsonStringConversion<TEntity, Guid[]>(modelBuilder, e => e.SiteIds);
            }
        }

        private static void RegisterJsonStringConversion<TEntity, TProperty>(ModelBuilder modelBuilder,
            Expression<Func<TEntity, TProperty>> propertySelector)
            where TEntity : class
        {
            modelBuilder
                .Entity<TEntity>()
                .Property(propertySelector)
                .HasConversion(data => JsonConvert.SerializeObject(data),
                    json => JsonConvert.DeserializeObject<TProperty>(json));
        }

        private static void RegisterDataConversion<TEntity, TData>(ModelBuilder modelBuilder)
            where TEntity : class, ITypedEntity<TData> where TData : ITypedData, new()
        {
            modelBuilder
                .Entity<TEntity>()
                .Property(e => e.Data)
                .HasColumnType("jsonb")
                .HasColumnName(nameof(ITypedEntity<TData>.Data));
            if (_requireArrayConversion)
            {
                RegisterJsonStringConversion<TEntity, TData>(modelBuilder, e => e.Data);
            }
        }

        private static void RegisterDiscriminator<TBase, TObject>(ModelBuilder modelBuilder,
            string discriminator)
            where TBase : class
        {
            var discriminatorBuilder =
                modelBuilder.Entity<TBase>().HasDiscriminator<string>(nameof(Section.Type));
            discriminatorBuilder.HasValue<TObject>(discriminator);
        }

        public void ConfigureDbContext(ModelBuilder modelBuilder)
        {
            foreach (var modelBuilderConfigurator in _modelBuilderConfigurators)
            {
                modelBuilderConfigurator(modelBuilder);
            }
        }
    }
}
