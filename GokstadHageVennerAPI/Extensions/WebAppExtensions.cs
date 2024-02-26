using GokstadHageVennerAPI.Mappers;
using GokstadHageVennerAPI.Repository;
using GokstadHageVennerAPI.Repository.Interfaces;
using GokstadHageVennerAPI.Services;
using GokstadHageVennerAPI.Services.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Microsoft.OpenApi.Models;
using StudentBloggAPI.Mappers.Interfaces;

namespace GokstadHageVennerAPI.Extensions;


public static class WebAppExtensions
{   
    public static void RegisterMappers(this WebApplicationBuilder builder)
    {
        var assembly = typeof(MemberMapper).Assembly;

        var mapperTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)))
            .ToList();

        foreach (var mapperType in mapperTypes)
        {
            var interfaceType = mapperType.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IMapper<,>));
            builder.Services.AddScoped(interfaceType, mapperType);
        }
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var assembly = typeof(MemberService).Assembly;

        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .ToList();

        foreach (var serviceType in serviceTypes)
        {
            var interfaceTypes = serviceType.GetInterfaces()
                .Where(i => i.Name.EndsWith("Service"));

            foreach (var interfaceType in interfaceTypes)
            {
                builder.Services.AddScoped(interfaceType, serviceType);
            }
        }
    }

    public static void RegisterRepositories(this WebApplicationBuilder builder)
    {
        var assembly = typeof(MemberService).Assembly;

        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .ToList();

        foreach (var serviceType in serviceTypes)
        {
            var interfaceTypes = serviceType.GetInterfaces()
                .Where(i => i.Name.EndsWith("Repository"));

            foreach (var interfaceType in interfaceTypes)
            {
                builder.Services.AddScoped(interfaceType, serviceType);
            }
        }
    }
    
    public static void AddSwaggerWithBasicAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                In = ParameterLocation.Header,
                Description = "Basic Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "basic"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}
