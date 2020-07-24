using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroPessoa.Data;
using CadastroPessoa.Models;

namespace CadastroPessoa.Controllers
{
    public class PessoaJuridicasController : Controller
    {
        private readonly Context _context;

        public PessoaJuridicasController(Context context)
        {
            _context = context;
        }

        // GET: PessoaJuridicas
        public async Task<IActionResult> Index()
        {
            var context = _context.PessoaJuridicas.Include(p => p.Empresa);
            return View(await context.ToListAsync());
        }

        // GET: PessoaJuridicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaJuridica = await _context.PessoaJuridicas
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            return View(pessoaJuridica);
        }

        // GET: PessoaJuridicas/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "NomeFantasia");
            return View();
        }

        // POST: PessoaJuridicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DataNascimento,EmpresaId")] PessoaJuridica pessoaJuridica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoaJuridica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "NomeFantasia", pessoaJuridica.EmpresaId);
            return View(pessoaJuridica);
        }

        // GET: PessoaJuridicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaJuridica = await _context.PessoaJuridicas.FindAsync(id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "NomeFantasia", pessoaJuridica.EmpresaId);
            return View(pessoaJuridica);
        }

        // POST: PessoaJuridicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DataNascimento,EmpresaId")] PessoaJuridica pessoaJuridica)
        {
            if (id != pessoaJuridica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoaJuridica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaJuridicaExists(pessoaJuridica.Id))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "NomeFantasia", pessoaJuridica.EmpresaId);
            return View(pessoaJuridica);
        }

        // GET: PessoaJuridicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaJuridica = await _context.PessoaJuridicas
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            return View(pessoaJuridica);
        }

        // POST: PessoaJuridicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaJuridica = await _context.PessoaJuridicas.FindAsync(id);
            _context.PessoaJuridicas.Remove(pessoaJuridica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaJuridicaExists(int id)
        {
            return _context.PessoaJuridicas.Any(e => e.Id == id);
        }
    }
}
