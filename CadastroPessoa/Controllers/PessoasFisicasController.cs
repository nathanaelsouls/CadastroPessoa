using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoa.Models;
using CadastroPessoa.Services;
using CadastroPessoa.Services.Exception;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPessoa.Controllers
{
    public class PessoasFisicasController : Controller
    {
        private readonly PessoaFisicaService _pessoaFisicaService;

        public PessoasFisicasController (PessoaFisicaService pessoaFisicaService)
        {
            _pessoaFisicaService = pessoaFisicaService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _pessoaFisicaService.BuscarTodasPessoasFisicasAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado" });
            }

            var pessoa = await _pessoaFisicaService.BuscarIdAsync(id.Value);
            if (pessoa == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(pessoa);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PessoaFisica pessoaFisica)
        {
            if (!ModelState.IsValid)
            {
                var pessoaCreate = await _pessoaFisicaService.BuscarTodasPessoasFisicasAsync();
                return View(pessoaCreate);
            }
            await _pessoaFisicaService.InserirPessoaFisicaAsync(pessoaFisica);
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado" });
            }

            var pessoa = await _pessoaFisicaService.BuscarIdAsync(id.Value);
            if (pessoa == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PessoaFisica pessoa)
        {
            if (id != pessoa.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id incompatível" });
            }

            if (!ModelState.IsValid)
            {
                var pessoaEdit = await _pessoaFisicaService.BuscarTodasPessoasFisicasAsync();
                return View(pessoaEdit);
            }
            try
            {
                await _pessoaFisicaService.AtualizarPessoaFisicaAsync(pessoa);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não informado" });
            }

            var obj = await _pessoaFisicaService.BuscarIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _pessoaFisicaService.RemoverPessoaFisicaAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
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
