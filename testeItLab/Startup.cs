using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using testeItLab.web.Utils;
using testItLab.infra.Data.Extensions;

namespace testeItLab.web
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
            services.AddCors(opt => opt.AddDefaultPolicy(builder =>
               builder.AllowAnyOrigin()));

            services.AddSwaggerGen(c =>
            {
                var version = GetType().Assembly.GetName().Version.ToString();
                var appPath = PlatformServices.Default.Application.ApplicationBasePath;
                var appName = PlatformServices.Default.Application.ApplicationName;
                var xmlDoc = Path.Combine(appPath, $"{appName}.xml");
                c.SwaggerDoc("v1", new Info
                {
                    Title = appName,
                    Version = version
                });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(requirement: new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", Enumerable.Empty<string>() },
                });
                c.SchemaFilter<EnumSchemaFilter>();
                c.DescribeStringEnumsInCamelCase();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDataContext();
            services.AddRepositories();
            services.AddServices();
            services.AddModelMapping();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseDeveloperExceptionPage();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "test-itlab");
                c.RoutePrefix = string.Empty;
            });
            if (!env.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseMvc();
        }
    }
}
