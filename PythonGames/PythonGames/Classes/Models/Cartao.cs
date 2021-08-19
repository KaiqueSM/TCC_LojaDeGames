using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class Cartao
    {
        [Key]
        [Display(Name = "Código do Carrinho")]
        public int cd_carrinho { get; set; }

        [Display(Name = "Nome no Cartão")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string nm_cartao { get; set; }

        [Display(Name = "Número do Cartão")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(19, ErrorMessage = "Este campo deve conter 19 caracteres", MinimumLength = 19)]
        public string no_cartao { get; set; }

        [Display(Name = "CVV")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(3, ErrorMessage = "Este campo deve conter 3 caracteres", MinimumLength = 3)]
        public string no_cvv { get; set; }

        [Display(Name = "Data de validade")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dt_validade { get; set; }


    }
}
