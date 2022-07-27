using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uibasoft.BaseLab.Domain.CustomEntities;

namespace Uibasoft.BaseLab.AppIntegra.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
          
            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettingsConfigOptions>(options => configuration.GetSection("AppSettingsConfig").Bind(options));
            services.Configure<ApiSecurityModuleOption>(options => configuration.GetSection("ApiSecurityModule").Bind(options));
            services.Configure<AuthJwtOptions>(options => configuration.GetSection("AuthJwtOptions").Bind(options));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

          
            return services;
        }

        public static IServiceCollection AddSwaggers(this IServiceCollection services, string xmlFileName)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "API BASE", Version = "v1" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                doc.IncludeXmlComments(xmlPath);

                #region Basic

                //doc.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "basic",
                //    In = ParameterLocation.Header,
                //    Description = "Basic Authorization Header Using the Bearer Scheme."
                //});
                //doc.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //          new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "basic"
                //                }
                //            },
                //            new string[] {}
                //    }
                //});

                #endregion

                #region Bearer

                doc.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor inserte el Token Bearer JWT.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                doc.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });

                #endregion

            });

            return services;
        }

        public static IServiceCollection AddHangFire(this IServiceCollection services, IConfiguration configuration)
        {           
            return services;
        }


    }
}
