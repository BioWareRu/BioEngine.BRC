using BioEngine.BRC.Web;
using BioEngine.BRC.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BioEngine.BRC.Site.Controllers
{
    public class ErrorsController : BaseErrorsController
    {
        public ErrorsController(BaseControllerContext context) : base(context)
        {
        }

        [Route("/error/{errorCode}")]
        public override IActionResult Error(int errorCode, ILogger<BaseErrorsController> logger)
        {
            return base.Error(errorCode, logger);
        }
    }
}
