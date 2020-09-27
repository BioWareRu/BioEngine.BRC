using BioEngine.BRC.Core.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Db
{
    public class BioContext : DbContext

    {
        public BioContext(DbContextOptions<BioContext> options) : base(options)
        {
        }

        [UsedImplicitly] public DbSet<Site> Sites { get; set; }
        [UsedImplicitly] public DbSet<Tag> Tags { get; set; }
        [UsedImplicitly] public DbSet<Menu> Menus { get; set; }
        [UsedImplicitly] public DbSet<Section> Sections { get; set; }
        [UsedImplicitly] public DbSet<ContentBlock> Blocks { get; set; }
        [UsedImplicitly] public DbSet<StorageItem> StorageItems { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<PropertiesRecord> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            BRCEntitiesRegistrar.Instance().ConfigureDbContext(modelBuilder);

            modelBuilder.Entity<Section>().HasIndex(s => s.SiteIds);
            modelBuilder.Entity<Section>().HasIndex(s => s.IsPublished);
            modelBuilder.Entity<Section>().HasIndex(s => s.Type);
            modelBuilder.Entity<Section>().HasIndex(s => s.Url);
        }
    }
}
