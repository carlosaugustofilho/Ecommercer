using Ecommercer.Communictaion.Requests;
using Ecommercer.Communictaion.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercer.Aplication.Cases.User.Registrar
{
    public interface IRegistroUsaurio
    {
        public Task<ResponseRegistrarUsuarioJson> Execute(RequestRegistrarUsuarioJson request);

    }
}
