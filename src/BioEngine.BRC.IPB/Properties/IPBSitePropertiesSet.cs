using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Properties;
using BioEngine.BRC.Core.Users;
using BioEngine.BRC.IPB.Api;
using BioEngine.BRC.IPB.Models;
using JetBrains.Annotations;

namespace BioEngine.BRC.IPB.Properties
{
    [PropertiesSet("Публикация на форуме", IsEditable = true)]
    public class IPBSitePropertiesSet : PropertiesSet
    {
        [PropertiesElement("Включено?", PropertyElementType.Checkbox)]
        public bool IsEnabled { get; set; }

        [PropertiesElement("Раздел на форуме", PropertyElementType.Dropdown)]
        public int ForumId { get; set; }
    }

    [UsedImplicitly]
    public class IPBSectionPropertiesOptionsResolver : IPropertiesOptionsResolver
    {
        private readonly IPBApiClientFactory _apiClientFactory;
        private readonly ICurrentUserProvider _currentUserProvider;
        private IPBApiClient _apiClient;

        public IPBSectionPropertiesOptionsResolver(IPBApiClientFactory apiClientFactory,
            ICurrentUserProvider currentUserProvider)
        {
            _apiClientFactory = apiClientFactory;
            _currentUserProvider = currentUserProvider;
        }

        public bool CanResolve(PropertiesSet properties)
        {
            return properties is IPBSitePropertiesSet;
        }

        private async Task<IPBApiClient> GetClientAsync()
        {
            return _apiClient ??= _apiClientFactory.GetClient(await _currentUserProvider.GetAccessTokenAsync());
        }

        public async Task<List<PropertiesOption>?> ResolveAsync(PropertiesSet properties, string property)
        {
            switch (property)
            {
                case "ForumId":
                    var response = await (await GetClientAsync()).GetForumsAsync(1, 1000);
                    var roots = response.Results.Where(f => f.ParentId == null).ToList();
                    var forums = new List<Forum>();
                    foreach (var forum in roots)
                    {
                        FillTree(forum, forums, response.Results.ToList());
                    }

                    return forums.Select(f => new PropertiesOption(f.FullName, f.Id, f.Category)).ToList();
                default: return null;
            }
        }

        private void FillTree(Forum forum, List<Forum> forums, List<Forum> allForums)
        {
            var children = allForums.Where(f => f.ParentId == forum.Id);
            foreach (var child in children)
            {
                child.Parent = forum;
                FillTree(child, forums, allForums);
                forum.Children.Add(child);
            }

            if (!forum.Children.Any())
            {
                forums.Add(forum);
            }
        }
    }
}
