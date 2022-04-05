using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using f7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace f7.Areas.Batch
{
    [Authorize("Admin role")]
    [Route("[controller]/[action]")]
    public class BatchController : Controller
    {
        private readonly f7DbContext _context;

        public BatchController(f7DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpGet("{page:int}")]
        public async Task<IActionResult> Index(string keyWord, string orderBy, int page = 1)
        {
            const int PER_PAGE = 8;

            page = page < 1 ? 0 : page - 1;
            string queryString = "";
            var batches = _context.batches as IQueryable<BatchModels>;
            if (!String.IsNullOrEmpty(keyWord))
            {
                batches = batches.Where(b => b.BatchId.Contains($"{keyWord}"));
                queryString += $"keyWord={keyWord}";
            }
            if (!String.IsNullOrEmpty(orderBy))
            {
                batches = SwitchCase(batches, orderBy);
                queryString += $"&orderBy={orderBy}";
            }
            var totalBatch = await batches.CountAsync();

            var pagingModel = new PagingModel()
            {
                countpages = (int)Math.Ceiling((double)(totalBatch) / PER_PAGE),
                currentpage = page + 1,
                generateUrl = (p) => Url.Action("index", "batch", new { page = p }) + "?" + queryString
            };
            TempData["PagingModel"] = pagingModel;
            var listBatches = batches.Skip(PER_PAGE * page).Take(PER_PAGE).ToList();
            return View(listBatches);
        }
        // public static int PropCompare(BatchModels batchModels, string keyWord)
        // {
        //     var props = batchModels.GetType().GetProperties();
        //     foreach (var prop in props)
        //     {
        //         if (String.Compare(prop.Name, keyWord, StringComparison.OrdinalIgnoreCase) == 0)
        //         {
        //             var r = (int)prop.GetValue(batchModels, null);
        //             return r;
        //         }
        //     }
        //     return 0;
        // }
        // Haizz chans
        public static IQueryable<BatchModels> SwitchCase(IQueryable<BatchModels> batches, string orderBy)
        {
            switch (orderBy)
            {
                case "amount":
                    batches = batches.OrderBy(b => b.Amount);
                    break;
                case "amount_des":
                    batches = batches.OrderByDescending(b => b.Amount);
                    break;
                case "quantity":
                    batches = batches.OrderBy(b => b.Quantity);
                    break;
                case "quantity_des":
                    batches = batches.OrderByDescending(b => b.Quantity);
                    break;
                case "price":
                    batches = batches.OrderBy(b => b.Price);
                    break;
                case "price_des":
                    batches = batches.OrderByDescending(b => b.Price);
                    break;
                case "remain":
                    batches = batches.OrderBy(b => b.Remain);
                    break;
                case "remain_des":
                    batches = batches.OrderByDescending(b => b.Remain);
                    break;
                case "expiredate":
                    batches = batches.OrderBy(b => b.ExpireDate);
                    break;
                case "expiredate_des":
                    batches = batches.OrderByDescending(b => b.ExpireDate);
                    break;
                case "state":
                    batches = batches.OrderBy(b => b.State);
                    break;
                case "state_des":
                    batches = batches.OrderByDescending(b => b.State);
                    break;
                case "batchid_des":
                    batches = batches.OrderByDescending(b => b.BatchId);
                    break;
                default:
                    batches = batches.OrderBy(b => b.BatchId);
                    break;
            }
            return batches;
        }

        // GET: Batch/Details/5
        public async Task<IActionResult> Detail(string BatchId, string WarehouseReceiptId)
        {
            if (BatchId == null || WarehouseReceiptId == null)
            {
                return NotFound();
            }

            var batchModels = await _context.batches
                .Include(b => b.Item)
                .Include(b => b.WarehouseReceipt)
                .FirstOrDefaultAsync(m => m.BatchId == BatchId
                    && m.WarehouseReceiptId == WarehouseReceiptId);
            if (batchModels == null)
            {
                return NotFound();
            }

            return PartialView("_DetailsPartial", batchModels);
        }

        // GET: Batch/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId");
            ViewData["ProviderId"] = new SelectList(_context.providers, "Id", "Id");
            ViewData["WarehouseReceiptId"] = new SelectList(_context.warehouseReceipts, "WarehouseReceiptId", "WarehouseReceiptId");
            return PartialView();
        }

        // POST: Batch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BatchId,ItemId,Price,Quantity,Amount,Remain,State,ReceivingDate,ManufacturingDate,ExpiresDate,WarehouseReceiptId,ProviderId")] BatchModels batchModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(batchModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId", batchModels.ItemId);
            ViewData["ProviderId"] = new SelectList(_context.providers, "Id", "Id", batchModels.ProviderId);
            ViewData["WarehouseReceiptId"] = new SelectList(_context.warehouseReceipts, "WarehouseReceiptId", "WarehouseReceiptId", batchModels.WarehouseReceiptId);
            return View(batchModels);
        }

        // GET: Batch/Edit/5
        public async Task<IActionResult> GetEdit(string warehouseReceiptId, string batchId)
        {
            if (batchId == null || warehouseReceiptId == null)
            {
                return NotFound();
            }

            var batchModels = await _context.batches
                .Where(b => b.BatchId == batchId && b.WarehouseReceiptId == warehouseReceiptId)
                .FirstOrDefaultAsync();
            if (batchModels == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId", batchModels.ItemId);
            ViewData["ProviderId"] = new SelectList(_context.providers, "Id", "Id", batchModels.ProviderId);
            ViewData["WarehouseReceiptId"] = new SelectList(
                _context.warehouseReceipts, "WarehouseReceiptId", "WarehouseReceiptId", batchModels.WarehouseReceiptId);
            return PartialView("_EditPartial", batchModels);
        }

        // POST: Batch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEdit(BatchModels batchModels)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(batchModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BatchModelsExists(batchModels.BatchId))
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
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId", batchModels.ItemId);
            ViewData["ProviderId"] = new SelectList(_context.providers, "Id", "Id", batchModels.ProviderId);
            ViewData["WarehouseReceiptId"] = new SelectList(_context.warehouseReceipts, "WarehouseReceiptId", "WarehouseReceiptId", batchModels.WarehouseReceiptId);
            return View(batchModels);
        }

        public IActionResult GetEditExpiration(string batchId)
        {
            var batch = (_context.batches.FirstOrDefault());
            var datetime = (DateTime)batch.ExpireDate;
            var formatedDate = datetime.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var batches = _context.batches as IQueryable<BatchModels>;
            if (!String.IsNullOrEmpty(batchId))
            {
                batches = batches.Where(b => b.BatchId == batchId);
            }
            batches = batches.Select(b => new BatchModels
            {
                BatchId = b.BatchId,
                ItemId = b.ItemId,
                WarehouseReceiptId = b.WarehouseReceiptId,
                Item = b.Item,
                ReceivingDate = b.ReceivingDate,
                ExpireDate = b.ExpireDate,
                ManufacturingDate = b.ManufacturingDate
            });
            return View("EditExpiration", batches.ToList());
        }
        public IActionResult PostEditExpiration(
            DateTime expireDate,
            DateTime manufacturingDate,
            string batchId,
            string warehouseReceiptId)
        {
            var batch = _context.batches
                .Where(x => x.BatchId == batchId && x.WarehouseReceiptId == warehouseReceiptId)
                .FirstOrDefault();
            batch.ManufacturingDate = manufacturingDate;
            batch.ExpireDate = expireDate;

            _context.SaveChanges();
            return Ok();
        }
        // GET: Batch/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batchModels = await _context.batches
                .Include(b => b.Item)
                .Include(b => b.Provider)
                .Include(b => b.WarehouseReceipt)
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (batchModels == null)
            {
                return NotFound();
            }

            return View(batchModels);
        }

        // POST: Batch/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(string batchId, string warehouseReceiptId)
        {
            var batchModels = await _context.batches.FindAsync(batchId, warehouseReceiptId);
            _context.batches.Remove(batchModels);
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BatchModelsExists(string id)
        {
            return _context.batches.Any(e => e.BatchId == id);
        }
    }
}
