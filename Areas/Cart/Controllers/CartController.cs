using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using f7.Models;
using f7.Models.Models.Areas.Cart.Models;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace f7.Models.Models.Areas.Cart.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private const string CART_KEY = "f7Cart";
        private f7DbContext _dbContext;
        private readonly ILogger<CartController> _logger;
        public CartController(f7DbContext dbContext, ILogger<CartController> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([Bind("ItemId", "Quantity")] CartModels cart)
        {
            var item = await _dbContext.items.Where(item => item.ItemId == cart.ItemId)
                                             .FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }

            var sessionItems = GetSessionItems();
            var cartModel = sessionItems.Find(item => item.ItemId == cart.ItemId);
            if (cartModel != null)
            {
                cartModel.Quantity++;
                _logger.LogInformation("--Tăng số lượng");
            }
            else
            {
                cartModel = new CartModels
                {
                    ItemId = cart.ItemId,
                    Quantity = 1
                };
                sessionItems.Add(cartModel);
                _logger.LogInformation("--Thêm mới");
            }

            SetSessionItems(sessionItems);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> IndexPartial()
        {
            var itemsInSession = GetSessionItems();
            int totalAmout = 0;
            List<CartViewModel> itemsInCart = new List<CartViewModel>();
            itemsInSession.ForEach(items => Console.WriteLine("--" + items.ItemId + "\n"));
            itemsInSession.ForEach((itemsinsess) =>
            {
                var item = _dbContext.items.Where(i => i.ItemId == itemsinsess.ItemId)
                                           .Select(i => new
                                           {
                                               i.ItemId,
                                               i.ItemName,
                                               i.SellingPrice,
                                               i.Unit
                                           })
                                           .FirstOrDefault();

                totalAmout += itemsinsess.Quantity * item.SellingPrice;

                itemsInCart.Add(new CartViewModel
                {
                    ItemName = item.ItemName,
                    ItemsId = item.ItemId,
                    Quantity = itemsinsess.Quantity,
                    SellingPrice = item.SellingPrice,
                    Amount = itemsinsess.Quantity * item.SellingPrice,
                    Unit = item.Unit
                });
            });
            await Task.CompletedTask;
            return PartialView("_CartIndexPartial", itemsInCart);
        }
        public void SetSessionItems(List<CartModels> items)
        {
            var jsonObject = JsonConvert.SerializeObject(items);
            HttpContext.Session.SetString(CART_KEY, jsonObject);
        }
        public List<CartModels> GetSessionItems()
        {
            string jsonCart = HttpContext.Session.GetString(CART_KEY);
            if (String.IsNullOrEmpty(jsonCart)) return new List<CartModels>();

            List<CartModels> items = JsonConvert.DeserializeObject<List<CartModels>>(jsonCart);
            return items;
        }
    }
}
