using Ecommercer.Api;
using Ecommercer.Domain.Migrations;
using Ecommercer.Infra;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    options.JsonSerializerOptions.IgnoreReadOnlyFields = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

// Registra a infraestrutura, incluindo o FluentMigrator
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configurar o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

ApplyMigrations(app.Services);

app.Run();

void ApplyMigrations(IServiceProvider services)
{
    Ecommercer.Infra.DependencyInjectionExtension.ApplyMigrations(services);
}