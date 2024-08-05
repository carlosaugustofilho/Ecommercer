using Ecommercer.Aplication.UseCases.User.Registrar;
using Ecommercer.Communictaion.Requests;
using FluentValidation;
using Ecommercer.Aplication.Cases.User.Registrar;
using Ecommercer.Infra.Datacontext.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Ecommercer.Aplication.Services.AutoMapper;
using AutoMapper;

namespace Ecommercer.Api
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddValidators(services);
            AddUseCases(services);
            AddAutoMapper(services);
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<RequestRegistrarUsuarioJson>, ErroMenssagemUsuario>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var autoMapper = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new UserProfileMapper());
            }).CreateMapper();

            services.AddSingleton(autoMapper);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegistroUsaurio, RegistroUsuario>();
        }
    }
}
