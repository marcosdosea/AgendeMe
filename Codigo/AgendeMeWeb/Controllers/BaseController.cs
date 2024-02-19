using AgendeMeWeb.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AgendeMeWeb.Controllers;

public class BaseController : Controller
{
    public void SetLayout() 
    {
        if (User.IsInRole(Papeis.Cidadao)) 
        {
            ViewData["Layout"] = "_LayoutCidadao";
        }
        else if (User.IsInRole(Papeis.Atendente))
        {
            ViewData["Layout"] = "_LayoutAtendente";
        }
        else if (User.IsInRole(Papeis.GestorOrgao)) 
        {
            ViewData["Layout"] = "_LayoutGestorOrgao";
        }
        else if (User.IsInRole(Papeis.GestorOrgao)) 
        {
            ViewData["Layout"] = "_LayoutGestorOrgao";
        }
        else if (User.IsInRole(Papeis.GestorPrefeitura)) 
        {
            ViewData["Layout"] = "_LayoutGestorPrefeitura";
        }
        else if (User.IsInRole(Papeis.Administrador))
        {
            ViewData["Layout"] = "_LayoutAdministrador";
        }
        else 
        {
            ViewData["Layout"] = "_Layout";
        }
    }
}