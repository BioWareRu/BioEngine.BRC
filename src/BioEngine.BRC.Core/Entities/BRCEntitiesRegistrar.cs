using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sitko.Core.Repository;

namespace BioEngine.BRC.Core.Entities
{
    public class BRCEntitiesRegistrar
    {
        private static readonly ConcurrentDictionary<string, EntityDescriptor>
            _blockTypes = new();

        private static readonly ConcurrentDictionary<Type, EntityDescriptor> _entities = new();

        private static bool _requireArrayConversion;
        private static BRCEntitiesRegistrar? _instance;

        private readonly List<Action<ModelBuilder>> _modelBuilderConfigurators = new();
        private readonly IServiceCollection _serviceCollection;

        private BRCEntitiesRegistrar(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public IEnumerable<EntityDescriptor> Blocks => _blockTypes.Values;

        public void RequireArrayConversion()
        {
            _requireArrayConversion = true;
        }

        public bool IsArrayConversionRequired()
        {
            return _requireArrayConversion;
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
                return Activator.CreateInstance(_blockTypes[key].Type) as ContentBlock;
            }

            return null;
        }

        public void RegisterContentBlock<TBlock, TBlockData>()
            where TBlock : ContentBlock<TBlockData>, new() where TBlockData : ContentBlockData, new()
        {
            _modelBuilderConfigurators.Add(modelBuilder =>
            {
                var descriptor = RegisterEntityDescriptor<TBlock>();
                RegisterDiscriminator<ContentBlock, TBlock>(modelBuilder, descriptor.Key);
                RegisterDataConversion<TBlock, TBlockData>(modelBuilder);
                if (!_blockTypes.ContainsKey(descriptor.Key))
                {
                    _blockTypes.TryAdd(descriptor.Key, descriptor);
                }
            });
        }

        private EntityDescriptor RegisterEntityDescriptor<TEntity>() where TEntity : class, IEntity
        {
            if (!_entities.ContainsKey(typeof(TEntity)))
            {
                var attr = typeof(TEntity).GetCustomAttribute<EntityAttribute>();
                if (attr == null)
                {
                    throw new ArgumentException($"Entity type without type attribute: {typeof(TEntity)}");
                }

                var descriptor = new EntityDescriptor(attr.Key, typeof(TEntity), attr.Title);

                _entities.TryAdd(typeof(TEntity), descriptor);
            }

            return _entities[typeof(TEntity)];
        }

        public void RegisterSection<TSection, TSectionData>()
            where TSection : Section<TSectionData> where TSectionData : ITypedData, new()
        {
            _serviceCollection.AddScoped<IValidator, SiteEntityValidator<TSection>>();
            _serviceCollection.AddScoped<IValidator<TSection>, SiteEntityValidator<TSection>>();
            RegisterEntity<TSection>();
            _modelBuilderConfigurators.Add(modelBuilder =>
            {
                var descriptor = RegisterEntityDescriptor<TSection>();
                RegisterDiscriminator<Section, TSection>(modelBuilder, descriptor.Key);
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
            _serviceCollection.AddScoped<IValidator, SiteEntityValidator<TContentItem>>();
            _serviceCollection.AddScoped<IValidator, SectionEntityValidator<TContentItem>>();
            _serviceCollection.AddScoped<IValidator, ContentItemValidator<TContentItem>>();
            _serviceCollection.AddScoped<IValidator<TContentItem>, SiteEntityValidator<TContentItem>>();
            _serviceCollection.AddScoped<IValidator<TContentItem>, SectionEntityValidator<TContentItem>>();
            _serviceCollection.AddScoped<IValidator<TContentItem>, ContentItemValidator<TContentItem>>();
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

        public void RegisterEntity<TEntity>(Action<EntityTypeBuilder<TEntity>>? configure = null)
            where TEntity : class, IEntity
        {
            _modelBuilderConfigurators.Add(modelBuilder =>
            {
                modelBuilder.Entity<TEntity>();
                configure?.Invoke(modelBuilder.Entity<TEntity>());
                RegisterEntityDescriptor<TEntity>();
            });
        }

        public BRCEntitiesRegistrar ConfigureModelBuilder(Action<ModelBuilder> configure)
        {
            _modelBuilderConfigurators.Add(configure);
            return this;
        }

        public void RegisterSiteEntity<TSiteEntity>(Action<EntityTypeBuilder<TSiteEntity>>? configure = null)
            where TSiteEntity : class, ISiteEntity
        {
            _serviceCollection.AddScoped<IValidator, SiteEntityValidator<TSiteEntity>>();
            _serviceCollection.AddScoped<IValidator<TSiteEntity>, SiteEntityValidator<TSiteEntity>>();
            RegisterEntity<TSiteEntity>();
            _modelBuilderConfigurators.Add(modelBuilder =>
            {
                modelBuilder.Entity<TSiteEntity>().HasIndex(e => e.SiteIds);
                configure?.Invoke(modelBuilder.Entity<TSiteEntity>());
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

        public EntityDescriptor GetEntityDescriptor(Type type)
        {
            if (_entities.ContainsKey(type))
            {
                return _entities[type];
            }

            throw new ArgumentException($"Unknown entity type {type}");
        }

        public EntityDescriptor GetEntityDescriptor<TEntity>() where TEntity : IEntity
        {
            return GetEntityDescriptor(typeof(TEntity));
        }
    }

    public record EntityDescriptor(string Key, Type Type, string Title);

    public static class EntityExtensions
    {
        public static EntityDescriptor GetEntityDescriptor(this IEntity entity)
        {
            return BRCEntitiesRegistrar.Instance().GetEntityDescriptor(entity.GetType());
        }
        
        public static EntityDescriptor GetEntityDescriptor<T>() where T : IEntity
        {
            return BRCEntitiesRegistrar.Instance().GetEntityDescriptor<T>();
        }
        
        public static string GetTitle(this IEntity entity)
        {
            return entity.GetEntityDescriptor().Title;
        }

        public static string GetTitle<T>() where T : IEntity
        {
            return BRCEntitiesRegistrar.Instance().GetEntityDescriptor(typeof(T)).Title;
        }
    }
}
