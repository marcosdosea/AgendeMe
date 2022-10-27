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

        public SelectList? ListaCargos { get; set; }
        public SelectList? ListaPrefeituras { get; set; }
    }
}
