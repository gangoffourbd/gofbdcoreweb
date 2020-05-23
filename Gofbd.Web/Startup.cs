namespace Gofbd.Web
{
    using Gofbd.Core;
    using Gofbd.DataAccess;
    using Gofbd.Web.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.AspNetCore.Authentication;
    using Gofbd.Web.Services;
    using Gofbd.Web.Handler;
    using MediatR;
    using System.Linq;

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
            services.AddDbContext<DataContext>((options) =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection"); //"Server=JAHIR-PC;Database=master;User Id=sa;password=MyPassord2020;Trusted_Connection=False;MultipleActiveResultSets=true;";//this.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IDataContext, DataContext>();
            services.AddScoped<IDataContextFactory, DataContextFactory>();
            services.AddSerilog();
            services.AddControllersWithViews();
            //services.AddBasicAuthentication();
            services.AddJwtAuthentication(Configuration);
            services.AddMediatR(AssemblyProvider.Instance.Assemblies.ToArray());
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
