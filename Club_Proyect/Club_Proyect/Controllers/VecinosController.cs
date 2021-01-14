using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Club_Proyect.Data;
using Club_Proyect.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Club_Proyect.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class VecinosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VecinosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vecinos
        public async Task<IActionResult> Index(string SortOrder, string currentFilter, string searchString, int? value)
        {
            //return View(await _context.Vecino.Where(s => s.Activo == true).Include(p => p.persona).ToListAsync());
            ViewData["NombreSortParm"] = SortOrder == "nombre_asc" ? "nombre_desc" : "nombre_asc";
            ViewData["ApellidoSortParam"] = SortOrder == "apellido_asc" ? "apellido_desc" : "apellido_asc";

            if (searchString != null)
            {
                value = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = SortOrder;

            var Vecinoquery = from j in _context.Vecino.Include(j => j.persona) where j.Activo == true select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                Vecinoquery = Vecinoquery.Where(j => j.persona.Nombre.Contains(searchString) || j.persona.Apellido.Contains(searchString)
                || j.persona.Direccion.Contains(searchString)
                || j.persona.DNI.ToString().Contains(searchString)
                || j.persona.FechaNacimiento.ToString().Contains(searchString));
            }

            switch (SortOrder)
            {
                case "nombre_desc":
                    Vecinoquery = Vecinoquery.OrderByDescending(j => j.persona.Nombre);
                    break;
                case "nombre_asc":
                    Vecinoquery = Vecinoquery.OrderBy(j => j.persona.Nombre);
                    break;
                case "apellido_desc":
                    Vecinoquery = Vecinoquery.OrderByDescending(j => j.persona.Apellido);
                    break;
                case "apellido_asc":
                    Vecinoquery = Vecinoquery.OrderBy(j => j.persona.Apellido);
                    break;
                default:
                    Vecinoquery = Vecinoquery.OrderBy(j => j.persona.Nombre);
                    break;


            }

            int pageSize = 10;
            var cli = Vecinoquery.AsNoTracking();
            return View(await Paginacion<Vecino>.CreateAsync(Vecinoquery.AsNoTracking(), value ?? 1, pageSize));

        }
        // GET: Vecinos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vecino = await _context.Vecino.Include(x => x.persona)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (vecino == null)
            {
                return NotFound();
            }

            return View(vecino);
        }

        // GET: Vecinos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vecinos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Nombre,telefono,persona")] Vecino vecino)
        {
            if (ModelState.IsValid)
            {
                var personaencontrada = _context.Persona.FirstOrDefault(p => p.DNI == vecino.persona.DNI);
                vecino.persona = personaencontrada;
                vecino.Activo = true;
                _context.Add(vecino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vecino);
        }

        // GET: Vecinos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vecino = await _context.Vecino.Include(x => x.persona)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (vecino == null)
            {
                return NotFound();
            }
            return View(vecino);
        }

        // POST: Vecinos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,telefono, persona")] Vecino vecino)
        {
            if (id != vecino.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vecino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VecinoExists(vecino.Id))
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
            return View(vecino);
        }

        // GET: Vecinos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vecino = await _context.Vecino.Include(x => x.persona)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (vecino == null)
            {
                return NotFound();
            }

            return View(vecino);
        }

        // POST: Vecinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vecino = await _context.Vecino.FindAsync(id);
            vecino.Activo = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VecinoExists(int id)
        {
            return _context.Vecino.Any(e => e.Id == id);
        }
    }
}
