using FluentMigrator;

namespace Ecommercer.Infra.Migrations.Versios
{
    public abstract class BaseVercao : ForwardOnlyMigration
    {
        protected void CriacaoTabela(string tabela)
        {
            Create.Table(tabela)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Cadastrado").AsDateTime().NotNullable()
                .WithColumn("Ativo").AsBoolean().NotNullable();
        }
    }
}
 