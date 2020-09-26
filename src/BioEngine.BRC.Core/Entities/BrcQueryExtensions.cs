using System;
using System.Linq;
using System.Linq.Expressions;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Extensions;
using Sitko.Core.Repository;

namespace BioEngine.BRC.Core.Entities
{
    public static class BrcQueryExtensions
    {
        public static IRepositoryQuery<T> ForSite<T>(this IRepositoryQuery<T> query, Site site)
            where T : class, IEntity, ISiteEntity
        {
            return query.Where(e => e.SiteIds.Contains(site.Id));
        }

        public static IRepositoryQuery<T> ForSection<T>(this IRepositoryQuery<T> query, Section section)
            where T : class, IEntity, ISectionEntity
        {
            return query.Where(e => e.SectionIds.Contains(section.Id));
        }

        public static IRepositoryQuery<T> WithTags<T>(this IRepositoryQuery<T> query, Tag[] tags)
            where T : class, IEntity, IContentItem
        {
            Expression<Func<T, bool>>? ex = null;
            foreach (var tag in tags)
            {
                ex = ex == null ? post => post.TagIds.Contains(tag.Id) : ex.And(post => post.TagIds.Contains(tag.Id));
            }

            if (ex != null)
            {
                query = query.Where(ex);
            }

            return query;
        }
    }
}
