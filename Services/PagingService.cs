using System.Linq;
using System.Web;
using System.Threading.Tasks;
using f7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;

namespace f7.Services
{
    public class PagingService
    {
        private f7DbContext _dbContext;
        public PagingService(f7DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<(IEnumerable<TModel>, PagingModel)> PagingHelper<TModel>(
            int currentPage,
            int PER_PAGE,
            string controllerName,
            IUrlHelper urlHelper,
            ITempDataDictionary tempData) where TModel : class
        {
            if (currentPage <= 1) currentPage = 1;
            var totalPages = (int)System.Math.Ceiling(
                (double)(await _dbContext.Set<TModel>().CountAsync()) / PER_PAGE);
            var pagingModel = new PagingModel()
            {
                countpages = totalPages,
                currentpage = currentPage,
                generateUrl = page => urlHelper.Action("index", controllerName, new { page })
            };

            var result = await _dbContext.Set<TModel>()
                                         .Skip(PER_PAGE * (currentPage - 1))
                                         .Take(PER_PAGE)
                                         .ToListAsync();
            return (result, pagingModel);
        }
    }
}
