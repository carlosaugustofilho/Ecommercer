using Ecommercer.Communictaion.Requests;
using FluentValidation;

namespace Ecommercer.Aplication.Cases.User.Registrar
{
    public class ErroMenssagemUsuario : AbstractValidator<RequestRegistrarUsuarioJson>
    {
        public ErroMenssagemUsuario()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome não pode ser nulo ou vazio.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email não pode ser nulo ou vazio.")
                .EmailAddress().WithMessage("O email fornecido não é válido.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha não pode ser nula ou vazia.");
        }
    }
}
