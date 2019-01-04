﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace MukSoft.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                {
                    services.AddTransient(handlerType.AsType(), type.AsType());
                }
            }
            return services;
        }


        public static IServiceCollection AddMediatorHandlers(this IServiceCollection services)
        {
            IEnumerable<Assembly> all = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(Assembly.Load);

            foreach (Assembly assembly in all)
            {
                var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

                foreach (var type in classTypes)
                {
                    var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                    foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                    {
                        services.AddTransient(handlerType.AsType(), type.AsType());
                    }
                }
            }
            return services;
        }
    }
}
