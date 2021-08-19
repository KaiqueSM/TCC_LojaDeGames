using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class ItensPedido
    {
        [Display(Name = "Código do Pedido")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_pedido { get; set; }

        [Display(Name = "Código do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_produto { get; set; }

        [Display(Name = "Quantidade do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public uint qt_prod { get; set; }




        // Atributos adicionais vindos da view de Itens do Pedido De Compra
        [Display(Name = "Nome do Produto")]
        public string nm_prod { get; set; }
        [Display(Name = "Link da imagem Do Produto")]
        public string link_img { get; set; }
    }
}
