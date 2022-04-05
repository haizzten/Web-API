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
using Microsoft.AspNetCore.Http;

namespace f7.Models.Models.Areas.Order.Controllers
{
    // [Authorize(Roles = RoleName.Administrator)]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private const string CART_KEY = "f7Cart";
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

            // var result = await pagingService.PagingHelper<OrderModels>(
            //     page, PER_PAGE, "Order", this.Url, this.TempData);
            // TempData["PagingModel"] = result.Item2;

            var orders = _context.orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Where(o => true);

            var totalOrders = orders.Count();
            var totalPage = (int)Math.Ceiling((double)(totalOrders) / PER_PAGE);
            if (page > totalPage)
            {
                page = page <= 1 ? 1 : totalPage;
            }

            orders = orders.Skip(PER_PAGE * (page - 1)).Take(PER_PAGE);

            TempData["PagingModel"] = new PagingModel()
            {
                countpages = totalPage,
                currentpage = page,
                generateUrl = (p) => Url.Action("index", "order", new { page = p })

            };
            var orderModels = orders.OrderBy(od => od.CreatedDate);

            // var customer = new List<CustomerModels>();
            List<OrderIndexViewModel> viewModel = new List<OrderIndexViewModel>();

            orderModels.ToList().ForEach(o =>
                {
                    viewModel.Add(new OrderIndexViewModel
                    {
                        CreatedDate = o.CreatedDate.ToString(),
                        CustomerName = o.Customer.Name,
                        CustomerId = o.CustomerId,
                        OrderId = o.OrderId,
                        PaymentMethod = o.PaymentMethod,
                        StaffName = o.Staff.Name,
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
        // [HttpGet]
        // public IActionResult Create()
        // {
        //     ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId");
        //     return View();
        // }

        [HttpPost]
        // [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateList(int[] quantity, string[] itemid)
        {

            var totalAmount = 0;
            var OrderId = Guid.NewGuid().ToString();
            var items = _context.items.Select(i => new { i.ItemId, i.SellingPrice }).ToList();
            for (int i = 0; i < itemid.Length; i++)
            {
                _context.orderDetail.Add(new OrderDetailModels
                {
                    ItemId = itemid[i],
                    OrderId = OrderId,
                    Quantity = quantity[i]
                });
                totalAmount += quantity[i] * items.Where(it => it.ItemId == itemid[i])
                    .FirstOrDefault().SellingPrice;
            }
            var order = new OrderModels
            {
                OrderId = OrderId,
                CreatedDate = DateTime.Now,
                StaffId = _userManager.GetUserId(this.User),
                State = OrderState.Waiting,
                CustomerId = "1",
                VAT = 10,
                TotalAmount = totalAmount
            };
            _context.orders.Add(order);
            await _context.SaveChangesAsync();
            HttpContext.Session.SetString(CART_KEY, "");

            return Redirect(Url.Action("index", "order"));
        }

        // // POST: Order/Create
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("OrderId,CreatedDate,CustomerId,PaymentMethod,VatPercent,StaffId")] OrderModels orderModels)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(orderModels);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     ViewData["StaffId"] = new SelectList(_context.staffs, "StaffId", "StaffId", orderModels.StaffId);
        //     return View(orderModels);
        // }

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
        public IActionResult Pay(string id)
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
            order.State = OrderState.Paid;
            order.PaymentMethod = "Tiền mặt";
            _context.SaveChanges();
            return Content(OrderState.Paid);
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
