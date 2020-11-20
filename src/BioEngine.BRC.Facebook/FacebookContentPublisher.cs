using System;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Db;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Publishing;
using BioEngine.BRC.Core.Routing;
using BioEngine.BRC.Facebook.Entities;
using BioEngine.BRC.Facebook.Service;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace BioEngine.BRC.Facebook
{
    public class FacebookContentPublisher : BaseContentPublisher<FacebookConfig, FacebookPublishRecord>
    {
        private readonly FacebookService _facebookService;
        private readonly LinkGenerator _linkGenerator;

        public FacebookContentPublisher(FacebookService facebookService, BioContext dbContext,
            ILogger<FacebookContentPublisher> logger, LinkGenerator linkGenerator) : base(dbContext, logger)
        {
            _facebookService = facebookService;
            _linkGenerator = linkGenerator;
        }

        protected override async Task<FacebookPublishRecord> DoPublishAsync(FacebookPublishRecord record,
            IContentItem entity, Site site,
            FacebookConfig config)
        {
            var postId = await _facebookService.PostLinkAsync(_linkGenerator.GeneratePublicUrl(entity, site),
                config);
            if (string.IsNullOrEmpty(postId))
            {
                throw new Exception($"Can't create facebook post for item {entity.Title} ({entity.Id.ToString()})");
            }

            record.PostId = postId;

            return record;
        }

        protected override async Task<bool> DoDeleteAsync(FacebookPublishRecord record, FacebookConfig config)
        {
            var deleted = await _facebookService.DeletePostAsync(record.PostId, config);
            if (deleted)
            {
                return true;
            }

            throw new Exception("Can't delete content post from Facebook");
        }
    }
}
