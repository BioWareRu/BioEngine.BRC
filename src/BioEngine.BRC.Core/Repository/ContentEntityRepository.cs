using System;
using System.Threading;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Helpers;
using BioEngine.BRC.Core.Repository.Abstractions;
using Sitko.Core.Repository;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public abstract class ContentEntityRepository<TEntity> : SiteEntityRepository<TEntity>,
        IContentEntityRepository<TEntity>
        where TEntity : class, IContentEntity, ISiteEntity, IBioEntity
    {
        protected ContentEntityRepository(EFRepositoryContext<TEntity, Guid, BioContext> repositoryContext) : base(
            repositoryContext)
        {
        }

        public virtual async Task PublishAsync(TEntity item)
        {
            item.IsPublished = true;
            item.DatePublished = DateTimeOffset.UtcNow;
            await UpdateAsync(item);
        }

        public virtual async Task UnPublishAsync(TEntity item)
        {
            item.IsPublished = false;
            item.DatePublished = null;
            await UpdateAsync(item);
        }


        public async Task<(TEntity[] items, int itemsCount)> GetAllWithBlocksAsync()
        {
            var result = await base.GetAllAsync();
            await DoGetAllWithBlocksAsync(result.items);
            return result;
        }

        public async Task<(TEntity[] items, int itemsCount)> GetAllWithBlocksAsync(
            Func<IRepositoryQuery<TEntity>, Task> configureQuery)
        {
            var result = await base.GetAllAsync(configureQuery);
            await DoGetAllWithBlocksAsync(result.items);
            return result;
        }

        public async Task<(TEntity[] items, int itemsCount)> GetAllWithBlocksAsync(
            Action<IRepositoryQuery<TEntity>> configureQuery)
        {
            var result = await base.GetAllAsync(configureQuery);
            await DoGetAllWithBlocksAsync(result.items);
            return result;
        }

        private async Task DoGetAllWithBlocksAsync(
            TEntity[] items)
        {
            var blocks = await BlocksHelper.GetBlocksAsync(items, Set<ContentBlock>());
            foreach (var item in items)
            {
                item.Blocks = blocks[item.Id];
            }
        }

        public async Task<TEntity?> GetWithBlocksAsync()
        {
            var entity = await base.GetAsync();
            return await DoGetWithBlocksAsync(entity);
        }

        public async Task<TEntity?> GetWithBlocksAsync(Func<IRepositoryQuery<TEntity>, Task> configureQuery)
        {
            var entity = await base.GetAsync(configureQuery);
            return await DoGetWithBlocksAsync(entity);
        }

        public async Task<TEntity?> GetWithBlocksAsync(Action<IRepositoryQuery<TEntity>> configureQuery)
        {
            var entity = await base.GetAsync(configureQuery);
            return await DoGetWithBlocksAsync(entity);
        }

        private async Task<TEntity?> DoGetWithBlocksAsync(TEntity? entity)
        {
            if (entity != null)
            {
                entity.Blocks = await BlocksHelper.GetBlocksAsync(entity, Set<ContentBlock>());
            }

            return entity;
        }

        public async Task<TEntity?> GetByIdWithBlocksAsync(Guid id)
        {
            var entity = await base.GetByIdAsync(id);
            return await DoGetWithBlocksAsync(entity);
        }

        public async Task<TEntity?> GetByIdWithBlocksAsync(Guid id,
            Func<IRepositoryQuery<TEntity>, Task> configureQuery)
        {
            var entity = await base.GetByIdAsync(id, configureQuery);
            return await DoGetWithBlocksAsync(entity);
        }

        public async Task<TEntity?> GetByIdWithBlocksAsync(Guid id,
            Action<IRepositoryQuery<TEntity>> configureQuery)
        {
            var entity = await base.GetByIdAsync(id, configureQuery);
            return await DoGetWithBlocksAsync(entity);
        }

        public async Task<TEntity[]> GetByIdsWithBlocksAsync(Guid[] ids)
        {
            var items = await base.GetByIdsAsync(ids);
            await DoGetAllWithBlocksAsync(items);
            return items;
        }

        public async Task<TEntity[]> GetByIdsWithBlocksAsync(Guid[] ids,
            Func<IRepositoryQuery<TEntity>, Task> configureQuery)
        {
            var items = await base.GetByIdsAsync(ids, configureQuery);
            await DoGetAllWithBlocksAsync(items);
            return items;
        }

        public async Task<TEntity[]> GetByIdsWithBlocksAsync(Guid[] ids,
            Action<IRepositoryQuery<TEntity>> configureQuery)
        {
            var items = await base.GetByIdsAsync(ids, configureQuery);
            await DoGetAllWithBlocksAsync(items);
            return items;
        }

        public async Task<AddOrUpdateOperationResult<TEntity, Guid>> AddWithBlocksAsync(TEntity item)
        {
            var result = await AddAsync(item);
            if (result.IsSuccess)
            {
                await BlocksHelper.AddBlocksAsync(item, Set<ContentBlock>());
                await DoSaveAsync();
            }

            return new AddOrUpdateOperationResult<TEntity, Guid>(item, result.Errors, new PropertyChange[0]);
        }

        public async Task<AddOrUpdateOperationResult<TEntity, Guid>> UpdateWithBlocksAsync(TEntity item)
        {
            var validationResult = await UpdateAsync(item);
            if (validationResult.IsSuccess)
            {
                await BlocksHelper.UpdateBlocksAsync(item, Set<ContentBlock>());
                await DoSaveAsync();
            }

            return new AddOrUpdateOperationResult<TEntity, Guid>(item, validationResult.Errors,
                validationResult.Changes);
        }

        public override async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                var deleted = await base.DeleteAsync(id, cancellationToken);
                if (deleted)
                {
                    await BlocksHelper.DeleteBlocksAsync(entity, Set<ContentBlock>());
                    await DoSaveAsync(cancellationToken);
                    return true;
                }
            }

            return false;
        }
    }
}
