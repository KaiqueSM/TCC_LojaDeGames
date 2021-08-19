using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class DetalheGame
    {
        [Key]
        [Display(Name = "Código do Game")]
        public int cd_produto { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string nm_genero { get; set; }

        [Display(Name = "Classificação Indicativa")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string vl_indicacao { get; set; }
    }
}
