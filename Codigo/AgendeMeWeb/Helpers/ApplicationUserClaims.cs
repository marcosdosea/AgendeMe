using System.Security.Claims;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AgendeMeWeb.Helpers 
{
    public class ApplicationUserClaims : UserClaimsPrincipalFactory<UsuarioIdentity, IdentityRole>
    {
        private readonly ICidadaoService _cidadaoService;

        public ApplicationUserClaims(
            UserManager<UsuarioIdentity> userManager, 
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options,
            ICidadaoService cidadaoService)
            : base(userManager, roleManager, options)
        {
            _cidadaoService = cidadaoService;
        }

        protected override async Task<ClaimsIdentity?> GenerateClaimsAsync(UsuarioIdentity user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            if (identity != null && identity.Name != null) {
                var pessoa = _cidadaoService.GetByCPF(identity.Name);

                if (pessoa != null)
                {
                    identity.AddClaim(new Claim("Id", pessoa.Id.ToString()));
                    identity.AddClaim(new Claim("Prefeitura", pessoa.Prefeitura != null ? pessoa.Prefeitura.Id.ToString() : ""));
                    identity.AddClaim(new Claim("NomeCompleto", pessoa.Nome ?? ""));
                    identity.AddClaim(new Claim("Nome", pessoa.Nome?.Split(" ")[0] ?? ""));
                    identity.AddClaim(new Claim("Papel", Papeis[pessoa.Papel]));
                }

                return identity;
            }
            return null;
        }

        static readonly Dictionary<string, string> Papeis = new()
        { 
            {"Administrador","Administrador"},
            {"Atendente","Atendente"},
            {"gestorOrgao","Gestor do Orgão"},
            {"gestorPrefeitura","Gestor da Prefeitura"},
            {"Profissional","Profissional"},
            {"Cidadao","Cidadão"},
        };
    }

    public struct Papeis 
    {
        public const string Administrador = "ADMINISTRADOR DO SISTEMA";
        public const string Atendente = "ATENDENTE";
        public const string GestorOrgao = "GESTOR DO ORGAO";
        public const string GestorPrefeitura = "GESTOR DA PREFEITURA";
        public const string Profissional = "PROFISSIONAL";
        public const string Cidadao = "CIDADAO";
    }
}