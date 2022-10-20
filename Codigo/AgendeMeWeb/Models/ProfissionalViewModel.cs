using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AgendeMeWeb.Models
{
    public class ProfissionalViewModel
    {
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int IdCidadao { get; set; }

        public string? NomeCidadao { get; set; }

        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int IdCargo { get; set; }

        public string? NomeCargo { get; set; }

        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int IdProfissionalPrefeitura { get; set; }

        public string? NomePrefeitura { get; set; }

        public SelectList? ListaCargos { get; set; }
        public SelectList? ListaPrefeituras { get; set; }
    }
}
