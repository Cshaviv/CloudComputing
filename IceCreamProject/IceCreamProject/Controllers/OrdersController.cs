using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IceCreamProject.Models;
using IceCreamProject.Data;

namespace IceCreamProject.Controllers
{

    public class OrdersController : Controller
    {
       // private readonly OrdersContext _context;
       // private readonly IceCreamFlavorsContext _context2;
        // private readonly IceCreamContext _context3;
        IceCreamContext context = new IceCreamContext();
        //public OrdersController(OrdersContext context,IceCreamFlavorsContext context2)
        //{
            
        //    _context = context;
        //    _context2 = context2;
        //    var _context3 = new IceCreamContext();
        //    _context3.Orders.Add(new Orders());
        //    _context3.SaveChangesAsync();
        //}

        // GET: Orders
        public IActionResult Index()
        {
            return View(context.Orders.ToList());
            //return View(await context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = context.Orders
                .FirstOrDefault(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewBag.Message = context.IceCreamFlavors.ToList();
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
            {

                //AddressChecking addressChecking = new AddressChecking();
                ////Boolean result = addressChecking.CheckAddress(City, Street);
                ////bool flag = checkStreet(orders.City, orders.Street);
                //if (addressChecking.CheckAddress(orders.City,orders.Street))
                //{
                //    orders.Date = DateTime.Now;
                //    if (orders.Date.Month >= 12 && orders.Date.Month < 3)
                //        orders.Season = "Winter";
                //    if (orders.Date.Month >= 3 && orders.Date.Month < 6)
                //        orders.Season = "Spring";
                //    if (orders.Date.Month >= 6 && orders.Date.Month < 9)
                //        orders.Season = "Summer";
                //    if (orders.Date.Month >= 9 && orders.Date.Month < 12)
                //        orders.Season = "Fall";
                //    WeatherClass weather = new WeatherClass();
                //    Main result = weather.CheckWeather(orders.City);
                //    orders.Pressure = result.pressure;
                //    orders.Humidity = result.humidity;
                //    orders.Temperature = (float)result.temp;
                var OrderCheck = new OrdersCheck();
                if (OrderCheck.Check(orders)!= null)
                {
                    context.Orders.Add(orders);
                    await context.SaveChangesAsync();
                    return View("~/Views/Orders/Successful.cshtml");
                }           
                else //-להודיע על שגיאה
                {
                    ViewBag.Message = context.IceCreamFlavors.ToList();//for combo box of flavors in the window
                    ViewBag.Data = string.Format("The address is not correct");
                    return View();
                }

            }
            return View(orders);
            //catch (Exception ex)
            //{
            //    ViewBag.TitlePopUp = "Error";
            //    ViewBag.Message = ex.Message;
            //    return View(orders);
            //}

        }
        //public bool checkStreet(string City, string Street)
        //{
        //    AddressChecking addressChecking = new AddressChecking();
        //    Boolean result = addressChecking.CheckAddress(City, Street);
        //    if (result)
        //        return true;
        //    else
        //        return false;
        //}

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await context.Orders.FindAsync(id);
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
                    context.Entry(orders).State = System.Data.Entity.EntityState.Modified;
                   // _context.Update(orders);
                    await context.SaveChangesAsync();
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = context.Orders
                .FirstOrDefault(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var orders = context.Orders.Find(id);
            context.Orders.Remove(orders);
            context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return context.Orders.Any(e => e.Id == id);
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

                foreach (var item in context.Orders)
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
