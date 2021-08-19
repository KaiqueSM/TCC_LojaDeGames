using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PythonGames.Classes.Models
{
    public class Abastecimento
    {
        [Key]
        [Display(Name = "Código do Abastecimento")]
        public int cd_abastecimento { get; set; }

        [Display(Name = "Código do Fornecedor")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int cd_fornecedor { get; set; }

        [Display(Name = "Data do Abastecimento")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dt_abastecimento { get; set; }




        // Atributos adicionais vindos da view de Abastecimento
        [Display(Name = "CNPJ do Fornecedor")]
        public string no_cnpj { get; set; }
        [Display(Name = "Nome do Fornecedor")]
        public string nm_fornecedor { get; set; }
    }
}