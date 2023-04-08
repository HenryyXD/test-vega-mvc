using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestVegaMVC;
using TestVegaMVC.Models;

namespace TestVegaMVC.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Supplier != null ? 
                          View(await _context.Supplier.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Supplier'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Supplier == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        public async Task<IActionResult> JsonById(int? id)
        {
            if (id == null || _context.Supplier == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Json(supplier);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Email,CNPJ,CEP,Address,CreatedBy")] Supplier supplier)
        {
            supplier.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
            return View(supplier);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Supplier == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Email,CNPJ,CEP,Address,CreatedAt,CreatedBy,UpdatedBy")] Supplier supplier)
        {
            supplier.UpdatedAt = DateTime.Now;
            if (id != supplier.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            } else {
                foreach(var i in ModelState.Values) {
                    foreach (var error in i.Errors)
                    {
                        System.Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(supplier);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Supplier == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Supplier == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Supplier'  is null.");
            }
            var supplier = await _context.Supplier.FindAsync(id);
            if (supplier != null)
            {
                _context.Supplier.Remove(supplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
          return (_context.Supplier?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
