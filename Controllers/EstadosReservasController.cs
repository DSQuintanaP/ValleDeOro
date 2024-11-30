using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValleDeOro.Models;

namespace ValleDeOro.Controllers
{
    public class EstadosReservasController : Controller
    {
        private readonly GvglampingContext _context;

        public EstadosReservasController(GvglampingContext context)
        {
            _context = context;
        }

        // GET: EstadosReservas
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadosReservas.ToListAsync());
        }

        // GET: EstadosReservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosReserva = await _context.EstadosReservas
                .FirstOrDefaultAsync(m => m.IdEstadoReserva == id);
            if (estadosReserva == null)
            {
                return NotFound();
            }

            return View(estadosReserva);
        }

        // GET: EstadosReservas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadosReservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstadoReserva,NombreEstadoReserva")] EstadosReserva estadosReserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadosReserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadosReserva);
        }

        // GET: EstadosReservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosReserva = await _context.EstadosReservas.FindAsync(id);
            if (estadosReserva == null)
            {
                return NotFound();
            }
            return View(estadosReserva);
        }

        // POST: EstadosReservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstadoReserva,NombreEstadoReserva")] EstadosReserva estadosReserva)
        {
            if (id != estadosReserva.IdEstadoReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadosReserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosReservaExists(estadosReserva.IdEstadoReserva))
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
            return View(estadosReserva);
        }

        // GET: EstadosReservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosReserva = await _context.EstadosReservas
                .FirstOrDefaultAsync(m => m.IdEstadoReserva == id);
            if (estadosReserva == null)
            {
                return NotFound();
            }

            return View(estadosReserva);
        }

        // POST: EstadosReservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadosReserva = await _context.EstadosReservas.FindAsync(id);
            if (estadosReserva != null)
            {
                _context.EstadosReservas.Remove(estadosReserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosReservaExists(int id)
        {
            return _context.EstadosReservas.Any(e => e.IdEstadoReserva == id);
        }
    }
}
