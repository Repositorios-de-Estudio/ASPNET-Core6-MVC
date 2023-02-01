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
    public class IngresoGastosController : Controller
    {
        private readonly AppDBContext _context;

        public IngresoGastosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: IngresoGastos
        // Index(int? mes, int? anio) para mostrar por mes y año segun lo seleccionado en la vista
        //public async Task<IActionResult> Index()
        public async Task<IActionResult> Index(int? mesV, int? anioV)
        {
            //POR DEFECTO SE CARGAN DATOS NULL LO QUE MUESTRA LA VISTA CON REGISTROS DE ESTE MES Y AÑO
            ViewData["mesV"] = mesV;
            ViewData["anioV"] = anioV;

            //Como los parametros son opcionales pueden ser null,
            // en caso de que lo sean se les asiga una valor
            if (mesV == null || anioV==null)
            {
                //mesV = DateTime.Now.Month;
                //anioV = DateTime.Now.Year;
                var appDBContextDefault = _context.IngresoGasto.Include(i => i.Categoria);
                return View(await appDBContextDefault.ToListAsync());
            }
            else
            {
                var appDBContextBusqueda = _context.IngresoGasto.Include(i => i.Categoria).Where(i => i.Fecha.Month == mesV && i.Fecha.Year == anioV);
                return View(await appDBContextBusqueda.ToListAsync());
            }

            //se crear variables mes y anio para ser usador en la vista con ViewBag
            

            //var appDBContext = _context.IngresoGasto.Include(i => i.Categoria);
            // se agrega where para selec where mes y anio
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
            //filtrar solo por categorias Activas
            ViewData["CategoriaId"] = new SelectList(_context.Categorias.Where(i => i.Estado==true), "Id", "NombreCategoria");
            //ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria");
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
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
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
