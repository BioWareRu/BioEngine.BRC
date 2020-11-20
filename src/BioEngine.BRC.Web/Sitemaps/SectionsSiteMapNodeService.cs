using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using cloudscribe.Web.SiteMap;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sitko.Core.Repository;

namespace BioEngine.BRC.Web.Sitemaps
{
    public abstract class SectionsSiteMapNodeService : BaseSiteMapNodeService<Section, Guid>
    {
        protected SectionsSiteMapNodeService(IHttpContextAccessor httpContextAccessor,
            IRepository<Section, Guid> repository,
            LinkGenerator linkGenerator)
            : base(httpContextAccessor, repository, linkGenerator)
        {
        }

        protected abstract string ContentUrlRouteName { get; }

        protected override async Task<List<ISiteMapNode>> GetNodesAsync(Section entity)
        {
            var nodes = await base.GetNodesAsync(entity);
            nodes.Add(new SiteMapNode(LinkGenerator.GetPathByName(ContentUrlRouteName, new {url = entity.Url}))
            {
                LastModified = entity.DateUpdated.DateTime, ChangeFrequency = Frequency, Priority = Priority
            });
            return nodes;
        }
    }
}
