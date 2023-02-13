using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryBook.Api.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection JwtConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            // JWT

            var secretKey = configuration.GetSection("Settings:Secret");
            var issuer = configuration.GetSection("Settings:Issuer");
            var validOn = configuration.GetSection("Settings:ValidOn");

            var key = Encoding.ASCII.GetBytes(secretKey.Value);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = validOn.Value,
                    ValidIssuer = issuer.Value
                };
            });

            return services;
        }
    }
}
