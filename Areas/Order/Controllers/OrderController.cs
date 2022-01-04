using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using f7.Models;
using f7.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace f7.Areas.Order.Controllers
{
    // [Authorize(Roles = RoleName.Administrator)]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly f7DbContext _context;
        private UserManager<f7AppUser> _userManager;
        public OrderController(f7DbContext context, UserManager<f7AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Order
        [HttpGet("{page:int?}")]
        public async Task<IActionResult> Index([FromServices] PagingService pagingService, int page = 1)
        {
            const int PER_PAGE = 12;

            var result = await pagingService.PagingHelper<OrderModels>(
                page, PER_PAGE, "Order", this.Url, this.TempData);
            TempData["PagingModel"] = result.Item2;

            var orderModels = result.Item1.OrderBy(od => od.CreatedDate);

            var customer = new List<CustomerModels>();
            List<OrderIndexViewModel> viewModel = new List<OrderIndexViewModel>();

            orderModels.ToList().ForEach(o =>
            {
                var userName = _context.Users.Where(u => u.Id == o.UserId)
                                                .Select(u => u.UserName)
                                                .FirstOrDefault();
                viewModel.Add(new OrderIndexViewModel
                {
                    CreatedDate = o.CreatedDate.ToString(),
                    CustomerName = userName,
                    CustomerId = o.UserId,
                    OrderId = o.OrderId,
                    PaymentMethod = o.PaymentMethod,
                    StaffId = o.StaffId,
                    State = o.State
                });
            });

            return View(viewModel);
        }

        // GET: Order/Details/5
        [HttpGet("{id?}")]
        public async Task<IActionResult> Details([FromQuery] string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var viewModel = new DetailViewModel();
            await _context.orderDetail.Include(o => o.Item)
                                      .Include(o => o.Order)
                                      .Where(od => od.OrderId == id)
                                      .ForEachAsync(od =>
                                      {
                                          int price = od.Item.SellingPrice * od.Quantity;
                                          viewModel.Items.Add(new ItemViewModel
                                          {
                                              Unit = od.Item.Unit,
                                              Quantity = od.Quantity,
                                              ItemName = od.Item.ItemName,
                                              Price = od.Item.SellingPrice,
                                              Amount = price
                                          });
                                          viewModel.TotalPrice += price;
                                      });

            return PartialView("OrderDetail", viewModel);
        }

        // GET: Order/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId");
            return View();
        }

        [HttpPost]
        // [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateList(int[] quantity, string[] itemid)
        {
            var OrderId = Guid.NewGuid().ToString();
            for (int i = 0; i < itemid.Length; i++)
            {
                _context.orderDetail.Add(new OrderDetailModels
                {
                    ItemId = itemid[i],
                    OrderId = OrderId,
                    Quantity = quantity[i]
                });
            }
            var order = new OrderModels
            {
                OrderId = OrderId,
                CreatedDate = DateTime.Now,
                UserId = _userManager.GetUserId(this.User),
                State = OrderState.Waiting
            };
            _context.orders.Add(order);
            await _context.SaveChangesAsync();

            return Redirect(Url.Action("index", "order"));
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CreatedDate,CustomerId,PaymentMethod,VatPercent,StaffId")] OrderModels orderModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId", orderModels.StaffId);
            return View(orderModels);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModels = await _context.orders.FindAsync(id);
            if (orderModels == null)
            {
                return NotFound();
            }
            ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId", orderModels.StaffId);
            return View(orderModels);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderId,CreatedDate,CustomerId,PaymentMethod,VatPercent,StaffId")] OrderModels orderModels)
        {
            if (id != orderModels.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelsExists(orderModels.OrderId))
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
            ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId", orderModels.StaffId);
            return View(orderModels);
        }

        [HttpPost]
        public IActionResult Accept(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var order = _context.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            order.State = OrderState.Accepted;
            _context.SaveChanges();
            return Content(OrderState.Accepted);
        }

        [HttpPost]
        public IActionResult Cancel(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var order = _context.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            order.State = OrderState.Canceled;
            _context.SaveChanges();
            return Content(OrderState.Canceled);
        }


        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModels = await _context.orders
                .Include(o => o.Staff)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderModels == null)
            {
                return NotFound();
            }

            return View(orderModels);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderModels = await _context.orders.FindAsync(id);
            if (id == null) return NotFound();
            _context.orders.Remove(orderModels);
            await _context.orderDetail.Where(o => o.OrderId == id)
                                      .ForEachAsync(o => _context.orderDetail.Remove(o));
            _context.orderDetail.RemoveRange();

            _context.SaveChanges();
            return Ok();
        }

        private bool OrderModelsExists(string id)
        {
            return _context.orders.Any(e => e.OrderId == id);
        }
    }
}
