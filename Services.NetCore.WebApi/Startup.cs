using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.NetCore.Infraestructure.Data.UnitOfWork;
using Services.NetCore.WebApi.DependencyInjection;

namespace Services.NetCore.WebApi
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "*";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Context Of Data
            services.AddDbContext<DataContext>(options =>
                                           options.UseSqlServer(GetConnectionString(),
                                           x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "EfCore_dbo")
                                           .MigrationsAssembly("Services.Workforce.WebApi")));


            //Dependency Injection
            new Container(services);

            //Enable Cors
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            //Configuration Controller Response
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages().AddNewtonsoftJson();

            //Configuration Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Services.NetCore.WebApi",
                    Description = "This application is using .net core version 8",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
        }

        private string GetConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnectionString");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Services NetCore WebApi");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
