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
    public class BufetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BufetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bufets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bufet.Include(x => x.venta).ToListAsync());
        }

        // GET: Bufets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bufet = await _context.Bufet
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bufet == null)
            {
                return NotFound();
            }

            return View(bufet);
        }

        // GET: Bufets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bufets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Horario, cliente")] Bufet bufet)
        {
            if (ModelState.IsValid)
            {
                var num_client = _context.Cliente.FirstOrDefault(x => x.Num_Cliente == bufet.cliente.Num_Cliente);
                if (num_client == null)
                {
                    return NotFound();
                }
                bufet.cliente = _context.Cliente.FirstOrDefault(x => x.Num_Cliente == bufet.cliente.Num_Cliente);
                bufet.ID = Guid.NewGuid();
                _context.Add(bufet);
                await _context.SaveChangesAsync();
                //return View("Ventas");
                return RedirectToAction("Create", "Ventas", bufet);
            }
            return View(bufet);
        }

        // GET: Bufets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bufet = await _context.Bufet.FindAsync(id);
            if (bufet == null)
            {
                return NotFound();
            }
            return View(bufet);
        }

        // POST: Bufets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Horario")] Bufet bufet)
        {
            if (id != bufet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bufet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BufetExists(bufet.ID))
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
            return View(bufet);
        }

        // GET: Bufets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bufet = await _context.Bufet
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bufet == null)
            {
                return NotFound();
            }

            return View(bufet);
        }

        // POST: Bufets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bufet = await _context.Bufet.FindAsync(id);
            _context.Bufet.Remove(bufet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BufetExists(Guid id)
        {
            return _context.Bufet.Any(e => e.ID == id);
        }
    }
}
