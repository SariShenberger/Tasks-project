using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using hw1.Interfaces;
using hw1.Services;

namespace hw1.Utilities
{
    public static class additions
    {
        public static void AddTask(this IServiceCollection services){
            services.AddSingleton<IConnect,TaskService>();
            
        }
    }
}