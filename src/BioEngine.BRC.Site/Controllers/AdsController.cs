using System;
using System.Threading.Tasks;
using BioEngine.BRC.Ads.Repository;
using BioEngine.BRC.Web;
using BioEngine.BRC.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.Site.Controllers
{
    [Route("ads")]
    public class AdsController : BaseSiteController
    {
        private readonly AdsRepository _adsRepository;

        public AdsController(BaseControllerContext context, AdsRepository adsRepository) : base(context)
        {
            _adsRepository = adsRepository;
        }

        [HttpGet("go/{adId}.html")]
        public virtual async Task<IActionResult> RedirectAsync(Guid adId)
        {
            var ad = await _adsRepository.GetByIdAsync(adId);
            return ad == null ? PageNotFound() : Redirect(ad.Url);
        }

        protected override IActionResult PageNotFound()
        {
            return View("~/Views/Errors/Error.cshtml", new ErrorsViewModel(GetPageContext(), 404));
        }
    }
}
