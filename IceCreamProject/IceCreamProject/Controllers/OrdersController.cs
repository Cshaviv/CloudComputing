using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IceCreamProject.Models;
namespace IceCreamProject.Controllers
{

    public class OrdersController : Controller
    {
        private readonly OrdersContext _context;
        private readonly IceCreamFlavorsContext _context2;

        public OrdersController(OrdersContext context,IceCreamFlavorsContext context2)
        {
            
            _context = context;
            _context2 = context2;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewBag.Message = _context2.IceCreamFlavor.ToList();
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,PhoneNumber,Email,City,Street,HouseNumber,Flavor,Season,Temperature,Date,Humidity,Pressure")] Orders orders)
        {
            if (ModelState.IsValid)
            { //להוסיף תקינות רחוב
                orders.Date = DateTime.Now;
                if (orders.Date.Month >= 12 && orders.Date.Month < 3)
                    orders.Season = "Winter";
                if (orders.Date.Month >= 3 && orders.Date.Month < 6)
                    orders.Season = "Spring";
                if (orders.Date.Month >= 6 && orders.Date.Month < 9)
                    orders.Season = "Summer";
                if (orders.Date.Month >= 9 && orders.Date.Month < 12)
                    orders.Season = "Fall";
                WeatherClass weather = new WeatherClass();
                Main result = weather.CheckWeather(orders.City);
                orders.Pressure = result.pressure;
                orders.Humidity = result.humidity;
                orders.Temperature = (float)result.temp;
               
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return View("~/Views/Orders/Successful.cshtml");
            }
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,PhoneNumber,Email,City,Street,HouseNumber,Flavor,Season,Temperature,Date,Humidity,Pressure")] Orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
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
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
        public IActionResult Successful()
        {        
            return View();
        }
        public IActionResult ShowGraph(DateTime date1, DateTime date2)
        {
            int counter = 1;
            List<Temperature> temps = new List<Temperature>();
            for (DateTime i = date1; i < date2; i = i.AddDays(1))
            {
                Temperature t = new Temperature { Id = counter++, Day = i.Day, Month = i.Month, TempValue = 0 };

                foreach (var item in _context.Orders)
                {
                    if (item.Date.Day == i.Day && item.Date.Month == i.Month)
                        t.TempValue++;//the number of orders in this date
                }
                temps.Add(t);

            }
            return View(temps);
        }

        public IActionResult Graph()
        {
            return View();
        }
    }
}
