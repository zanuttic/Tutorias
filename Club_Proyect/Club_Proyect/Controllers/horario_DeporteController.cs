using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Club_Proyect.Data;
using Club_Proyect.Entities;
using Club_Proyect.Entity;

namespace Club_Proyect.Controllers
{
    public class horario_DeporteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public horario_DeporteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: horario_Deporte
        public async Task<IActionResult> Index()
        {
            return View(await _context.horario_Deporte.Include(x => x.deporte).ToListAsync());
        }

        // GET: horario_Deporte/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario_Deporte = await _context.horario_Deporte
                .Include(x => x.deporte)
                .Include(s=>s.socios)
                .Include("socios.persona")
                .FirstOrDefaultAsync(m => m.ID == id);

            if (horario_Deporte == null)
            {
                return NotFound();
            }

            return View(horario_Deporte);
        }

        // GET: horario_Deporte/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: horario_Deporte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Descripcion,cantidad_Socios,Activo, deporte")] horario_Deporte horario_Deporte)
        {
            if (ModelState.IsValid)
            {
                //horario_Deporte.Nombre = Guid.NewGuid();
                var deporteEncontrado = await _context.Deporte.FirstOrDefaultAsync(x => x.Nombre == horario_Deporte.deporte.Nombre);
                horario_Deporte.deporte = deporteEncontrado;
                _context.Add(horario_Deporte);
                var socio1 = new Socio()
                {
                    ID = new Guid("00932533-6F0D-440B-B7BF-80935DF4DFA7"),
                    persona = new Persona() { ID = new Guid("2CB80394-C729-4CD2-A8CF-073B01298226") },
                    NumSocio = 1,
                    FechaIngresoClub = DateTime.Now,
                    Categoria = 1,
                    ActivoOno = true,

                };
                var socio2 = new Socio()
                {
                    ID = new Guid("5734224B-C544-43E7-BB54-B3D0D84B35FC"),
                    persona = new Persona() { ID = new Guid("9F3A2C7E-2CB7-45F7-B7F4-55BA40409FF6") },
                    NumSocio = 5,
                    FechaIngresoClub = DateTime.Now,
                    Categoria = 2,
                    ActivoOno = true,

                };
                horario_Deporte.socios.Add(socio1);
                horario_Deporte.socios.Add(socio2);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(horario_Deporte);
        }

        // GET: horario_Deporte/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario_Deporte = await _context.horario_Deporte.Include(x => x.deporte)
               .FirstOrDefaultAsync(m => m.ID == id);
            if (horario_Deporte == null)
            {
                return NotFound();
            }
            return View(horario_Deporte);
        }

        // POST: horario_Deporte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Nombre,Descripcion,cantidad_Socios,Activo, deporte, socio")] horario_Deporte horario_Deporte)
        {
            if (id != horario_Deporte.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var socioNuevo = _context.Socio.FirstOrDefault(x => x.NumSocio == horario_Deporte.socio.NumSocio);
                    var cantSocio = horario_Deporte.cantidad_Socios;
                    if (cantSocio <= horario_Deporte.socios.Count)
                    {
                        return NotFound();
                    }
                    horario_Deporte.socios.Add(socioNuevo);
                    _context.Update(horario_Deporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!horario_DeporteExists(horario_Deporte.ID))
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
            return View(horario_Deporte);
        }

        // GET: horario_Deporte/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario_Deporte = await _context.horario_Deporte
                .FirstOrDefaultAsync(m => m.ID == id);
            if (horario_Deporte == null)
            {
                return NotFound();
            }

            return View(horario_Deporte);
        }

        // POST: horario_Deporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var horario_Deporte = await _context.horario_Deporte.FindAsync(id);
            _context.horario_Deporte.Remove(horario_Deporte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool horario_DeporteExists(Guid id)
        {
            return _context.horario_Deporte.Any(e => e.ID == id);
        }
    }
}
