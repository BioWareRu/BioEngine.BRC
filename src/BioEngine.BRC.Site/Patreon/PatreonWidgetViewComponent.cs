using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BioEngine.BRC.Site.Patreon
{
    public class PatreonWidgetViewComponent : ViewComponent
    {
        private readonly PatreonApiHelper _patreonApiHelper;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<PatreonWidgetViewComponent> _logger;

        public PatreonWidgetViewComponent(PatreonApiHelper patreonApiHelper,
            IHostEnvironment hostEnvironment,
            ILogger<PatreonWidgetViewComponent> logger)
        {
            _patreonApiHelper = patreonApiHelper;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentGoal = new PatreonGoal();
            try
            {
                if (!_hostEnvironment.IsDevelopment())
                {
                    currentGoal = await _patreonApiHelper.GetCurrentGoalAsync();
                }
                else
                {
                    currentGoal.Description = "Test goal";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while loading patreon goals: {ex.Message}");
            }

            return View(currentGoal);
        }
    }
}
