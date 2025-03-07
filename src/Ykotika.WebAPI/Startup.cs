using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Threading.RateLimiting;
using Ykotika.Application;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Interfaces;
using Ykotika.FileStorage;
using Ykotika.Persistence;
using Ykotika.Security;
using Ykotika.SpreadsheetService;
using Ykotika.Email;
using Ykotika.WebApi.Extensions;
using Ykotika.WebAPI.Middleware;
using Ykotika.WebAPI.ModelBinders;

namespace Ykotika.WebAPI
{
    public class Startup(IConfiguration configuration)
    {
        private readonly string _policyCORSName = "CORSOrigins";

        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IYkotikaDbContext).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(CellProperty).Assembly));
            });

            services.AddPersistence(Configuration);
            services.AddFileStorage();
            services.AddSecurity(Configuration);
            services.AddArticle();
            services.AddEmail(Configuration);
            services.AddSpreadsheet();
            services.AddApiAuthentication();
            services.AddApplication();
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new CustomQueryBinderProvider());
            });
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("fixed", httpContext =>
                {
                    return
                    RateLimitPartition.GetFixedWindowLimiter
                    (partitionKey: httpContext.User.Identity?.Name
                    ?? httpContext.Connection.RemoteIpAddress?.ToString()
                    ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 15,
                        Window = TimeSpan.FromSeconds(5)
                    });
                });
                options.AddPolicy("RefreshTokenLimiter", context =>
                    RateLimitPartition.GetFixedWindowLimiter(context.Connection.RemoteIpAddress?.ToString(), _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 1,
                        Window = TimeSpan.FromSeconds(5),
                        QueueLimit = 0
                    }));
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")?.Split(";");

            services.AddCors(options =>
            {
                options.AddPolicy(_policyCORSName, policy =>
                {
                    policy
                        .WithOrigins(allowedOrigins ?? ["http://localhost:3000", "https://localhost:3000"])
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
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.RoutePrefix = string.Empty;
                    config.SwaggerEndpoint("swagger/v1/swagger.json", "Ykotika API");
                    config.InjectStylesheet("/swagger-ui/SwaggerDark.css");
                });
            }
            if (env.IsProduction() || env.IsStaging())
            {
                app.UseCustomExceptionHandler();
            }
            string staticFilesPath = serviceProvider.GetService<IFileService>().BaseStaticFolder;
            app.UseRouting();
            app.UseCors(_policyCORSName);
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath),
                RequestPath = "/static"
            });
            app.UseHttpsRedirection();
            app.UseRateLimiter();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
