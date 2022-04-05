using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using f7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using f7.Areas.Batch;

namespace f7.Areas.WarehouseReceipt
{
    [Authorize("Admin role")]
    [Route("[controller]/[action]")]
    public class WarehouseReceiptController : Controller
    {
        private readonly f7DbContext _context;

        [BindProperty]
        public List<BatchModels> WRDModel { get; set; }

        public WarehouseReceiptController(f7DbContext context)
        {
            _context = context;
        }

        [HttpGet("/itemsearch")]
        public IActionResult itemsearch(string term)
        {
            var itemList = _context.items
                .Where(item => item.ItemName.Contains($"{term}"))
                .Select(item => new
                {
                    id = item.ItemId,
                    text = item.ItemName,
                    itemName = item.ItemName,
                    itemUnit = item.Unit,
                })
                .ToList();

            return Json(itemList);
        }
        [HttpGet("{index}")]
        public IActionResult GetAddBatch(int index)
        {
            var data = new WRDetailViewModel()
            {
                index = index
            };
            return PartialView("_AddBatchPartial", data);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.warehouseReceipts.ToListAsync());
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouseReceiptModels = await _context.warehouseReceipts
                .FirstOrDefaultAsync(m => m.WarehouseReceiptId == id);
            if (warehouseReceiptModels == null)
            {
                return NotFound();
            }

            return View(warehouseReceiptModels);
        }
        public IActionResult BatchIndex(string warehouseReceiptId)
        {
            // var batches = new List<BatchIndexPartialViewModels>();
            // _context.batches.Where(b => b.WarehouseReceiptId == warehouseReceiptId)
            //     .Include(b => b.WarehouseReceipt)
            //     // .Join(
            //     //     _context.providers,
            //     //     b => b.ProviderId,
            //     //     p => p.Id,
            //     //     (b, p) => new
            //     //     {
            //     //         batch = b,
            //     //         providerName = p.Name
            //     //     })
            //     .ToList()
            //     .ForEach(b => batches.Add(new BatchIndexPartialViewModels
            //     {
            //         batch = b,
            //         providerName = b.WarehouseReceipt.ProviderId
            //     }));

            // return PartialView("_BatchesIndexPartial", batches);
            return Ok();
        }

        // GET: WarehouseReceipt/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WarehouseReceipt/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            WarehouseReceiptModels inpModel,
            string[] itemid,
            string[] batchid,
            string[] providerid,
            int[] price,
            int[] quantity,
            IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var totalAmount = 0;
                for (int i = 0; i < itemid.Length; i++)
                {
                    totalAmount += price[i] * quantity[i];
                }

                var model = new WarehouseReceiptModels()
                {
                    WarehouseReceiptId = inpModel.WarehouseReceiptId,
                    DateTime = inpModel.DateTime ?? DateTime.Now,
                    DelivererName = inpModel.DelivererName,
                    PurchaseOrderId = inpModel.PurchaseOrderId,
                    WarehouseName = inpModel.WarehouseName ?? "Kho cửa hàng",
                    TotalAmount = totalAmount
                };
                _context.Add(model);

                for (int i = 0; i < itemid.Length; i++)
                {
                    _context.batches.Add(new BatchModels
                    {
                        ID = Guid.NewGuid().ToString(),
                        ItemId = itemid[i],
                        BatchId = batchid[i],
                        Price = price[i],
                        Quantity = quantity[i],
                        ReceivingDate = DateTime.Now,
                        WarehouseReceiptId = inpModel.WarehouseReceiptId,
                        State = BatchState.InStock,
                        Amount = price[i] * quantity[i],
                        Remain = quantity[i],
                        WarehouseReceipt = model
                    });
                    await _context.SaveChangesAsync();
                }
            }

            // IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return Redirect(Url.Action("index"));
        }

        // GET: WarehouseReceipt/Edit/5
        public async Task<IActionResult> GetEdit(string warehousereceiptid)
        {
            if (warehousereceiptid == null)
            {
                return NotFound();
            }

            var warehouseReceiptModels = await _context.warehouseReceipts.FindAsync(warehousereceiptid);
            if (warehouseReceiptModels == null)
            {
                return NotFound();
            }
            return PartialView("_EditPartial", warehouseReceiptModels);
        }

        // POST: WarehouseReceipt/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEdit(string warehouseReceiptId, [Bind("Id,DateTime,DelivererName,AttachedOriginalDocumentId,WarehouseName,TotalAmount,WarehouseId,WarehouseReceiptId")] WarehouseReceiptModels warehouseReceiptModels)
        {
            if (warehouseReceiptId != warehouseReceiptModels.WarehouseReceiptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouseReceiptModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseReceiptModelsExists(warehouseReceiptModels.WarehouseReceiptId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return View(warehouseReceiptModels);
        }

        // GET: WarehouseReceipt/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouseReceiptModels = await _context.warehouseReceipts
                .FirstOrDefaultAsync(m => m.WarehouseReceiptId == id);
            if (warehouseReceiptModels == null)
            {
                return NotFound();
            }

            return View(warehouseReceiptModels);
        }

        // POST: WarehouseReceipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var warehouseReceiptModels = await _context.warehouseReceipts.FindAsync(id);
            _context.warehouseReceipts.Remove(warehouseReceiptModels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseReceiptModelsExists(string id)
        {
            return _context.warehouseReceipts.Any(e => e.WarehouseReceiptId == id);
        }
    }
}
