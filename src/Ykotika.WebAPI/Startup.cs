using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Ykotika.Application;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Interfaces;
using Ykotika.FileStorage;
using Ykotika.Persistence;
using Ykotika.Security;
using Ykotika.WebApi.Extensions;
using Ykotika.WebAPI.Middleware;

namespace Ykotika.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IYkotikaDbContext).Assembly));
            });

            services.AddPersistence(Configuration);
            services.AddFileStorage();
            services.AddSecurity(Configuration);
            services.AddApiAuthentication();
            services.AddApplication();
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction() || env.IsStaging())
            {
                app.UseCustomExceptionHandler();
            }
            string staticFilesPath = serviceProvider.GetService<IFileService>().BaseStaticFolder;
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath),
                RequestPath = "/static"
            });
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.RoutePrefix = string.Empty;
                config.SwaggerEndpoint("swagger/v1/swagger.json", "Ykotika API");
                config.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
