using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class PedidoDeCompra
    {
        [Key]
        [Display(Name = "Código do Pedido")]
        public int cd_pedido { get; set; }

        [Display(Name = "Código do Funcionárioa")]
        public int cd_funcionario { get; set; }

        [Display(Name = "Código do Fornecedor")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_fornecedor { get; set; }

        [Display(Name = "Data do Pedido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dt_pedido { get; set; }

        [Display(Name = "Status do Pedido")]
        public int ped_status { get; set; }


        // Atributos adicionais vindos da view de Pedido De Compra
        [Display(Name = "CPF do funcionário")]
        public string cpf_func { get; set; }
        [Display(Name = "CNPJ do Fornecedor")]
        public string no_cnpj { get; set; }
        [Display(Name = "Nome do Fornecedor")]
        public string nm_fornecedor { get; set; }
    }
}
