using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class Carrinho
    {
        [Key]
        [Display(Name = "Código")]
        public int cd_carrinho { get; set; }

        [Display(Name = "Código do Funcionário")]
        public int cd_funcionario { get; set; }

        [Display(Name = "Código do Cliente")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_cliente { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_forma { get; set; }

        [Display(Name = "Data da Venda")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dt_venda { get; set; }

        [Display(Name = "Valor total")]
        public double vl_total { get; set; }




        // Atributos adicionais vindos da view de carrinho
        [Display(Name = "CPF do Cliente")]
        public string cpf_cli { get; set; }
        [Display(Name = "Nome do Cliente")]
        public string nm_cli { get; set; }
        [Display(Name = "CPF do funcionário")]
        public string cpf_func { get; set; }
        [Display(Name = "Nome do funcionário")]
        public string nm_func { get; set; }
        [Display(Name = "Forma de Pagamento")]
        public string nm_forma { get; set; }


    }
}
