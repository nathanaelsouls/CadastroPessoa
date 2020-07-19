using CadastroPessoa.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoa.Data
{
    public class Context : DbContext
    {
        public DbSet<Empresa> Empresas{ get; set;}
        public DbSet<PessoaFisica> PessoaFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridicas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-NATHANAE\SQLEXPRESS;Database=CadastroPessoa;Integrated Security=True");
        }
    }
}
