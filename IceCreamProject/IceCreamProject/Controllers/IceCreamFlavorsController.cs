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
using IceCreamProject.Data;
using System.Data.Entity;

namespace IceCreamProject.Controllers
{
    public class IceCreamFlavorsController : Controller
    {
       // private readonly IceCreamFlavorsContext _context;
        IceCreamContext context = new IceCreamContext();
        //public IceCreamFlavorsController(IceCreamFlavorsContext context)
        //{
        //    _context = context;
        //    //var context1 = new IceCreamContext();
        //    //foreach(var item in context.IceCreamFlavor)
        //    //{
        //    //    context1.IceCreamFlavors.Add(item);
        //    //    context1.SaveChanges();
        //    //}
        //}

        // GET: IceCreamFlavors
        public async Task<IActionResult> Index()
        {
            return View(await context.IceCreamFlavors.ToListAsync());

        }
        public async Task<IActionResult> IndexManager()
        {
            return View(await context.IceCreamFlavors.ToListAsync());
        }

        // GET: IceCreamFlavors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iceCreamFlavor = await context.IceCreamFlavors
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
                        context.IceCreamFlavors.Add(iceCreamFlavor);
                        await context.SaveChangesAsync();
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

            var iceCreamFlavor = await context.IceCreamFlavors.FindAsync(id);
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
                    //context.IceCreamFlavors.r()
                    context.Entry(iceCreamFlavor).State = System.Data.Entity.EntityState.Modified;
                    //context.IceCreamFlavors.Update(iceCreamFlavor);
                    await context.SaveChangesAsync();
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

            var iceCreamFlavor = await context.IceCreamFlavors
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
            var iceCreamFlavor = await context.IceCreamFlavors.FindAsync(id);
            context.IceCreamFlavors.Remove(iceCreamFlavor);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexManager));
        }

        private bool IceCreamFlavorExists(int id)
        {
            return context.IceCreamFlavors.Any(e => e.Id == id);
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
