using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AgendeMeWeb.Models
{
    public class ProfissionalViewModel
    {
        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "É obrigatório definir um cargo")]
        public int IdCargo { get; set; }

        [Display(Name = "Profissional")]
        [Required(ErrorMessage = "É obrigatório definir um profissional")]
        public int IdProfissional { get; set; }

        [Display(Name = "Prefeitura")]
        [Required(ErrorMessage = "É obrigatório definir uma prefeitura")]
        public int IdPrefeitura { get; set; }

        [Display(Name = "Profissional")]
        public string? NomeProfissional { get; set; }
        [Display(Name = "Prefeitura")]
        public string? NomePrefeitura { get; set; }
        [Display(Name = "Cargo")]
        public string? NomeCargo { get; set; }

        public SelectList? ListaCargos { get; set; }
        public SelectList? ListaPrefeituras { get; set; }
    }
}
