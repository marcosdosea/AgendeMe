
using System.Security.Claims;
using AgendeMeWeb.Areas.Identity.Data;
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
                var pessoa = _cidadaoService.GetByCPF(identity.Name) ?? _cidadaoService.GetByEmail(identity.Name);

                if (pessoa != null)
                {
                    identity.AddClaim(new Claim("Id", pessoa.Id.ToString()));
                    identity.AddClaim(new Claim("IdPrefeitura", pessoa.IdPrefeitura.ToString() ?? ""));
                }

                return identity;
            }
            return null;
        }
    }
}