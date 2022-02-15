using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TCC.Models
{
    public class Apartamento
    {
        public int ID { get; set; }
        [DisplayName("Proprietário")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public string Proprietario { get; set; }
        [DisplayName("Quantidade de quartos")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public int qntQuartos { get; set; }
        [DisplayName("Usuario")]
        public string User { get; set; }
    }
}