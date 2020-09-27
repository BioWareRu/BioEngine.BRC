using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Repository;
using BioEngine.BRC.Web;
using BioEngine.BRC.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.Site.Controllers
{
    public class SectionsController : BaseSiteController
    {
        private readonly GamesRepository _gamesRepository;
        private readonly DevelopersRepository _developersRepository;

        public SectionsController(BaseControllerContext context, GamesRepository gamesRepository,
            DevelopersRepository developersRepository) : base(context)
        {
            _gamesRepository = gamesRepository;
            _developersRepository = developersRepository;
        }

        [HttpGet("/games.html")]
        public async Task<IActionResult> ListGamesAsync()
        {
            var games = await _gamesRepository.GetAllAsync(query => query.ForSite(Site).Where(g => g.IsPublished));
            return ListSections(games.items, "Игры");
        }

        [HttpGet("/developers.html")]
        public async Task<IActionResult> ListDevelopersAsync()
        {
            var developers =
                await _developersRepository.GetAllAsync(query => query.ForSite(Site).Where(d => d.IsPublished));
            return ListSections(developers.items, "Компании");
        }

        private IActionResult ListSections(IEnumerable<Section> sections, string title)
        {
            var sectionsList = new Dictionary<char, List<Section>>();
            foreach (var section in sections.OrderBy(s => s.Title))
            {
                var firstLetter = section.Title.ToCharArray().First();
                if (!sectionsList.ContainsKey(firstLetter))
                {
                    sectionsList.Add(firstLetter, new List<Section>());
                }

                sectionsList[firstLetter].Add(section);
            }

            return View("Index",
                new PageViewModel<Dictionary<char, List<Section>>>(GetPageContext(), sectionsList) {Title = title});
        }
    }
}
