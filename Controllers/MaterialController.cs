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
    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaterialController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context?.Materials?.Include(m => m.Supplier);
            return View(await applicationDbContext?.ToListAsync()!);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
                .Include(m => m.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        public IActionResult Create()
        {
            ViewData["IdSupplier"] = new SelectList(_context.Supplier, "Id", "Name");
            return View();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdSupplier,Code,Name,Description,FiscalCode,Specie,CreatedBy")] Material material)
        {
            material.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } else {
                foreach(var i in ModelState.Values) {
                    foreach (var error in i.Errors)
                    {
                        System.Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            ViewData["IdSupplier"] = new SelectList(_context.Supplier, "Id", "Name", material.IdSupplier);
            return View(material);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }

            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            ViewData["IdSupplier"] = new SelectList(_context.Supplier, "Id", "Name", material.IdSupplier);
            return View(material);
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdSupplier,Code,Name,Description,FiscalCode,Specie,CreatedAt,CreatedBy,UpdatedBy")] Material material)
        {
            material.UpdatedAt = DateTime.Now;
            if (id != material.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.Id))
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
            ViewData["IdSupplier"] = new SelectList(_context.Supplier, "Id", "Name", material.IdSupplier);
            return View(material);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Materials == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
                .Include(m => m.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Materials == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Materials'  is null.");
            }
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                _context.Materials.Remove(material);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialExists(int id)
        {
          return (_context.Materials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
