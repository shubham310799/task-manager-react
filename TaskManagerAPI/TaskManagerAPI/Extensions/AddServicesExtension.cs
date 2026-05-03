using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagerAPI.Entities;
using TaskManagerAPI.Repositories;
using TaskManagerAPI.Repositories.Interface;
using TaskManagerAPI.Services;
using TaskManagerAPI.Services.Interface;

namespace TaskManagerAPI.Extensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserTaskService, UserTaskService>();
            services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IHttpContextHelper, HttpContextHelper>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services)
        {
            var key = Encoding.UTF8.GetBytes("y7K9vQ2xT8mP4sL1aZ6rD3nF5bH0uJ8wC9eX2kM4pR7tY1qW");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}
