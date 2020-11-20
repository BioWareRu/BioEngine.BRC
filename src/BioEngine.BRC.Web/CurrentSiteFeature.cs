namespace BioEngine.BRC.Web
{
    public class CurrentSiteFeature
    {
        public CurrentSiteFeature(Core.Entities.Site site)
        {
            Site = site;
        }

        public Core.Entities.Site Site { get; }
    }
}
