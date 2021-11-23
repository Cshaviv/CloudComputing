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
    public class ManagersController : Controller
    {
        private readonly ManagersContext _context;

        public ManagersController(ManagersContext context)
        {
            _context = context;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Manager.ToListAsync());
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,password")] Manager manager)
        {
            if (id != manager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.Id))
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
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Manager
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manager = await _context.Manager.FindAsync(id);
            _context.Manager.Remove(manager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerExists(int id)
        {
            return _context.Manager.Any(e => e.Id == id);
        }
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult Predication()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult LogIn([Bind("Email,Password")] Manager managers)
        {
            bool flag = false;
            if (ModelState.IsValid)
            {
                foreach (var manager in _context.Manager)
                {
                    if (manager.Email == manager.Email && manager.Password == manager.Password)
                    {
                        ViewBag.text = "true";
                        flag = true;
                        return View("~/Views/Managers/Welocome.cshtml", managers);   
                    }
                }
                if (flag == false)
                    ViewBag.text = "false";
            }
            return View();
            //return null;//איך מודיעים על שגיאה?
            //sessionStorage.isMeneger = true;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult Predication([Bind("city,  season,  temperature,  humidity,  day")] Icecream pred)
        {
            if (pred.humidity == null && pred.temperature == null)
                ViewBag.text = "";
            else
            {
                
                string ans = Icecream.PredictIcecream(pred.city, pred.season, pred.temperature, pred.humidity, pred.day);

                ViewBag.text = "the prediction is " + ans;
            }
            return View();

        }
        public IActionResult LogOut()
        {            
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Welocome()
        {
            return View();
        }

    }
}
