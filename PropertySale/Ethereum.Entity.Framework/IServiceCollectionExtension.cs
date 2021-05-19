using Ethereum.Entity.Framework.Interfaces;
using Ethereum.Entity.Framework.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Entity.Framework
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddBFEDServiceInterfaceMapping(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseService, DatabaseService>();            
            return services;
        }
    }
}
