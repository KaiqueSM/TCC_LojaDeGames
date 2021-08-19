using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class ItensAbs
    {
        [Display(Name = "Código do Abastecimento")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_abastecimento { get; set; }

        [Display(Name = "Código do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_produto { get; set; }

        [Display(Name = "Quantidade do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public uint qt_prod { get; set; }




        // Atributos adicionais vindos da view de Itens do Abastecimento
        [Display(Name = "Nome do Produto")]
        public string nm_prod { get; set; }
        [Display(Name = "Link da imagem Do Produto")]
        public string link_img { get; set; }
    }
}
