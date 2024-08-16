using AutoMapper;
using Ecommercer.Communictaion.Requests;
using Ecommercer.Communictaion.Response;
using Ecommercer.Domain.Entites;
using Ecommercer.Domain.Repositories;
using FluentValidation;
using Ecommercer.Aplication.Services.Cripto;
using System;
using System.Linq;
using System.Threading.Tasks;
using Ecommercer.Aplication.Cases.User.Registrar;

namespace Ecommercer.Aplication.UseCases.User.Registrar
{
    public class RegistroUsuario : IRegistroUsaurio
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IValidator<RequestRegistrarUsuarioJson> _validator;
        private readonly IMapper _mapper;

        public RegistroUsuario(IUsuarioRepository usuarioRepository, IValidator<RequestRegistrarUsuarioJson> validator, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ResponseRegistrarUsuarioJson> Execute(RequestRegistrarUsuarioJson request)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errorMessages);
            }

            var usuario = _mapper.Map<Usuario>(request);
            usuario.Senha = CriptoSenha.Encrypt(request.Senha, request.Nome);

            if (_usuarioRepository != null && await _usuarioRepository.ExistUsuarioAtivioEmail(request.Email))
            {
                throw new ArgumentException("O email já está em uso.");
            }

            if (_usuarioRepository != null)
            {
                await _usuarioRepository.Add(usuario);
            }

            return new ResponseRegistrarUsuarioJson
            {
                Name = request.Nome,
            };
        }


    }


}
