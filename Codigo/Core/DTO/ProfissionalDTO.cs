using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class ProfissionalDTO
    {
        public int IdCidadao { get; set; }

        [Display(Name = "Nome")]
        public string? NomeCidadao { get; set; }

        [Display(Name = "Cargo")]
        public string? NomeCargo { get; set; }

        [Display(Name = "Prefeitura")]
        public string? NomePrefeitura { get; set; }
    }
}
