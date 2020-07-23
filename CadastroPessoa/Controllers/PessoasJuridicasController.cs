using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoa.Services;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPessoa.Controllers
{
    public class PessoasJuridicasController : Controller
    {
        private readonly PessoaJuridicaService _pessoaJuridicaService;
        
        public PessoasJuridicasController(PessoaJuridicaService pessoaJuridicaService)
        {
            _pessoaJuridicaService = pessoaJuridicaService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pessoaJuridicaService.BuscarTodasPessoasJuridicasAsync());
        }
    }
}
