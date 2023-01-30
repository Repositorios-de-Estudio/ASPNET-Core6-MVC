using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IngresosGastos.Data;
using IngresosGastos.Models;

namespace IngresosGastos.Controllers
{
    public class CategoriaTipoesController : Controller
    {
        private readonly AppDBContext _context;


        public CategoriaTipoesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: CategoriaTipoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriaTipo.ToListAsync());
        }

        // GET: CategoriaTipoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoriaTipo == null)
            {
                return NotFound();
            }

            var categoriaTipo = await _context.CategoriaTipo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaTipo == null)
            {
                return NotFound();
            }

            return View(categoriaTipo);
        }

        // GET: CategoriaTipoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaTipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo")] CategoriaTipo categoriaTipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaTipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaTipo);
        }

        // GET: CategoriaTipoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoriaTipo == null)
            {
                return NotFound();
            }

            var categoriaTipo = await _context.CategoriaTipo.FindAsync(id);
            if (categoriaTipo == null)
            {
                return NotFound();
            }
            return View(categoriaTipo);
        }

        // POST: CategoriaTipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo")] CategoriaTipo categoriaTipo)
        {
            if (id != categoriaTipo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaTipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaTipoExists(categoriaTipo.Id))
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
            return View(categoriaTipo);
        }

        // GET: CategoriaTipoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoriaTipo == null)
            {
                return NotFound();
            }

            var categoriaTipo = await _context.CategoriaTipo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaTipo == null)
            {
                return NotFound();
            }

            return View(categoriaTipo);
        }

        // POST: CategoriaTipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoriaTipo == null)
            {
                return Problem("Entity set 'AppDBContext.CategoriaTipo'  is null.");
            }
            var categoriaTipo = await _context.CategoriaTipo.FindAsync(id);
            if (categoriaTipo != null)
            {
                _context.CategoriaTipo.Remove(categoriaTipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaTipoExists(int id)
        {
          return _context.CategoriaTipo.Any(e => e.Id == id);
        }
    }
}
