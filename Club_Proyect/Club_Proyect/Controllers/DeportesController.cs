using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Club_Proyect.Data;
using Club_Proyect.Entities;

namespace Club_Proyect.Controllers
{
    public class DeportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deportes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deporte.ToListAsync());
        }

        // GET: Deportes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deporte = await _context.Deporte
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deporte == null)
            {
                return NotFound();
            }

            return View(deporte);
        }

        // GET: Deportes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deportes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Descripcion,Activo")] Deporte deporte)
        {
            if (ModelState.IsValid)
            {
                deporte.ID = Guid.NewGuid();
                _context.Add(deporte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deporte);
        }

        // GET: Deportes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deporte = await _context.Deporte.FindAsync(id);
            if (deporte == null)
            {
                return NotFound();
            }
            return View(deporte);
        }

        // POST: Deportes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Nombre,Descripcion,Activo")] Deporte deporte)
        {
            if (id != deporte.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeporteExists(deporte.ID))
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
            return View(deporte);
        }

        // GET: Deportes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deporte = await _context.Deporte
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deporte == null)
            {
                return NotFound();
            }

            return View(deporte);
        }

        // POST: Deportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deporte = await _context.Deporte.FindAsync(id);
            _context.Deporte.Remove(deporte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeporteExists(Guid id)
        {
            return _context.Deporte.Any(e => e.ID == id);
        }
    }
}
