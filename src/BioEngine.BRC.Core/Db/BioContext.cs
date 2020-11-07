using BioEngine.BRC.Core.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Sitko.Core.Storage;

namespace BioEngine.BRC.Core.Db
{
    public class BioContext : DbContext

    {
        public BioContext(DbContextOptions<BioContext> options) : base(options)
        {
        }

        public DbSet<Site> Sites => Set<Site>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<ContentBlock> Blocks => Set<ContentBlock>();
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
