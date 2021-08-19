using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class Suporte
    {
        [Key]
        [Display(Name = "Código do Suporte")]
        public int cd_suporte { get; set; }

        [Display(Name = "Código do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_produto { get; set; }

        [Display(Name = "Código do Carrinho")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_carrinho { get; set; }

        [Display(Name = "Data do Suporte")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dt_sup { get; set; }

        [Display(Name = "Descrição do Problema")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(500, ErrorMessage = "Este campo deve conter no máximo 500 caracteres")]
        public string sup_descricao { get; set; }

        [Display(Name = "Status do Suporte")]
        public int sup_status { get; set; }


        // Atributos adicionais vindos da view de Suporte
        [Display(Name = "Nome do Produto")]
        public string nm_prod { get; set; }
        [Display(Name = "Link da imagem Do Produto")]
        public string link_img { get; set; }

        [Display(Name = "CPF do Cliente")]
        public string cpf_cli { get; set; }
        [Display(Name = "Nome do Cliente")]
        public string nm_cli { get; set; }
        [Display(Name = "Email do Cliente")]
        public string nm_email { get; set; }
    }
}
