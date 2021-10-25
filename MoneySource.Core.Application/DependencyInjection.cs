using AutoMapper.EquivalencyExpression;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MoneySource.Core.Application.Profiles;
using System.Reflection;

namespace MoneySource.Core.Application
{
    public static class DependencyIjection
    {
        public static void AddAplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(cfg => cfg.AddCollectionMappers(), typeof(SourceProfile));
        }
    }
}
