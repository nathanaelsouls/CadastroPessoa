using CadastroPessoa.Data;
using CadastroPessoa.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoa.Services
{
    public class PessoaJuridicaService
    {
        private readonly Context _context;

        public PessoaJuridicaService(Context context)
        {
            _context = context;
        }

        public async Task<List<PessoaJuridica>> BuscarTodasPessoasJuridicasAsync()
        {
            return await _context.PessoaJuridicas.Include(e => e.Empresa).OrderBy(p => p.Name).ToListAsync();
        }

    }
}
