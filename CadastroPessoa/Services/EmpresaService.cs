using CadastroPessoa.Data;
using CadastroPessoa.Models;
using CadastroPessoa.Services.Exception;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoa.Services
{
    public class EmpresaService
    {
        private readonly Context _context;

        public EmpresaService(Context context)
        {
            _context = context;
        }

        public async Task<List<Empresa>> BuscarTodasEmpresasAsync()
        {
            return await _context.Empresas.OrderBy(x => x.NomeFantasia).ToListAsync();
        }

        public async Task<Empresa> BuscarIdAsync(int id)
        {
            return await _context.Empresas.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task InserirEmpresaAsync(Empresa obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverEmpresa(int id)
        {
            try
            {
                var obj = await _context.Empresas.FindAsync(id);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não é possível excluir empresa");
            }
        }

        public async Task AtualizarEmpresaAsync(Empresa obj)
        {
            bool idIgual = await _context.Empresas.AnyAsync(x => x.Id == obj.Id);
            if (!idIgual)
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
