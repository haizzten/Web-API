using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using f7.Models;

namespace f7.Models.Models.Areas.OrderDetail.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly f7DbContext _context;

        public OrderDetailController(f7DbContext context)
        {
            _context = context;
        }

        // GET: OrderDetail
        public async Task<IActionResult> Index()
        {
            var f7DbContext = _context.orderDetail.Include(o => o.Item).Include(o => o.Order);
            return View(await f7DbContext.ToListAsync());
        }

        // GET: OrderDetail/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailModels = await _context.orderDetail
                .Include(o => o.Item)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (orderDetailModels == null)
            {
                return NotFound();
            }

            return View(orderDetailModels);
        }

        // GET: OrderDetail/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId");
            ViewData["OrderId"] = new SelectList(_context.orders, "OrderId", "OrderId");
            return View();
        }

        // POST: OrderDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,OrderId,Quantity")] OrderDetailModels orderDetailModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetailModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId", orderDetailModels.ItemId);
            ViewData["OrderId"] = new SelectList(_context.orders, "OrderId", "OrderId", orderDetailModels.OrderId);
            return View(orderDetailModels);
        }

        // GET: OrderDetail/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailModels = await _context.orderDetail.FindAsync(id);
            if (orderDetailModels == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId", orderDetailModels.ItemId);
            ViewData["OrderId"] = new SelectList(_context.orders, "OrderId", "OrderId", orderDetailModels.OrderId);
            return View(orderDetailModels);
        }

        // POST: OrderDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemId,OrderId,Quantity")] OrderDetailModels orderDetailModels)
        {
            if (id != orderDetailModels.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetailModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailModelsExists(orderDetailModels.ItemId))
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
            ViewData["ItemId"] = new SelectList(_context.items, "ItemId", "ItemId", orderDetailModels.ItemId);
            ViewData["OrderId"] = new SelectList(_context.orders, "OrderId", "OrderId", orderDetailModels.OrderId);
            return View(orderDetailModels);
        }

        // GET: OrderDetail/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailModels = await _context.orderDetail
                .Include(o => o.Item)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (orderDetailModels == null)
            {
                return NotFound();
            }

            return View(orderDetailModels);
        }

        // POST: OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderDetailModels = await _context.orderDetail.FindAsync(id);
            _context.orderDetail.Remove(orderDetailModels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailModelsExists(string id)
        {
            return _context.orderDetail.Any(e => e.ItemId == id);
        }
    }
}
