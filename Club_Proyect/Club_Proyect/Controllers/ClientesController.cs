using System;
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
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        //  public async Task<IActionResult> Index()
        // {
        //  var listaCliente = _context.Cliente.Where(s=>s.Activo_oNo==true).Include(x => x.persona).ToList(); //Include incluye los datos de la entidad que se relacione asociado a ese cliente
        //
        //    return View(await _context.Cliente.ToListAsync());
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

            var Clientequery = from j in  _context.Cliente.Include(j =>j.persona) where j.Activo_oNo == true select j; 

            if (!String.IsNullOrEmpty(searchString))
            {
                Clientequery = Clientequery.Where(j => j.persona.Nombre.Contains(searchString) || j.persona.Apellido.Contains(searchString)
                || j.persona.Direccion.Contains(searchString)
                || j.persona.DNI.ToString().Contains(searchString)
                || j.persona.FechaNacimiento.ToString().Contains(searchString));

            }

            switch (SortOrder)
            {
                case "nombre_desc":
                    Clientequery = Clientequery.OrderByDescending(j => j.persona.Nombre);
                    break;
                case "nombre_asc":
                    Clientequery = Clientequery.OrderBy(j => j.persona.Nombre);
                    break;
                case "apellido_desc":
                    Clientequery = Clientequery.OrderByDescending(j => j.persona.Apellido);
                    break;
                case "apellido_asc":
                    Clientequery = Clientequery.OrderBy(j => j.persona.Apellido);
                    break;
                default:
                    Clientequery = Clientequery.OrderBy(j => j.persona.Nombre);
                    break;


            }

            int pageSize = 10;
            var cli = Clientequery.AsNoTracking();
            return View(await Paginacion<Cliente>.CreateAsync(Clientequery.AsNoTracking(), value ?? 1, pageSize));
            // return View(await juegos.AsNoTracking().ToListAsync());

            //return View(await _context.Juegos.ToListAsync());
        }
        
        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.Include(x => x.persona)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Num_Cliente,Saldo,Activo_oNo, persona")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
               
                var persona = _context.Persona.FirstOrDefault(x => x.DNI == cliente.persona.DNI);
                if (persona == null)
                {
                    cliente.error = true;
                    cliente.mensaje = string.Format("la persona con dni:{0} no existe", cliente.persona.DNI);
                    return View(cliente);
                }
                var personaid = persona.ID;
                var clientetemp = _context.Cliente.FirstOrDefault(x => x.persona.ID == personaid);
                if (clientetemp != null)
                {
                    cliente.error = true;
                    cliente.mensaje = string.Format("la persona con dni:{0} ya existe como cliente numero {1} ", cliente.persona.DNI,clientetemp.Num_Cliente);
                    return View(cliente);
                }

                cliente.Activo_oNo = true;
                cliente.persona.Nombre = persona.Nombre;
                cliente.persona.Apellido = persona.Apellido;
                cliente.persona.Direccion = persona.Direccion;
                cliente.persona.FechaNacimiento = persona.FechaNacimiento;
                cliente.persona.ID = persona.ID;
                cliente.persona.Activo = persona.Activo;

                cliente.ID = Guid.NewGuid();
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.Include(x => x.persona)
               .FirstOrDefaultAsync(m => m.ID == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Num_Cliente,Saldo,Activo_oNo, persona")] Cliente cliente)
        {
            if (id != cliente.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ID))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            cliente.Activo_oNo = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            return _context.Cliente.Any(e => e.ID == id);
        }
    }
}
