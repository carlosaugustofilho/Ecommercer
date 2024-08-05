using Ecommercer.Domain.Entites;
using Ecommercer.Domain.Repositories;
using Ecommercer.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ecommercer.Infra.Datacontext.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbEcommecer _dbEcommecer;

        public UsuarioRepository(DbEcommecer dbEcommecer)
        {
            _dbEcommecer = dbEcommecer;
        }

        public async Task Add(Usuario usuario)
        {
            await _dbEcommecer.Usuarios.AddAsync(usuario);
            await _dbEcommecer.SaveChangesAsync();
        }

        public async Task<bool> ExistUsuarioAtivioEmail(string email)
        {
            return await _dbEcommecer.Usuarios.AnyAsync(u => u.Email.Equals(email) && u.Ativo);
        }
    }
}
