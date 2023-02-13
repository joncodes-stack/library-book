using LibraryBook.Business.Interface;
using LibraryBook.Business.Interface.Repository;
using LibraryBook.Business.Interface.Service;
using LibraryBook.Business.Notificacoes;
using LibraryBook.Business.Services;
using LibraryBook.CrossCutting.Interfaces;
using LibraryBook.CrossCutting.Services;
using LibraryBook.EF.Context;
using LibraryBook.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Context
            services.AddScoped<LibraryBookContext>();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            //Reposiotories
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
