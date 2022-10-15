using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgendeMeWeb.Models
{
    public class ProfissionalViewModel
    {
        public int IdCidadao { get; set; }
        public int IdCargo { get; set; }
        public int IdPrefeitura { get; set; }

        public SelectList? ListaCargos { get; set; }
        public SelectList? ListaPrefeituras { get; set; }
    }
}
