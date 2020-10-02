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

        [UsedImplicitly] public DbSet<Site> Sites => Set<Site>();
        [UsedImplicitly] public DbSet<Tag> Tags => Set<Tag>();
        [UsedImplicitly] public DbSet<Menu> Menus => Set<Menu>();
        [UsedImplicitly] public DbSet<Section> Sections => Set<Section>();
        [UsedImplicitly] public DbSet<ContentBlock> Blocks => Set<ContentBlock>();
        [UsedImplicitly] public DbSet<StorageItem> StorageItems => Set<StorageItem>();
        public DbSet<PropertiesRecord> Properties => Set<PropertiesRecord>();

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
