using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AgendeMeWeb.Models
{
    public class ProfissionalViewModel
    {
        public int IdCargo { get; set; }
        public int IdProfissional { get; set; }
        public int IdPrefeitura { get; set; }


        public string? NomeProfissional { get; set; }
        public string? NomePrefeitura { get; set; }
        public string? NomeCargo { get; set; }
        public SelectList? ListaCargos { get; set; }
        public SelectList? ListaPrefeituras { get; set; }
    }
}
