using System.Reflection;
using Ykotika.Application;
using Ykotika.Application.Common.Mappings;
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
                config.AddProfile(new AssemblyMappingProfile(typeof(YkotikaDbContext).Assembly));
            });

            services.AddPersistence(Configuration);
            services.AddSecurity(Configuration);
            services.AddApiAuthentication();
            services.AddApplication();
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                    //policy.WithOrigins("https://infinite-ellipse-ykotika-ru-frontend-9e75.twc1.net",
                    //                   "https://infinite-ellipse-ykotika-ru-backend-869f.twc1.net")
                    //.AllowCredentials();
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
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.RoutePrefix = string.Empty;
                config.SwaggerEndpoint("swagger/v1/swagger.json", "Restaurant API");
            });
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
