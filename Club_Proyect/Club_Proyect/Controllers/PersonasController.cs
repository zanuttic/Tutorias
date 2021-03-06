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
    // [Authorize(Roles = "Admin")]
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personas
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Persona.Where(s => s.Activo == true).ToListAsync());
        //}
        public async Task<IActionResult> Index(string SortOrder, string currentFilter, string searchString, int? value)
        {
            //ViewData["NombreSortParm"] = String.IsNullOrEmpty(SortOrder) ? "nombre_desc" : "";
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

            var personaQuery = from j in _context.Persona select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                personaQuery = personaQuery.Where(j => j.Nombre.Contains(searchString) || j.Apellido.Contains(searchString)
                || j.Direccion.Contains(searchString)
                || j.DNI.ToString().Contains(searchString)
                || j.FechaNacimiento.ToString().Contains(searchString));

            }

            switch (SortOrder)
            {
                case "nombre_desc":
                    personaQuery = personaQuery.OrderByDescending(j => j.Nombre);
                    break;
                case "nombre_asc":
                    personaQuery = personaQuery.OrderBy(j => j.Nombre);
                    break;
                case "apellido_desc":
                    personaQuery = personaQuery.OrderByDescending(j => j.Apellido);
                    break;
                case "apellido_asc":
                    personaQuery = personaQuery.OrderBy(j => j.Apellido);
                    break;
                default:
                    personaQuery = personaQuery.OrderBy(j => j.Nombre);
                    break;


            }

            int pageSize = 10;
            return View(await Paginacion<Persona>.CreateAsync(personaQuery.AsNoTracking(), value ?? 1, pageSize));
            // return View(await juegos.AsNoTracking().ToListAsync());

            //return View(await _context.Juegos.ToListAsync());
        }

        //public async Task<IActionResult> Index(string SortOrder, string currentFilter, string searchString, int? page)
        //{
        //    ViewData["NombreSortParm"] = String.IsNullOrEmpty(SortOrder) ? "nombre_desc" : "";
        //    ViewData["CategoriaSortParam"] = SortOrder == "categoria_asc" ? "categoria_desc" : "categoria_asc";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }
        //    ViewData["CurrentFilter"] = searchString;
        //    ViewData["CurrentSort"] = SortOrder;

        //    var juegos = from j in _context.Juegos select j;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        juegos = juegos.Where(j => j.Nombre.Contains(searchString) || j.Categoria.Contains(searchString) || j.Descripcion.Contains(searchString));

        //    }

        //    switch (SortOrder)
        //    {
        //        case "nombre_desc":
        //            juegos = juegos.OrderByDescending(j => j.Nombre);
        //            break;
        //        case "categoria_desc":
        //            juegos = juegos.OrderByDescending(j => j.Categoria);
        //            break;
        //        case "categoria_asc":
        //            juegos = juegos.OrderBy(j => j.Categoria);
        //            break;
        //        default:
        //            juegos = juegos.OrderBy(j => j.Nombre);
        //            break;


        //    }

        //    int pageSize = 10;
        //    return View(await Paginacion<Juegos>.CreateAsync(juegos.AsNoTracking(), page ?? 1, pageSize));
        //    // return View(await juegos.AsNoTracking().ToListAsync());

        //    //return View(await _context.Juegos.ToListAsync());
        //}

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .FirstOrDefaultAsync(m => m.ID == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DNI,Nombre,Apellido,FechaNacimiento,Direccion,Activo")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persona.ID = Guid.NewGuid();
                persona.Activo = true;
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,DNI,Nombre,Apellido,FechaNacimiento,Direccion,Activo")] Persona persona)
        {
            if (id != persona.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.ID))
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
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .FirstOrDefaultAsync(m => m.ID == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var persona = await _context.Persona.FindAsync(id);
            var cliente = await _context.Cliente.Where(d => d.Activo_oNo == true && d.persona.ID == id).ToListAsync();
            if (cliente.Any())
            {
                persona.error = true;
                persona.mensaje.Add(string.Format("El cliente {0} esta asociado a esta persona. ", cliente[0].Num_Cliente));

            }
            var socio = await _context.Socio.Where(d => d.ActivoOno == true && d.persona.ID == id).ToListAsync();
            if (socio.Any())
            {
                persona.error = true;
                persona.mensaje.Add(string.Format("El socio {0} esta asociado a esta persona. ", socio[0].NumSocio));

            }
            var vecino = await _context.Vecino.Where(d => d.Activo == true && d.persona.ID == id).ToListAsync();
            if (vecino.Any())
            {
                persona.error = true;
                persona.mensaje.Add(string.Format("El vecino {0} esta asociado a esta persona. ", vecino[0].Nombre));

            }
            var empleado = await _context.Empleado.Where(d => d.Activo_oNo == true && d.persona.ID == id).ToListAsync();
            if (empleado.Any())
            {
                persona.error = true;
                persona.mensaje.Add(string.Format("El empleado {0} esta asociado a esta persona. ", empleado[0].Num_Legajo));

            }
            if (persona.error) return View(persona);
            persona.Activo = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(Guid id)
        {
            return _context.Persona.Any(e => e.ID == id);
        }
    }
}
