using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using f7.Models;
using Microsoft.AspNetCore.Authorization;

namespace f7.Areas.PurchaseOrder
{
    [Authorize("Admin role")]
    [Route("[controller]/[action]")]
    public class PurchaseOrder : Controller
    {
        private readonly f7DbContext _context;

        public PurchaseOrder(f7DbContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrder
        public async Task<IActionResult> Index()
        {
            var f7DbContext = _context.purchaseOrders.Include(p => p.Provider).Include(p => p.Staff);
            return View(await f7DbContext.ToListAsync());
        }

        // GET: PurchaseOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderModels = await _context.purchaseOrders
                .Include(p => p.Provider)
                .Include(p => p.Staff)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrderModels == null)
            {
                return NotFound();
            }

            return View(purchaseOrderModels);
        }

        // GET: PurchaseOrder/Create
        public IActionResult GetCreate()
        {
            ViewData["ProviderId"] = new SelectList(_context.providers, "Id", "Id");
            ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId");
            return PartialView("_PurchaseOrderCreatePartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCreate([Bind("PurchaseOrderId,ProviderId,CreatedDate,CompanyName,Address,State,StaffId")] PurchaseOrderModels purchaseOrderModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrderModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProviderId"] = new SelectList(_context.providers, "Id", "Id", purchaseOrderModels.ProviderId);
            ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId", purchaseOrderModels.StaffId);
            return View(purchaseOrderModels);
        }

        // GET: PurchaseOrder/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderModels = await _context.purchaseOrders.FindAsync(id);
            if (purchaseOrderModels == null)
            {
                return NotFound();
            }
            ViewData["ProviderId"] = new SelectList(_context.providers, "Id", "Id", purchaseOrderModels.ProviderId);
            ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId", purchaseOrderModels.StaffId);
            return View(purchaseOrderModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PurchaseOrderId,ProviderId,CreatedDate,CompanyName,Address,State,StaffId")] PurchaseOrderModels purchaseOrderModels)
        {
            if (id != purchaseOrderModels.PurchaseOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrderModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderModelsExists(purchaseOrderModels.PurchaseOrderId))
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
            ViewData["ProviderId"] = new SelectList(_context.providers, "Id", "Id", purchaseOrderModels.ProviderId);
            ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId", purchaseOrderModels.StaffId);
            return View(purchaseOrderModels);
        }

        // GET: PurchaseOrder/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderModels = await _context.purchaseOrders
                .Include(p => p.Provider)
                .Include(p => p.Staff)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
            if (purchaseOrderModels == null)
            {
                return NotFound();
            }

            return View(purchaseOrderModels);
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var purchaseOrderModels = await _context.purchaseOrders.FindAsync(id);
            _context.purchaseOrders.Remove(purchaseOrderModels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderModelsExists(string id)
        {
            return _context.purchaseOrders.Any(e => e.PurchaseOrderId == id);
        }
    }
}
