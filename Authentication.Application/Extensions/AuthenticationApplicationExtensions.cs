﻿using Authentication.Application.UserModule;

namespace Authentication.Application.Extensions
{
    public static class AuthenticationApplicationExtensions
    {
        public static void AddApplicationExtensions( this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
