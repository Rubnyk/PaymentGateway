using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Domain.Models;
using System.Linq;
using System.Text;

namespace PaymentGateway.Api
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {         
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            services.Configure<RetryConfig>(configuration.GetSection("RetryConfig"));
            return services;
        }

        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwagger();
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.AddJwtAuth(configuration);
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); });

            return services;
        }

        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = false
                };
            });
            return services;
        }
    }
}
