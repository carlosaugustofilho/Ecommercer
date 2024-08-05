

namespace Ecommercer.Domain.Entites
{
    public class Usuario : EntidadeUsuarioBase
    {
        public long Id { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime Cadastrado { get; set; } = DateTime.UtcNow;
       

    }

}