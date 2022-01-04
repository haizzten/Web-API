using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using f7.Models;

namespace f7.Areas.ItemDetail.Controllers
{
    public class ItemDetailsController : Controller
    {
        private readonly f7DbContext _context;

        public ItemDetailsController(f7DbContext context)
        {
            _context = context;
        }

        // GET: ItemDetails
        public async Task<IActionResult> Index()
        {
            var f7DbContext = _context.itemDetails.Include(i => i.ItemModels);
            return View(await f7DbContext.ToListAsync());
        }

        // GET: ItemDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDetailModels = await _context.itemDetails
                .Include(i => i.ItemModels)
                .FirstOrDefaultAsync(m => m.ConsignmentId == id);
            if (itemDetailModels == null)
            {
                return NotFound();
            }

            return View(itemDetailModels);
        }

        // GET: ItemDetails/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId");
            return View();
        }

        // POST: ItemDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ConsignmentId,CostPrice,Manufacturer,ManufacturingDate,ExpiresDate")] ItemDetailModels itemDetailModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemDetailModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId", itemDetailModels.ItemId);
            return View(itemDetailModels);
        }

        // GET: ItemDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDetailModels = await _context.itemDetails.FindAsync(id);
            if (itemDetailModels == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId", itemDetailModels.ItemId);
            return View(itemDetailModels);
        }

        // POST: ItemDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemId,ConsignmentId,CostPrice,Manufacturer,ManufacturingDate,ExpiresDate")] ItemDetailModels itemDetailModels)
        {
            if (id != itemDetailModels.ConsignmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemDetailModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemDetailModelsExists(itemDetailModels.ConsignmentId))
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
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId", itemDetailModels.ItemId);
            return View(itemDetailModels);
        }

        // GET: ItemDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDetailModels = await _context.itemDetails
                .Include(i => i.ItemModels)
                .FirstOrDefaultAsync(m => m.ConsignmentId == id);
            if (itemDetailModels == null)
            {
                return NotFound();
            }

            return View(itemDetailModels);
        }

        // POST: ItemDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var itemDetailModels = await _context.itemDetails.FindAsync(id);
            _context.itemDetails.Remove(itemDetailModels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemDetailModelsExists(string id)
        {
            return _context.itemDetails.Any(e => e.ConsignmentId == id);
        }
    }
}
