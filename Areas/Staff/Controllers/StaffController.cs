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

namespace f7.Areas.Staff.Controllers
{
    [Authorize("Admin role")]
    [Route("[controller]/[action]")]
    public class StaffController : Controller
    {
        private readonly f7DbContext _context;

        public StaffController(f7DbContext context)
        {
            _context = context;
        }

        // GET: Staff
        [HttpGet("{page:int?}")]
        public async Task<IActionResult> Index([FromServices] PagingService pagingService, int page = 1)
        {
            const int PER_PAGE = 20;

            // if (page <= 1) page = 1;
            // var totalPage = (double)(await _context.items.CountAsync()) / PER_PAGE;
            // TempData["PagingModel"] = new PagingModel()
            // {
            //     countpages = (int)Math.Ceiling(totalPage),
            //     currentpage = page,
            //     generateUrl = page =>
            //         this.Url.Action("index", "item", new { page })
            // };
            // var staffs = await _context.staffs.Skip(PER_PAGE * (page - 1))
            //                                 .Take(PER_PAGE)
            //                                 .ToListAsync();

            var result = await pagingService.PagingHelper<StaffModels>(
                page, PER_PAGE, "staff", this.Url, this.TempData);
            TempData["PagingModel"] = result.Item2;
            return View(result.Item1);
        }

        // GET: Staff/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffModels = await _context.staffs
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffModels == null)
            {
                return NotFound();
            }

            return View(staffModels);
        }

        // GET: Staff/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,StaffName,Gender,DateOfBirth,Phone,Address,Position")] StaffModels staffModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffModels);
        }

        // GET: Staff/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffModels = await _context.staffs.FindAsync(id);
            if (staffModels == null)
            {
                return NotFound();
            }
            return View(staffModels);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StaffId,StaffName,Gender,DateOfBirth,Phone,Address,Position")] StaffModels staffModels)
        {
            if (id != staffModels.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffModelsExists(staffModels.StaffId))
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
            return View(staffModels);
        }

        // GET: Staff/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffModels = await _context.staffs
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffModels == null)
            {
                return NotFound();
            }

            return View(staffModels);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var staffModels = await _context.staffs.FindAsync(id);
            _context.staffs.Remove(staffModels);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffModelsExists(string id)
        {
            return _context.staffs.Any(e => e.StaffId == id);
        }
    }
}
