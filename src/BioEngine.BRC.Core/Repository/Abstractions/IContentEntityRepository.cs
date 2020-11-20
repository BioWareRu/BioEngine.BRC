using System;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities.Abstractions;
using Sitko.Core.Repository;

namespace BioEngine.BRC.Core.Repository.Abstractions
{
    public interface IContentEntityRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : class, IContentEntity, IBioEntity
    {
        Task PublishAsync(TEntity item);

        Task UnPublishAsync(TEntity item);


        Task<(TEntity[] items, int itemsCount)> GetAllWithBlocksAsync();

        Task<(TEntity[] items, int itemsCount)> GetAllWithBlocksAsync(
            Func<IRepositoryQuery<TEntity>, Task> configureQuery);

        Task<(TEntity[] items, int itemsCount)> GetAllWithBlocksAsync(
            Action<IRepositoryQuery<TEntity>> configureQuery);

        Task<TEntity?> GetWithBlocksAsync(Func<IRepositoryQuery<TEntity>, Task> configureQuery);
        Task<TEntity?> GetWithBlocksAsync(Action<IRepositoryQuery<TEntity>> configureQuery);
        Task<TEntity?> GetWithBlocksAsync();
        Task<TEntity?> GetByIdWithBlocksAsync(Guid id);
        Task<TEntity?> GetByIdWithBlocksAsync(Guid id, Func<IRepositoryQuery<TEntity>, Task> configureQuery);
        Task<TEntity?> GetByIdWithBlocksAsync(Guid id, Action<IRepositoryQuery<TEntity>> configureQuery);
        Task<TEntity[]> GetByIdsWithBlocksAsync(Guid[] ids);
        Task<TEntity[]> GetByIdsWithBlocksAsync(Guid[] ids, Func<IRepositoryQuery<TEntity>, Task> configureQuery);
        Task<TEntity[]> GetByIdsWithBlocksAsync(Guid[] ids, Action<IRepositoryQuery<TEntity>> configureQuery);

        Task<AddOrUpdateOperationResult<TEntity, Guid>> AddWithBlocksAsync(TEntity item);

        Task<AddOrUpdateOperationResult<TEntity, Guid>> UpdateWithBlocksAsync(TEntity item);
    }
}
