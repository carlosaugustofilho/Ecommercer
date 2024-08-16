using Bogus;
using Ecommercer.Communictaion.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validadores.Test
{
    public class RequestRegistrarUsuarioJsonBuilder
    {

        public static RequestRegistrarUsuarioJson Build()
        {
            return new Faker<RequestRegistrarUsuarioJson>()
            .RuleFor(user => user.Nome, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Nome))
            .RuleFor(user => user.Senha, (f) => f.Internet.Password());
        }
    }
}
