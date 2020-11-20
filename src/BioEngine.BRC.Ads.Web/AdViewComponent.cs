using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BioEngine.BRC.Ads.Entities;
using BioEngine.BRC.Web;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.Ads.Web
{
    public class AdViewComponent : ViewComponent
    {
        private readonly AdsProvider _adsProvider;

        public AdViewComponent(AdsProvider adsProvider)
        {
            _adsProvider = adsProvider;
        }

        [SuppressMessage("ReSharper", "Mvc.ViewComponentViewNotResolved")]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ad = await _adsProvider.NextAsync(HttpContext.Features.Get<CurrentSiteFeature>().Site);
            if (ad != null)
                return View(new AdViewModel(ad));
            return Content(string.Empty);
        }
    }

    public class AdViewModel
    {
        public Ad Ad { get; }

        public AdViewModel(Ad ad)
        {
            Ad = ad;
        }
    }
}
