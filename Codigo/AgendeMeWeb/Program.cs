using AgendeMeWeb.Helpers;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service;

namespace AgendeMeWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AgendeMeContext>(
                options => options.UseMySQL(builder.Configuration.GetConnectionString("AgendeMeDatabase")));

            builder.Services.AddDbContext<IdentityContext>(
                options => options.UseMySQL(builder.Configuration.GetConnectionString("AgendeMeDatabase")));

            builder.Services.AddIdentity<UsuarioIdentity, IdentityRole>(options =>
            {
                // SignIn settings
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;

                // Default Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<IdentityContext>().AddRoles<IdentityRole>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "AgendeMeSession";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                // ReturnUrlParameter requires 
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            builder.Services.AddTransient<IAgendaDoServicoService, AgendaDoServicoService>();
            builder.Services.AddTransient<IAgendamentoService, AgendamentoService>();
            builder.Services.AddTransient<IAreaDeServicoService, AreaDeServicoService>();
            builder.Services.AddTransient<ICargoService, CargoService>();
            builder.Services.AddTransient<ICidadaoService, CidadaoService>();
            builder.Services.AddTransient<IOrgaoPublicoService, OrgaoPublicoService>();
            builder.Services.AddTransient<IPrefeituraService, PrefeituraService>();
            builder.Services.AddTransient<IServicoPublicoService, ServicoPublicoService>();
            builder.Services.AddTransient<IDiaAgendamentoService, DiaAgendamentoService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IUserClaimsPrincipalFactory<UsuarioIdentity>, ApplicationUserClaims>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.MapRazorPages();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=AgendarServico}/{action=Index}/{id?}");

            app.Run();
        }
    }
}