using LibraryBook.Business.Interface;
using LibraryBook.Business.Interface.Service;
using LibraryBook.Business.Notificacoes;
using LibraryBook.Business.Services;
using LibraryBook.EF.Context;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibraryBook.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Context
            services.AddScoped<LibraryBookContext>();

            //Services
            services.AddScoped<ITokenService, TokenService>();


            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
