using CadastroPessoa.Data;
using CadastroPessoa.Models;
using CadastroPessoa.Services.Exception;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoa.Services
{
    public class PessoaFisicaService
    {
        private readonly Context _context;

        public PessoaFisicaService(Context context)
        {
            _context = context;
        }

        public async Task<List<PessoaFisica>> BuscarTodasPessoasFisicasAsync()
        {
            return await _context.PessoaFisicas.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<PessoaFisica> BuscarIdAsync(int id)
        {
            return await _context.PessoaFisicas.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task InserirPessoaFisicaAsync(PessoaFisica obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverPessoaFisicaAsync(int id)
        {
            try
            {
                var obj = await _context.PessoaFisicas.FindAsync(id);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível excluir pessoa");
            }
        }

        public async Task AtualizarPessoaFisicaAsync(PessoaFisica obj)
        {
            bool idIgual = await _context.PessoaFisicas.AnyAsync(x => x.Id == obj.Id);
            if (!idIgual)
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
