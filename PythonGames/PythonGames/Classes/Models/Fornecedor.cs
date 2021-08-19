using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class Fornecedor
    {
        [Key]
        [Display(Name = "Código do Fornecedor")]
        public int cd_fornecedor { get; set; }

        [Display(Name = "Nome do Fornecedor")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string nm_fornecedor { get; set; }

        [Display(Name = "CNPJ do Funcionário")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(18, ErrorMessage = "Este campo deve conter 18 caracteres", MinimumLength = 18)]
        public string no_cnpj { get; set; }

        [Display(Name = "Status do Fornecedor")]
        public int flag { get; set; }

    }
}
