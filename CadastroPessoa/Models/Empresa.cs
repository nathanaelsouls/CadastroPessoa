using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoa.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio")]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio")]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio")]
        [Display(Name = "Endereço-Empresa")]
        public string Endereco { get; set; }

    }
}
