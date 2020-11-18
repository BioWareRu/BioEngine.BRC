namespace BioEngine.BRC.Core.Entities
{
    [Entity("site", "Сайт")]
    public class Site : BaseEntity
    {
        public string Url { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
    }
}
