using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;

namespace BioEngine.BRC.Web.Model
{
    public class SectionContentListViewModel<TSection, TContent> : ListViewModel<TContent>
        where TContent : class, IContentItem where TSection : Section
    {
        public new TSection Section { get; }

        public SectionContentListViewModel(PageViewModelContext context, TSection section, TContent[] items,
            int totalItems, int page,
            int itemsPerPage) : base(context, items, totalItems, page, itemsPerPage)
        {
            Section = section;
        }
    }
}
