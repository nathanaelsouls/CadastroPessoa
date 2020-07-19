using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroPessoa.Data;
using CadastroPessoa.Models;
using CadastroPessoa.Services;
using System.Diagnostics;
using CadastroPessoa.Services.Exception;

namespace CadastroPessoa.Controllers
{
    public class EmpresasController : Controller
    {
        //private readonly Context _context;
        private readonly EmpresaService _empresaService;

        public EmpresasController(Context context, EmpresaService empresaService)
        {
            //_context = context;
            _empresaService = empresaService;
        }

        // GET: Empresas
        public IActionResult Index()
        {
            return View(_empresaService.BuscarTodasEmpresasAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {            
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado" });
            }

            var empresa = await _empresaService.BuscarIdAsync(id.Value);
            if (empresa == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                var empresaCreate = await _empresaService.BuscarTodasEmpresasAsync();
                return View(empresaCreate);
            }
            await _empresaService.InserirEmpresaAsync(empresa);
            return RedirectToAction(nameof(Index));
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado" });
            }

            var empresa = await _empresaService.BuscarIdAsync(id.Value);
            if (empresa == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível" });
            }

            if (!ModelState.IsValid)
            {
                var empresaEdit = await _empresaService.BuscarTodasEmpresasAsync();
                return View(empresaEdit);
            }
            try
            {
                await _empresaService.AtualizarEmpresaAsync(empresa);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado" });
            }

            var empresa = await _empresaService.BuscarIdAsync(id.Value);
            if (empresa == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _empresaService.RemoverEmpresa(id);
                return RedirectToAction(nameof(Index));
            } 
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
