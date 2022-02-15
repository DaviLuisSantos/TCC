using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TCC.Data;
using TCC.Models;

namespace TCC.Controllers
{
    [Authorize]
    public class ApartamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Apartamento
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apartamentos
            .AsNoTracking()
            .Where(x => x.User == User.Identity.Name)
            .ToListAsync());
        }

        // GET: Apartamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _context.Apartamentos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartamento == null)
            {
                return NotFound();
            }

            if (apartamento.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(apartamento);
        }

        // GET: Apartamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Proprietario,qntQuartos,User")] Apartamento apartamento)
        {
            if (ModelState.IsValid)
            {
                apartamento.User = User.Identity.Name;
                _context.Add(apartamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartamento);
        }

        // GET: Apartamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _context.Apartamentos.FindAsync(id);
            if (apartamento == null)
            {
                return NotFound();
            }

            if (apartamento.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(apartamento);
        }

        // POST: Apartamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Proprietario,qntQuartos")] Apartamento apartamento)
        {
            if (id != apartamento.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    apartamento.User = User.Identity.Name;
                    _context.Update(apartamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartamentoExists(apartamento.ID))
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
            return View(apartamento);
        }

        // GET: Apartamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamento = await _context.Apartamentos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (apartamento == null)
            {
                return NotFound();
            }
            if (apartamento.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(apartamento);
        }

        // POST: Apartamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartamento = await _context.Apartamentos.FindAsync(id);
            _context.Apartamentos.Remove(apartamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartamentoExists(int id)
        {
            return _context.Apartamentos.Any(e => e.ID == id);
        }
    }
}
