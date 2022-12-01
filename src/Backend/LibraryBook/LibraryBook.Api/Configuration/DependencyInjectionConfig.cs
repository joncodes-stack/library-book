using LibraryBook.Business.Interface;
using LibraryBook.Business.Interface.Repository;
using LibraryBook.Business.Interface.Service;
using LibraryBook.Business.Notificacoes;
using LibraryBook.Business.Services;
using LibraryBook.EF.Context;
using LibraryBook.EF.Repositories;
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
            services.AddScoped<IUserService, UserService>();

            //Reposiotories
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
