using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class ItensCarrinho
    {
        [Display(Name = "Código do Carrihno")]
        public int cd_carrinho { get; set; }

        [Display(Name = "Código do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_produto { get; set; }

        [Display(Name = "Quantidade do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public uint qt_item { get; set; }

        [Display(Name = "Valor do item")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public double vl_item { get; set; }




        // Atributos adicionais vindos da view de itens do carrinho
        [Display(Name = "Nome do Produto")]
        public string nm_prod { get; set; }
        [Display(Name = "Link da imagem Do Produto")]
        public string link_img { get; set; }
        [Display(Name = "Categoria")]
        public string nm_categoria { get; set; }
        [Display(Name = "Preço unitário")]
        public double vl_prod { get; set; }
        [Display(Name = "Quantidade em Estoque")]
        public int qt_estoque { get; set; }
        [Display(Name = "Descrição do Produto")]
        public string prod_desc { get; set; }
    }
}
