using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class Funcionario
    {
        [Key]
        [Display(Name = "Código do Funcionário")]
        public int cd_funcionario { get; set; }

        [Display(Name = "Nome do Funcionário")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter até 50 caracteres")]
        public string nm_func { get; set; }

        [Display(Name = "CPF do Funcionário")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(14, ErrorMessage = "Este campo deve conter 14 caracteres", MinimumLength = 14)]
        public string cpf_func { get; set; }

        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(20, ErrorMessage = "Este campo deve conter entre 8 e 20 caracteres", MinimumLength = 8)]
        public string nm_usu { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(16, ErrorMessage = "Este campo deve conter entre 8 e 16 caracteres", MinimumLength = 8)]
        public string senha_usu { get; set; }

        [Display(Name = "Acesso do Funcionário")]
        public int func_acesso { get; set; }

        [Display(Name = "Status do Funcionário")]
        public int flag { get; set; }
    }
}
