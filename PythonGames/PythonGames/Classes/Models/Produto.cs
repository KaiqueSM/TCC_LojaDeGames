using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class Produto
    {
        [Key]
        [Display(Name = "Código do Produto")]
        public int cd_produto { get; set; }

        [Display(Name = "Nome do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string nm_prod { get; set; }

        [Display(Name = "Link da imagem Do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(200, ErrorMessage = "Este campo deve conter no máximo 200 caracteres")]
        public string link_img { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        public string nm_categoria { get; set; }

        [Display(Name = "Preço unitário")]
        [Required(ErrorMessage = "Preço unitário é um Campo Obrigatório!")]
        public double vl_prod { get; set; }

        [Display(Name = "Quantidade em Estoque")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int qt_estoque { get; set; }

        [Display(Name = "Descrição do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(400, ErrorMessage = "Este campo deve conter no máximo 400 caracteres")]
        public string prod_desc { get; set; }

        [Display(Name = "Status")]
        public int flag { get; set; }


        // Atributos adicionais vindos de view (só para games)
        [Display(Name = "Gêneoro do Game")]
        public string nm_genero { get; set; }
        [Display(Name = "Classificação Indicativa")]
        public string vl_indicacao { get; set; }


    }
}
