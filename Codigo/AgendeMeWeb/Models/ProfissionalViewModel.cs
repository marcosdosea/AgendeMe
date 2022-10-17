using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AgendeMeWeb.Models
{
    public class ProfissionalViewModel
    {
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int IdCidadao { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatótio.")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "O nome do cidadão deve ter entre 5 e 70 caracteres")]
        public string? Nome { get; set; }
        public int IdCargo { get; set; }
        public int IdProfissionalPrefeitura { get; set; }

        public SelectList? ListaCargos { get; set; }
        public SelectList? ListaPrefeituras { get; set; }
    }
}
