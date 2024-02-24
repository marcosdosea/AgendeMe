using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgendeMeWeb.Models;

public class Atendimento 
{
    public SelectList? ListaOrgaos{ get; set; }
    public SelectList? ListaServicos{ get; set; }
}