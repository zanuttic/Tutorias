﻿using System;
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
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Productos.ToListAsync());
        //}

        public async Task<IActionResult> Index(string SortOrder, string currentFilter, string searchString, int? value)
        {
            ViewData["NombreSortParm"] = String.IsNullOrEmpty(SortOrder) ? "nombre_desc" : "";
            ViewData["CategoriaSortParam"] = SortOrder == "categoria_asc" ? "categoria_desc" : "categoria_asc";

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

            var productosQuery = from j in _context.Productos select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                productosQuery = productosQuery.Where(j => j.Nombre.Contains(searchString) || j.Impuesto.ToString().Contains(searchString) );

            }

            switch (SortOrder)
            {
                case "nombre_desc":
                    productosQuery = productosQuery.OrderByDescending(j => j.Nombre);
                    break;
                default:
                    productosQuery = productosQuery.OrderBy(j => j.Nombre);
                    break;


            }

            int pageSize = 10;
            return View(await Paginacion<Productos>.CreateAsync(productosQuery.AsNoTracking(), value ?? 1, pageSize));
            // return View(await juegos.AsNoTracking().ToListAsync());

            //return View(await _context.Juegos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Stock,Costo,Impuesto,Ganancia")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                productos.ID = Guid.NewGuid();
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Nombre,Stock,Costo,Impuesto,Ganancia")] Productos productos)
        {
            if (id != productos.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.ID))
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
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productos = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(Guid id)
        {
            return _context.Productos.Any(e => e.ID == id);
        }
    }
}
