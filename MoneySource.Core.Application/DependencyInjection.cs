using AutoMapper.EquivalencyExpression;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MoneySource.Core.Application.Infrastructure.Validation;
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

            AssemblyScanner.FindValidatorsInAssembly(typeof(RequestValidatorBehavior<,>).Assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidatorBehavior<,>));
        }
    }
}
