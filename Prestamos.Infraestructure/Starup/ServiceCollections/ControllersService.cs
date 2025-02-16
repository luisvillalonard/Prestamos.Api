using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Atributos;
using Prestamos.Infraestructure.Filtros;

namespace Pos.Infraestructure.ServiceCollections.ServiceCollections
{
    public static class ControllersService
    {
        private static readonly string MyAllowSpecificOrigins = "CorsAllowOrigins";
        private static readonly string SettingsApi = "SettingsApi";

        public static IServiceCollection AddControllersExtend(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettingsModel>(configuration.GetSection(SettingsApi));
            
            services
                .AddControllers(opt => { 
                    opt.Filters.Add<GlobalValidationFilterAttribute>(); 
                    opt.Filters.Add<TokenAuthenticationFilter>(); 
                })

                .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true)

                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    opt.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                    opt.UseCamelCasing(false);
                });

            services.AddHttpContextAccessor();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            var appsettings = configuration.GetSection(SettingsApi).Get<AppSettingsModel>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy
                                        .WithOrigins(appsettings == null ? new string[] { } : appsettings.CorsAllowOrigins)
                                        .AllowAnyMethod()
                                        .AllowAnyHeader();
                                  });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prestamos Api", Version = "v1" });
            });

            return services;
        }
    }
}
