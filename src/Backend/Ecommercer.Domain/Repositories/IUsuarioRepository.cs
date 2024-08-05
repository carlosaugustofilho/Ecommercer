using Ecommercer.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommercer.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        public Task Add(Usuario usuario);

        public Task<bool> ExistUsuarioAtivioEmail(string email);
    }
}
