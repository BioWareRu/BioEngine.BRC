using System;
using System.Threading.Tasks;
using BioEngine.BRC.Core.Entities;
using BioEngine.BRC.Core.Entities.Abstractions;
using BioEngine.BRC.Core.Repository;
using BioEngine.BRC.Web.Model;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Sitko.Core.Repository;

namespace BioEngine.BRC.Web
{
    public abstract class BaseSiteController : BaseController
    {
        protected BaseSiteController(BaseControllerContext context) : base(context)
        {
        }

        protected Site Site
        {
            get
            {
                var siteFeature = HttpContext.Features.Get<CurrentSiteFeature>();
                if (siteFeature == null)
                {
                    throw new ArgumentException("CurrentSiteFeature is empty");
                }

                return siteFeature.Site;
            }
        }

        protected virtual PageViewModelContext GetPageContext()
        {
            var context = new PageViewModelContext(LinkGenerator, PropertiesProvider, Site, Storage);

            return context;
        }

        protected virtual IActionResult PageNotFound()
        {
            return NotFound();
        }
    }

    public abstract class SiteController<TEntity, TEntityPk, TRepository> : BaseSiteController
        where TEntity : class, IRoutable, IContentEntity, IEntity<TEntityPk>
        where TRepository : ContentEntityRepository<TEntity>, IRepository<TEntity, TEntityPk>
    {
        protected SiteController(
            BaseControllerContext<TEntity, TEntityPk, TRepository> context)
            : base(context)
        {
            Repository = context.Repository;
        }

        protected int Page { get; private set; } = 1;
        protected virtual int ItemsPerPage { get; } = 20;

        [PublicAPI] protected TRepository Repository;


        public virtual async Task<IActionResult> ListAsync()
        {
            var (items, itemsCount) = await GetAllAsync(async query => await ApplyListConditionsAsync(query));
            return View("List", new ListViewModel<TEntity>(GetPageContext(), items,
                itemsCount, Page, ItemsPerPage));
        }

        public virtual async Task<IActionResult> ListPageAsync(int page)
        {
            var (items, itemsCount) = await GetAllAsync(ApplyListConditionsAsync, page);
            return View("List", new ListViewModel<TEntity>(GetPageContext(), items,
                itemsCount, Page, ItemsPerPage));
        }


        public virtual async Task<IActionResult> ShowAsync(string url)
        {
            var entity =
                await Repository.GetAsync(async entities =>
                    (await ApplyShowConditionsAsync(entities)).Where(e => e.Url == url));
            if (entity == null)
            {
                return PageNotFound();
            }

            return View(new EntityViewModel<TEntity>(GetPageContext(), entity, ContentEntityViewMode.Entity));
        }

        protected virtual Task<IRepositoryQuery<TEntity>> ApplyPublishConditionsAsync(IRepositoryQuery<TEntity> query)
        {
            return Task.FromResult(query.Where(e => e.IsPublished));
        }

        protected virtual void ApplyDefaultOrder(IRepositoryQuery<TEntity> provider)
        {
            provider.OrderByDescending(e => e.DateAdded);
        }

        [PublicAPI]
        protected virtual Task<(TEntity[] items, int itemsCount)> GetAllAsync(
            Func<IRepositoryQuery<TEntity>, Task<IRepositoryQuery<TEntity>>> configureQuery, int page = 0)
        {
            return Repository.GetAllWithBlocksAsync(async bioQuery =>
                await ConfigureQueryAsync(bioQuery, page, configureQuery));
        }

        protected async Task<IRepositoryQuery<TEntity>> ConfigureQueryAsync(IRepositoryQuery<TEntity> query, int page,
            Func<IRepositoryQuery<TEntity>, Task<IRepositoryQuery<TEntity>>>? configureQuery = null)
        {
            if (ControllerContext.HttpContext.Request.Query.ContainsKey("order"))
            {
                query.OrderByString(ControllerContext.HttpContext.Request.Query["order"]);
            }
            else
            {
                ApplyDefaultOrder(query);
            }

            if (page > 0)
            {
                Page = page;
            }
            else if (ControllerContext.HttpContext.Request.Query.ContainsKey("page"))
            {
                Page = int.Parse(ControllerContext.HttpContext.Request.Query["page"]);
                if (Page < 1) Page = 1;
            }

            query.ForSite(Site).Paginate(page, ItemsPerPage);
            if (configureQuery != null)
            {
                await configureQuery(query);
            }

            return query;
        }

        protected virtual Task<IRepositoryQuery<TEntity>> ApplyListConditionsAsync(IRepositoryQuery<TEntity> query)
        {
            return ApplyPublishConditionsAsync(query);
        }

        protected virtual Task<IRepositoryQuery<TEntity>> ApplyShowConditionsAsync(IRepositoryQuery<TEntity> query)
        {
            return ApplyPublishConditionsAsync(query);
        }
    }
}
