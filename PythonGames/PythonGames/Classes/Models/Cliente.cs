using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class Cliente
    {
        [Key]
        [Display(Name = "Código do Cliente")]
        public int cd_cliente { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string nm_cli { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(14, ErrorMessage = "Este campo deve conter 14 caracteres", MinimumLength = 14)]
        public string cpf_cli { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string nm_email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(16, ErrorMessage = "Este campo deve conter entre 8 e 16 caracteres", MinimumLength = 8)]
        public string senha_cli { get; set; }

        [Display(Name = "Status do Cliente")]
        public int flag { get; set; }

    }
}
