using Core.Security.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Core.Security;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services,IConfiguration configuration)
    {
        TokenOptions? tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

        if (tokenOptions == null)
            throw new Exception("TokenOptions section could not be bound");

        services.AddSingleton(tokenOptions);

        services.AddScoped<ITokenHelper, JwtHelper>();
        return services;
    }
}
