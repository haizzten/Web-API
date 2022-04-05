using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using f7.Models;
using f7.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using f7.Areas.Item;

namespace f7.Areas.Item
{
    [Authorize("Admin role")]
    [Route("[controller]/[action]")]
    public class ItemController : Controller
    {
        private readonly f7DbContext _context;
        ILogger<ItemController> _logger;

        public ItemController(f7DbContext context, ILogger<ItemController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Item
        [HttpGet("{page:int?}")]
        public async Task<IActionResult> Index(string price, string name, string itemName, int page = 1)
        {
            // var ItemsContext = _context.items as IQueryable<ItemModels>;
            // ItemsContext = ItemsContext.Where(i => i.ItemName.Contains($"{itemName}"));
            // ItemsContext = ItemsContext.OrderByDescending(i => i.SellingPrice);
            // ItemsContext = ItemsContext.Skip(0).Take(10);
            // var ItemsSelect = ItemsContext.Select(i => new
            // {
            //     Item = new ItemModels { ItemName = i.ItemName, SellingPrice = i.SellingPrice },
            //     Total = ItemsContext.Count()
            // })
            // .ToList();

            const int PER_PAGE = 10;
            var itemIndexViewModel = new ItemIndexViewModel();
            var items = _context.items as IQueryable<ItemModels>;
            string queryString = "";
            if (itemName != null)
            {
                items = items.Where(i => i.ItemName.Contains($"{itemName}"));
                queryString = queryString + $"&itemName={itemName}";
            }
            if (name == "des")
            {
                items.OrderByDescending(i => i.ItemName);
                queryString = queryString + $"&name={name}";
            }
            else if (name == "acs")
            {
                items.OrderBy(i => i.ItemName);
                queryString = queryString + $"&name={name}";
            }
            switch (price)
            {
                // case "unit":
                //     items = items.OrderBy(i => i.Unit).OrderBy(i => i.ItemName);
                //     queryString.Concat($"&orderBy=unit");
                //     break;
                // case "unit_des":
                //     items = items.OrderByDescending(i => i.Unit);
                //     queryString.Concat($"&orderBy=unit_des");
                //     break;
                case "des":
                    items = items.OrderByDescending(i => i.SellingPrice);
                    queryString.Concat($"&price=des");
                    break;
                default:
                    items = items.OrderBy(i => i.SellingPrice);
                    break;
                    // case "name_des":
                    //     items = items.OrderByDescending(i => i.SellingPrice);
                    //     queryString.Concat($"&orderBy=name_des");
                    //     break;
                    // default:
                    //     items = items.OrderBy(i => i.ItemName)
                    //                  .ThenByDescending(i => i.ItemName);
                    //     break;
            }

            var totalItems = items.Count();
            var totalPage = (int)Math.Ceiling((double)(totalItems) / PER_PAGE);
            if (page > totalPage)
            {
                page = page <= 1 ? 1 : totalPage;
            }

            items = items.Skip(PER_PAGE * (page - 1)).Take(PER_PAGE);
            var itemsList = items.ToList();

            itemIndexViewModel.itemModels = itemsList;

            TempData["PagingModel"] = new PagingModel()
            {
                countpages = totalPage,
                currentpage = page,

                generateUrl = (p) =>
                {
                    return this.Url.Action("index", "item", new { page = p }) + "?" + queryString;
                }
            };

            return View(itemIndexViewModel);
        }

        // GET: Item/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.items
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,Description,Unit,SellingPrice")] ItemModels item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Item/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Item/Edit/5
        [HttpPost("{id}")] //haizz, quên cái route value hoài
                           // [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string id, [Bind("ItemId,ItemName,Description,Unit,SellingPrice")] ItemModels item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Item/Delete/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.items
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var item = await _context.items.FindAsync(id);
            _context.items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(string id)
        {
            return _context.items.Any(e => e.ItemId == id);
        }
        // public PartialViewResult ProductionModelDetail(int id)
        // {
        //     ProductBAL obj = new ProductBAL();
        //     List<ProductionStockDetailModel> productModellist = new List<ProductionStockDetailModel>();
        //     List<ProductionStockDetailEntity> paintentity = obj.ProductionModelDetail(id);
        //     ProductionStockDetailModel productModel;
        //     if (paintentity != null)
        //     {
        //         foreach (ProductionStockDetailEntity item in paintentity)
        //         {
        //             productModel = new ProductionStockDetailModel();
        //             productModel.MakeId = item.MakeId;
        //             productModel.ModelId = item.ModelId;
        //             productModel.PartId = item.PartId;
        //             productModellist.Add(productModel);
        //         }
        //     }
        //     return PartialView(productModellist);
        // }

        [HttpGet]
        public IActionResult GetCreate()
        {
            return PartialView("_ItemCreatePartial");
        }
        [HttpPost]
        public IActionResult PostCreate(ItemModels model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation(model.ItemName);
                _context.items.Add(model);
                _context.SaveChanges();
                this.Response.StatusCode = 203;
                return Content("Creating succeed");
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest();
        }
        [HttpGet]
        public IActionResult GetEdit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var item = _context.items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return PartialView("_ItemEditPartial", item);
        }

        [HttpPost]
        public IActionResult PostEdit([Bind("ItemId,ItemName,Description,Unit,SellingPrice,Image")] ItemModels model)
        {
            if (ModelState.IsValid)
            {
                //     try
                //     {
                //         _context.Update(model);
                //         _context.SaveChanges();
                //     }
                //     catch (DbUpdateConcurrencyException)
                //     {
                //         if (!ItemExists(model.ItemId))
                //         {
                //             return NotFound();
                //         }
                //         else
                //         {
                //             throw;
                //         }
                //     }
                //     return Ok();
                var item = _context.items.Where(x => x.ItemId == model.ItemId)
                                         .AsNoTracking()
                                         .FirstOrDefault();
                if (item != null)
                {
                    _context.items.Update(model);
                    _context.SaveChanges();
                }
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult PostDelete(string id)
        {
            if (ModelState.IsValid)
            {
                var item = _context.items.Where(x => x.ItemId == id)
                         .AsNoTracking()
                         .FirstOrDefault();
                if (item != null)
                {
                    _context.items.Remove(item);
                    _context.SaveChanges();
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}
