using System;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Posts.Entities;
using BioEngine.BRC.Web.Model;

namespace BioEngine.BRC.Posts.Web
{
    public class PostViewModel : EntityViewModel<Post>
    {
        public int CommentsCount { get; }
        public Uri CommentsUri { get; }

        public PostViewModel(PageViewModelContext context, Post entity, int commentsCount,
            Uri commentsUri,
            ContentEntityViewMode mode = ContentEntityViewMode.List) :
            base(context, entity, mode)
        {
            CommentsCount = commentsCount;
            CommentsUri = commentsUri;
        }
    }
}
