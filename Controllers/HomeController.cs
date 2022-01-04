using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using f7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using f7.Services;

namespace f7.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly f7DbContext _dbContext;
        public HomeController(
            ILogger<HomeController> logger,
            f7DbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [Route("/{page:int?}")]
        public async Task<IActionResult> Index([FromServices] PagingService pagingService, int page = 1)
        {

            const int PER_PAGE = 12;

            // if (page <= 1) page = 1;

            // var totalPage = (double)(await _dbContext.items.CountAsync()) / PER_PAGE;
            // TempData["PagingModel"] = new PagingModel()
            // {
            //     countpages = (int)Math.Ceiling(totalPage),
            //     currentpage = page,
            //     generateUrl = page =>
            //         this.Url.Action("index", "home", new { page })
            // };
            // var items = await _dbContext.items.Skip(PER_PAGE * (page - 1))
            //                                 .Take(PER_PAGE)
            //                                 .ToListAsync();
            // return View(items);
            var items = await pagingService.PagingHelper<ItemModels>(
                page, PER_PAGE, "Home", this.Url, this.TempData);
            var pagingModel = items.Item2;
            TempData["PagingModel"] = pagingModel;

            return View("HomeIndex", items.Item1);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
