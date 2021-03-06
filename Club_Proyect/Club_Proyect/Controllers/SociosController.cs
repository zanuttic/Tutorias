﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Club_Proyect.Data;
using Club_Proyect.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Club_Proyect.Controllers
{
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SociosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Socios
        public async Task<IActionResult> Index(string SortOrder, string currentFilter, string searchString, int? value)
        {
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

            var Socioquery = from j in _context.Socio.Include(j => j.persona) where j.ActivoOno == true select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                Socioquery = Socioquery.Where(j => j.persona.Nombre.Contains(searchString) || j.persona.Apellido.Contains(searchString)
                || j.persona.Direccion.Contains(searchString)
                || j.persona.DNI.ToString().Contains(searchString)
                || j.persona.FechaNacimiento.ToString().Contains(searchString));
            }

            switch (SortOrder)
            {
                case "nombre_desc":
                    Socioquery = Socioquery.OrderByDescending(j => j.persona.Nombre);
                    break;
                case "nombre_asc":
                    Socioquery = Socioquery.OrderBy(j => j.persona.Nombre);
                    break;
                case "apellido_desc":
                    Socioquery = Socioquery.OrderByDescending(j => j.persona.Apellido);
                    break;
                case "apellido_asc":
                    Socioquery = Socioquery.OrderBy(j => j.persona.Apellido);
                    break;
                default:
                    Socioquery = Socioquery.OrderBy(j => j.persona.Nombre);
                    break;


            }

            int pageSize = 10;
            var soc = Socioquery.AsNoTracking();
            return View(await Paginacion<Socio>.CreateAsync(Socioquery.AsNoTracking(), value ?? 1, pageSize));
        }

        // GET: Socios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socio.Include(x => x.persona)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // GET: Socios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Socios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([Bind("ID,NumSocio,FechaIngresoClub,Categoria,ActivoOno, persona")] Socio socio)
        {
            if (ModelState.IsValid)
            {
                socio.ID = Guid.NewGuid();
                var personaEncontrada = await _context.Persona.FirstOrDefaultAsync(x => x.DNI == socio.persona.DNI);
                socio.persona = personaEncontrada;
                socio.ActivoOno = true;
                _context.Socio.Add(socio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(socio);
        }

        // GET: Socios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socio.Include(x => x.persona).FirstOrDefaultAsync(e => e.ID == id);
            if (socio == null)
            {
                return NotFound();
            }
            return View(socio);
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,NumSocio,FechaIngresoClub,Categoria,ActivoOno, persona")] Socio socio)
        {
            if (id != socio.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocioExists(socio.ID))
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
            return View(socio);
        }

        // GET: Socios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socio.Include(x => x.persona).FirstOrDefaultAsync(e => e.ID == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var socio = await _context.Socio.FindAsync(id);
            socio.ActivoOno = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocioExists(Guid id)
        {
            return _context.Socio.Any(e => e.ID == id);
        }
    }
}
