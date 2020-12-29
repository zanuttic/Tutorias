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
    public class VentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            
            return View( _context.Venta.Where(x => x.ActivooNo == true).Include(x => x.productos).ToList());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta.Include(x => x.productos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        ////GET: Ventas/Create
        //public IActionResult Create()
        //{
        //    var listaProducto = _context.Productos.ToList();
        //    ViewBag.productos = listaProducto;
        //    return View();
        //}

        public IActionResult Create([Bind("ID,Horario, cliente")] Bufet bufet)
        {
            var listaProducto = _context.Productos.ToList();
            ViewBag.productos = listaProducto;

            if (bufet.cliente is null)
            {
                bufet = _context.Bufet.Include("cliente").FirstOrDefault(x => x.ID == bufet.ID);
            }
            ViewData["bufet"] = bufet;
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Fecha,metodoPago,Renta,totalVenta,Prod, ID_Bufet")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                //var bufet = _context.Bufet.FirstOrDefault(x => x.ID == venta.ID_Bufet);
                var BufetNuevo = new Entities.Bufet();
                var prodid = new Guid(venta.Prod);
                var producto = _context.Productos.FirstOrDefault(p=>p.ID==prodid);
                venta.ID = Guid.NewGuid();
                venta.productos = producto;
                BufetNuevo.venta = venta;
                _context.Add(BufetNuevo);
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta.Include(x => x.productos).FirstOrDefaultAsync(x => x.ID == id);
            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Fecha,metodoPago,Renta,totalVenta, productos")] Venta venta)
        {
            if (id != venta.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //recuperar productos de bd productos id
                    var p = _context.Productos.FirstOrDefault(x => x.ID == venta.productos.ID);

                    //pisar el objeto producto dentro de obj venta
                    p.Nombre = venta.productos.Nombre;
                    p.Costo = venta.productos.Costo;
                    p.Ganancia = venta.productos.Ganancia;
                    p.Impuesto = venta.productos.Impuesto;
                    p.Stock = venta.productos.Stock;

                    var v = _context.Venta.FirstOrDefault(x => x.ID == venta.productos.ID);

                    p.Nombre = venta.productos.Nombre;
                    p.Costo = venta.productos.Costo;
                    p.Ganancia = venta.productos.Ganancia;
                    p.Impuesto = venta.productos.Impuesto;
                    p.Stock = venta.productos.Stock;

                    //_context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.ID))
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
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta
                .FirstOrDefaultAsync(m => m.ID == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var venta = await _context.Venta.FindAsync(id);
            venta.ActivooNo = false; //baja logica
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(Guid id)
        {
            return _context.Venta.Any(e => e.ID == id);
        }
    }
}
