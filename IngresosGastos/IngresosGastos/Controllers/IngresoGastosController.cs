﻿using System;
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
    public class IngresoGastosController : Controller
    {
        private readonly AppDBContext _context;

        public IngresoGastosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: IngresoGastos
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.IngresoGasto.Include(i => i.Categoria);
            return View(await appDBContext.ToListAsync());
        }

        // GET: IngresoGastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IngresoGasto == null)
            {
                return NotFound();
            }

            var ingresoGasto = await _context.IngresoGasto
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingresoGasto == null)
            {
                return NotFound();
            }

            return View(ingresoGasto);
        }

        // GET: IngresoGastos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria");
            return View();
        }

        // POST: IngresoGastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriaId,Fecha,Valor")] IngresoGasto ingresoGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingresoGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", ingresoGasto.CategoriaId);
            return View(ingresoGasto);
        }

        // GET: IngresoGastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IngresoGasto == null)
            {
                return NotFound();
            }

            var ingresoGasto = await _context.IngresoGasto.FindAsync(id);
            if (ingresoGasto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", ingresoGasto.CategoriaId);
            return View(ingresoGasto);
        }

        // POST: IngresoGastos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriaId,Fecha,Valor")] IngresoGasto ingresoGasto)
        {
            if (id != ingresoGasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingresoGasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresoGastoExists(ingresoGasto.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", ingresoGasto.CategoriaId);
            return View(ingresoGasto);
        }

        // GET: IngresoGastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IngresoGasto == null)
            {
                return NotFound();
            }

            var ingresoGasto = await _context.IngresoGasto
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingresoGasto == null)
            {
                return NotFound();
            }

            return View(ingresoGasto);
        }

        // POST: IngresoGastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IngresoGasto == null)
            {
                return Problem("Entity set 'AppDBContext.IngresoGasto'  is null.");
            }
            var ingresoGasto = await _context.IngresoGasto.FindAsync(id);
            if (ingresoGasto != null)
            {
                _context.IngresoGasto.Remove(ingresoGasto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngresoGastoExists(int id)
        {
          return _context.IngresoGasto.Any(e => e.Id == id);
        }
    }
}