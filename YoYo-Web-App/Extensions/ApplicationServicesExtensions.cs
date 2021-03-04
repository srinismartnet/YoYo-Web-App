using Core.Interface;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoYo_Web_App.Services.Athlets;
using YoYo_Web_App.Services.Interfaces.AthletServices;

namespace YoYo_Web_App.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAthletService, AthletService>();
            services.AddScoped<IAthletRepository, AthletRepository>();
            return services;
        }
    }
}
