using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace SVNUtilsWebApi
{
#pragma warning disable CS1591
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SVNUtilsWebApi", Version = "v1" });
                var currentAssembly = GetType().Assembly;
                c.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(currentAssembly.Location) ?? string.Empty, $"{currentAssembly.GetName().Name}.xml"), true);
            });

            InitRepository();
        }

        private async void InitRepository()
        {
            var rootRepository = Configuration["RootRepository"];
            PowershellHost.CustomHostedRunspace.Default.InitializeRunspaces(1, 5, "VisualSVN");
            if (await SVNUtils.SvnRepo.TryGetRepositoryAsync(rootRepository) == null)
            {
                await SVNUtils.SvnRepo.CreateRepositoryAsync(rootRepository);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SVNUtilsWebApi v1"));

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.SetIsOriginAllowed(_ => true).AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
#pragma warning restore CS1591
}
