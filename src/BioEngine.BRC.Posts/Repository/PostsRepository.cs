using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Repository;
using BioEngine.BRC.Core.Users;
using BioEngine.BRC.Posts.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Sitko.Core.Repository;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace BioEngine.BRC.Posts.Repository
{
    [UsedImplicitly]
    public class PostsRepository : ContentItemRepository<Post>
    {
        private readonly TagsRepository _tagsRepository;
        private readonly IUserDataProvider _userDataProvider;
        private readonly ICurrentUserProvider _currentUserProvider;


        public PostsRepository(EFRepositoryContext<Post, Guid, BioContext> repositoryContext,
            SectionsRepository sectionsRepository, TagsRepository tagsRepository,
            IUserDataProvider userDataProvider, ICurrentUserProvider currentUserProvider) : base(
            repositoryContext, sectionsRepository)
        {
            _tagsRepository = tagsRepository;
            _userDataProvider = userDataProvider;
            _currentUserProvider = currentUserProvider;
        }

        protected override async Task AfterLoadAsync(Post[] entities,
            CancellationToken cancellationToken = default)
        {
            await base.AfterLoadAsync(entities, cancellationToken);

            var sectionsIds = entities.SelectMany(p => p.SectionIds).Distinct().ToArray();
            var sections = await SectionsRepository.GetByIdsAsync(sectionsIds, cancellationToken);

            var tagIds = entities.SelectMany(p => p.TagIds).Distinct().ToArray();
            var tags = await _tagsRepository.GetByIdsAsync(tagIds, cancellationToken);

            var userIds = entities.Select(e => e.AuthorId).Distinct().ToArray();
            var users = await _userDataProvider.GetDataAsync(userIds);

            foreach (var entity in entities)
            {
                entity.Sections = sections.Where(s => entity.SectionIds.Contains(s.Id)).ToList();
                entity.Tags = tags.Where(t => entity.TagIds.Contains(t.Id)).ToList();
                entity.Author = users.FirstOrDefault(d => d.Id.Equals(entity.AuthorId));
            }
        }

        protected override async Task<bool> AfterSaveAsync(IEnumerable<RepositoryRecord<Post, Guid>> items,
            CancellationToken cancellationToken = default)
        {
            var user = _currentUserProvider.CurrentUser;
            foreach (var record in items)
            {
                var version = new PostVersion {Id = Guid.NewGuid(), ContentId = record.Item.Id};
                version.SetContent(record.Item);

                if (user != null)
                {
                    version.ChangeAuthorId = user.Id;
                }

                await Set<PostVersion>().AddAsync(version, cancellationToken);
                await DoSaveAsync(cancellationToken);
            }


            return await base.AfterSaveAsync(items, cancellationToken);
        }

        public async Task<List<PostVersion>> GetVersionsAsync(Guid itemId)
        {
            return await Set<PostVersion>().Where(v => v.ContentId == itemId).ToListAsync();
        }

        public async Task<PostVersion> GetVersionAsync(Guid itemId, Guid versionId)
        {
            return await Set<PostVersion>().Where(v => v.ContentId == itemId && v.Id == versionId)
                .FirstOrDefaultAsync();
        }
    }
}
