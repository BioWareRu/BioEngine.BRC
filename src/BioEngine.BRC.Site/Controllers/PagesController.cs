using System;
using BioEngine.BRC.Pages.Entities;
using BioEngine.BRC.Pages.Repository;
using BioEngine.BRC.Web;
using BioEngine.BRC.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.Site.Controllers
{
    public class PagesController : SiteController<Page, Guid, PagesRepository>
    {
        public PagesController(BaseControllerContext<Page, Guid, PagesRepository> context) :
            base(context)
        {
        }

        protected override IActionResult PageNotFound()
        {
            return View("~/Views/Errors/Error.cshtml", new ErrorsViewModel(GetPageContext(), 404));
        }
    }
}
