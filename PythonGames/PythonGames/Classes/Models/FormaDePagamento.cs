using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PythonGames.Classes.Models
{
    public class FormaDePagamento
    {
        [Key]
        [Display(Name = "Código da Froma")]
        public int cd_forma { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string nm_forma { get; set; }
    }
}
