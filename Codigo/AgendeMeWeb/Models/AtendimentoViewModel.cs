using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgendeMeWeb.Models;

public class Atendimento 
{
    [Display(Name = "Órgão")]
    public SelectList? ListaOrgaos{ get; set; }
    [Display(Name = "Serviço")]
    public SelectList? ListaServicos{ get; set; }
}