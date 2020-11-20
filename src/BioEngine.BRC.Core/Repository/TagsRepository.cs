using System;
using System.Threading;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using FluentValidation.Results;
using Sitko.Core.Repository;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Core.Repository
{
    public class TagsRepository : EFRepository<Tag, Guid, BioContext>
    {
        public TagsRepository(EFRepositoryContext<Tag, Guid, BioContext> repositoryContext) : base(
            repositoryContext)
        {
        }

        public override async Task<AddOrUpdateOperationResult<Tag, Guid>> AddAsync(Tag entity,
            CancellationToken cancellationToken = default)
        {
            var existingTag = await GetAsync(q => q.Where(t => t.Title == entity.Title), cancellationToken);
            if (existingTag != null)
            {
                return new AddOrUpdateOperationResult<Tag, Guid>(existingTag, new ValidationFailure[0],
                    new PropertyChange[0]);
            }

            return await base.AddAsync(entity, cancellationToken);
        }
    }
}
