using FluentMigrator;

namespace Ecommercer.Infra.Migrations.Versios
{
    [Migration(VercoesBanco.TABELA_USUARIO, "Criação da tabela Usuario")]
    public class Vercao01 : BaseVercao
    {
        public override void Up()
        {
            CriacaoTabela("usuarios"); 

            Alter.Table("usuarios")
                .AddColumn("Nome").AsString(255).NotNullable()
                .AddColumn("Email").AsString(255).NotNullable()
                .AddColumn("Senha").AsString(2000).NotNullable();
        }
    }
}
