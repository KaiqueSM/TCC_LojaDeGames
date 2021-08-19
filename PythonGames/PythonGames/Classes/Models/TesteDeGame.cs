using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class TesteDeGame
    {
        [Key]
        [Display(Name = "Código do Teste de Game")]
        public int cd_teste { get; set; }

        [Display(Name = "Código do Produto")]
        [Required(ErrorMessage = "Escolha o Produro")]
        public int cd_produto { get; set; }

        [Display(Name = "Código do Cliente")]
        [Required(ErrorMessage = "Escolha o Cliente")]
        public int cd_cliente { get; set; }

        [Display(Name = "Data do Teste")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dt_teste { get; set; }

        [Display(Name = "Hora do Teste")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string hr_teste { get; set; }




        // Atributos adicionais vindos da view de Teste de Game
        [Display(Name = "Nome do Produto")]
        public string nm_prod { get; set; }
        [Display(Name = "Link da imagem Do Produto")]
        public string link_img { get; set; }

        [Display(Name = "CPF do Cliente")]
        public string cpf_cli { get; set; }
        [Display(Name = "Nome do Cliente")]
        public string nm_cli { get; set; }

    }
}
