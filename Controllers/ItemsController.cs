using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace f7.Controllers
{
    [Authorize]
    [Route("items/[action]")]
    public class ItemsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ItemsAsync()
        {
            // return Task.FromResult(Json(new Item
            // {
            //     name = "test iem",
            //     dateCreate = DateTime.Now
            // }) as IActionResult);

            return Json(new Item
            {
                name = "test itemmmmmmm",
                dateCreate = DateTime.Now
            });

        }
        public class Item
        {
            public string name { get; set; }
            public DateTime dateCreate { get; set; }
        }
    }
}
