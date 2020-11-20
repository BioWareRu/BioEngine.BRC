using Sitko.Core.App;

namespace BioEngine.BRC.Posts.Admin
{
    public class PostsAdminModule : PostsModule<PostsAdminModuleConfig>
    {
        public PostsAdminModule(PostsAdminModuleConfig config, Application application) : base(config, application)
        {
        }
    }

    public class PostsAdminModuleConfig : PostsModuleConfig
    {
    }
}
