using AgoraSocialApp.Converters;
using AgoraSocialApp.Data;
using AgoraSocialApp.Filters;
using AgoraSocialApp.Models;
using IdentityServer4.Contrib.RavenDB.Services;
using IdentityServer4.Contrib.RavenDB.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raven.Client.Json.Serialization.NewtonsoftJson;
using Raven.DependencyInjection;
using Raven.Identity;

namespace AgoraSocialApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddRavenDbDocStore(options =>
            {
                // workaround for https://github.com/JamesNK/Newtonsoft.Json/issues/1713 via https://github.com/dotnet/aspnetcore/issues/22620 and https://github.com/JudahGabriel/RavenDB.Identity/issues/16#issuecomment-526336542
                // TODO: Check if this ever gets fixed... note: it might not
                options.BeforeInitializeDocStore = store =>
                {
                    store.Conventions.Serialization = new NewtonsoftJsonSerializationConventions
                    {
                        CustomizeJsonSerializer = (serializer) =>
                        {
                            serializer.Converters.Add(new JsonClaimConverter());
                            serializer.Converters.Add(new JsonClaimsPrincipalConverter());
                            serializer.Converters.Add(new JsonClaimsIdentityConverter());
                        }
                    };
                };
            });
            services.AddRavenDbAsyncSession();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRavenDbIdentityStores<ApplicationUser, Raven.Identity.IdentityRole>();
                //.AddEntityFrameworkStores<ApplicationDbContext>();
             
            services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
                //.AddOperationalStore<TContext>()
                //.ConfigureReplacedServices()
                .AddIdentityResources()
                .AddApiResources()
                //.AddClients()
                .AddClientStore<RavenDBClientStore>()
                //.AddResourceStore<AgoraRavenDBResourceStore>()
                .AddCorsPolicyService<CorsPolicyService>()
                .AddSigningCredentials();


            //.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt()
                .AddGoogle(o =>
                {
                    //o.SignInScheme = IdentityConstants.ExternalScheme;

                    IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                    o.ClientId = googleAuthNSection["ClientId"];
                    o.ClientSecret = googleAuthNSection["ClientSecret"];
                });
                //.AddExternalCookie();

            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddMvcOptions(o => o.Filters.Add<RavenSaveChangesAsyncFilter>());


            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
                //configuration.RootPath = "SvelteApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                //spa.Options.SourcePath = "ClientApp";
                //spa.Options.SourcePath = "SvelteApp";
                spa.Options.SourcePath = "SvelteKit";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");

                }
            });
        }
    }
}
