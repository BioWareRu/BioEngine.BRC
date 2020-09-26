using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Helpers
{
    public static class BlocksHelper
    {
        public static async Task<Dictionary<Guid, List<ContentBlock>>> GetBlocksAsync<TEntity>(TEntity[] entities,
            DbSet<ContentBlock> dbSet)
            where TEntity : IContentEntity
        {
            if (entities.Length == 0)
            {
                return new Dictionary<Guid, List<ContentBlock>>();
            }

            var ids = entities.Select(e => e.Id).ToArray();
            var blocks = await dbSet
                .Where(b => ids.Contains(b.ContentId)).ToListAsync();
            return ids.ToDictionary(id => id,
                id => blocks.Where(b => b.ContentId == id).OrderBy(b => b.Position).ToList());
        }

        public static async Task<List<ContentBlock>> GetBlocksAsync<TEntity>(TEntity entity,
            DbSet<ContentBlock> dbSet)
            where TEntity : IContentEntity
        {
            return (await GetBlocksAsync(new[] {entity}, dbSet))[entity.Id];
        }

        public static Task AddBlocksAsync<TEntity>(TEntity entity, DbSet<ContentBlock> dbSet)
            where TEntity : IContentEntity
        {
            foreach (var block in entity.Blocks)
            {
                block.ContentId = entity.Id;
            }

            return dbSet.AddRangeAsync(entity.Blocks);
        }

        public static async Task UpdateBlocksAsync<TEntity>(TEntity entity,
            DbSet<ContentBlock> dbSet)
            where TEntity : IContentEntity
        {
            var oldBlocks = await dbSet
                .Where(b => b.ContentId == entity.Id).AsNoTracking()
                .ToListAsync();
            foreach (var block in entity.Blocks)
            {
                block.ContentId = entity.Id;

                var oldBlock = oldBlocks.FirstOrDefault(b => b.Id == block.Id);
                if (oldBlock == null)
                {
                    dbSet.Add(block);
                }
            }

            foreach (var oldBlock in oldBlocks)
            {
                var block = entity.Blocks.FirstOrDefault(b => b.Id == oldBlock.Id);
                if (block == null)
                {
                    dbSet.Remove(oldBlock);
                }
                else
                {
                    dbSet.Attach(block);
                    dbSet.Update(block);
                }
            }
        }

        public static async Task DeleteBlocksAsync<TEntity>(TEntity entity, DbSet<ContentBlock> dbSet)
            where TEntity : IContentEntity
        {
            var blocks = await GetBlocksAsync(entity, dbSet);

            dbSet.RemoveRange(blocks);
        }
    }
}
