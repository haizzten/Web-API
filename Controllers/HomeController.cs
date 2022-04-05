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

namespace f7.Models.Models.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly f7DbContext _context;
        public HomeController(
            ILogger<HomeController> logger,
            f7DbContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }
        [Route("/")]
        public IActionResult MainPage()
        {
            return View();
        }
        [Route("/Order/Create/{page:int?}")]
        public async Task<IActionResult> Index([FromServices] PagingService pagingService,
            string keyWord, string searchBy, int page = 1)
        {

            const int PER_PAGE = 12;
            if (page <= 1) page = 1;

            var items = _context.items as IQueryable<ItemModels>;
            if (keyWord != null)
            {
                if (searchBy == "ItemName")
                {
                    items = items.Where(item => item.ItemName.Contains($"{keyWord}"));
                }
                else
                {
                    items = items.Where(item => item.ItemId.Contains($"{keyWord}"));
                }
            }

            var totalPage = (double)(await _context.items.CountAsync()) / PER_PAGE;

            TempData["PagingModel"] = new PagingModel()
            {
                countpages = (int)Math.Ceiling(totalPage),
                currentpage = page,
                generateUrl = page =>
                    this.Url.Action("create", "order", new { page })
            };
            items = items.Skip(PER_PAGE * (page - 1)).Take(PER_PAGE);

            // var items = await pagingService.PagingHelper<ItemModels>(
            //     page, PER_PAGE, "Home", this.Url, this.TempData);
            // var pagingModel = items.Item2;

            return View("HomeIndex", items.ToList());
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
