using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Core.Comments
{
    public interface ICommentsProvider
    {
        Task<Dictionary<Guid, (int count, Uri? uri)>> GetCommentsDataAsync(IContentItem[] entities, Site site);
        Task<Dictionary<Guid, Uri?>> GetCommentsUrlAsync(IContentItem[] entities, Site site);

        Task<IEnumerable<BaseComment>> GetLastCommentsAsync<TContent>(Site site, int count)
            where TContent : class, IContentItem;

        Task<List<(TContent entity, int commentsCount)>> GetMostCommentedAsync<TContent>(Site site, int count,
            TimeSpan period) where TContent : class, IContentItem;

        Task<BaseComment> AddCommentAsync(IContentItem entity, string text, string authorId,
            Guid? replyTo = null);

        Task<BaseComment> UpdateCommentAsync(IContentItem entity, Guid commentId, string text);
        Task<BaseComment> DeleteCommentAsync(IContentItem entity, Guid commentId);
        Task<IEnumerable<BaseComment>> GetCommentsAsync(IContentItem entity, Site site);
        Task<BaseComment> GetCommentByIdAsync(IContentItem entity, Guid commentId);
    }
}
