using JCalzado.Data;
using JCalzado.WebAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace JCalzado.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<TiendaDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("TiendaDb")));

            services.ConfigureDependencies();

            services.AddAutoMapper(typeof(Startup));

            services.ConfigureJWT(Configuration);

            services.ConfigureCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CALZADO API",
                    Description = "ASP.NET Core Web API CALZADO",
                    TermsOfService = new Uri("https://andrewprogramador.wixsite.com/cibernet"),
                    Contact = new OpenApiContact
                    {
                        Name = "Sigi Andre Diaz Quiroz",
                        Email = "andrew_programador@hotmail.com",
                        Url = new Uri("https://www.instagram.com/andre_diazq/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GITHUB REPOSITORIO",
                        Url = new Uri("https://github.com/sigiandre/Calzado-Backend"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API CALZADO V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
