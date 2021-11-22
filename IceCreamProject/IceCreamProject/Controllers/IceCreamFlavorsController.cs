using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IceCreamProject.Models;
using System.Net;
using Firebase.Storage;


namespace IceCreamProject.Controllers
{
    public class IceCreamFlavorsController : Controller
    {
        private readonly IceCreamFlavorsContext _context;

        public IceCreamFlavorsController(IceCreamFlavorsContext context)
        {
            _context = context;
        }

        // GET: IceCreamFlavors
        public async Task<IActionResult> Index()
        {
            return View(await _context.IceCreamFlavor.ToListAsync());
        }
        public async Task<IActionResult> IndexManager()
        {
            return View(await _context.IceCreamFlavor.ToListAsync());
        }

        // GET: IceCreamFlavors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iceCreamFlavor = await _context.IceCreamFlavor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iceCreamFlavor == null)
            {
                return NotFound();
            }

            return View(iceCreamFlavor);
           
        }

        // GET: IceCreamFlavors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IceCreamFlavors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,Description")] IceCreamFlavor iceCreamFlavor)
        {

            if (ModelState.IsValid)
            {
                ImaggaSampleClass image = new ImaggaSampleClass();
                var result = image.CheckImage(iceCreamFlavor.ImagePath);
                for(int i=0; i<10; i++)
                {
                    if(result[i]== "ice cream")
                    {
                        firebaseImgAsync(iceCreamFlavor.ImagePath, iceCreamFlavor.Name);
                        _context.Add(iceCreamFlavor);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(IndexManager));
                    }                       
                }              
            }
            return View(iceCreamFlavor);
        }

        // GET: IceCreamFlavors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iceCreamFlavor = await _context.IceCreamFlavor.FindAsync(id);
            if (iceCreamFlavor == null)
            {
                return NotFound();
            }
            return View(iceCreamFlavor);
        }

        // POST: IceCreamFlavors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImagePath,Description")] IceCreamFlavor iceCreamFlavor)
        {
            if (id != iceCreamFlavor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iceCreamFlavor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IceCreamFlavorExists(iceCreamFlavor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexManager));
            }
            return View(iceCreamFlavor);
        }

        // GET: IceCreamFlavors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iceCreamFlavor = await _context.IceCreamFlavor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iceCreamFlavor == null)
            {
                return NotFound();
            }

            return View(iceCreamFlavor);
        }

        // POST: IceCreamFlavors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iceCreamFlavor = await _context.IceCreamFlavor.FindAsync(id);
            _context.IceCreamFlavor.Remove(iceCreamFlavor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexManager));
        }

        private bool IceCreamFlavorExists(int id)
        {
            return _context.IceCreamFlavor.Any(e => e.Id == id);
        }

        public async void firebaseImgAsync(string webUrl, string name)
        {
            WebClient client = new WebClient();
            string path = @"C:\imges\" + name + ".jpg";
            client.DownloadFile(webUrl, path);//Download img to computer
            var stream = System.IO.File.Open(path, System.IO.FileMode.Open);
            var task = new FirebaseStorage("icecream-2dbae.appspot.com").Child(name +".jpg").PutAsync(stream);
            var url = await task;
        }

    }
}
