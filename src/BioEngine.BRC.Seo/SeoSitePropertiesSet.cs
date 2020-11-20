using BioEngine.BRC.Core.Properties;

namespace BioEngine.BRC.Seo
{
    [PropertiesSet("Seo", IsEditable = true)]
    public class SeoSitePropertiesSet : PropertiesSet
    {
        [PropertiesElement("Описание", PropertyElementType.LongString)]
        public string Description { get; set; } = "";

        [PropertiesElement("Ключевые слова")] public string Keywords { get; set; } = "";

        [PropertiesElement("Google TagManager Id")]
        public string TagManagerId { get; set; } = "";
    }
}
