using AutoMapper;
using Ecommercer.Aplication.Cases.User.Registrar;
using Ecommercer.Aplication.UseCases.User.Registrar;
using Ecommercer.Communictaion.Requests;
using Ecommercer.Domain.Entites;
using Ecommercer.Domain.Repositories;
using Ecommercer.Infra.Datacontext.Repositories;
using FluentAssertions;
using FluentValidation;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Validadores.Test.Usuarios.Registrar
{
    public class RegistroUsuarioTest
    {
        [Fact]
        public void Sucess()
        {
            IValidator<RequestRegistrarUsuarioJson> validator = new ErroMenssagemUsuario();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RequestRegistrarUsuarioJson, Usuario>());
            IMapper mapper = new Mapper(config);

            IUsuarioRepository usuarioRepository = null;

            var registroUsuario = new RegistroUsuario(usuarioRepository, validator, mapper);

            var request = RequestRegistrarUsuarioJsonBuilder.Build();

            var result = registroUsuario.Execute(request).Result;
            result.Should().NotBeNull();
        }
        [Fact]
        public void Error_Name()
        {
            IValidator<RequestRegistrarUsuarioJson> validator = new ErroMenssagemUsuario();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RequestRegistrarUsuarioJson, Usuario>());
            IMapper mapper = new Mapper(config);

            IUsuarioRepository usuarioRepository = null; 
            var registroUsuario = new RegistroUsuario(usuarioRepository, validator, mapper);

            var request = RequestRegistrarUsuarioJsonBuilder.Build();

            request.Nome = string.Empty; 

            var validationResult = validator.Validate(request); 
            validationResult.IsValid.Should().BeFalse();

            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage == "O nome não pode ser nulo ou vazio.");
        }

    }
}

