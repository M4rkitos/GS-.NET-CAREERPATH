using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
// Importante: Usings para encontrar as outras camadas
using CareerPath.Infrastructure.Data;
using CareerPath.Infrastructure.Repositories;
using CareerPath.Application.Interfaces;
using CareerPath.Application.Services;
using CareerPath.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Adicionar Serviços ao Container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Configuração do Banco de Dados (PostgreSQL)
// Lê a ConnectionString do appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Injeção de Dependência (Vínculo entre Interface e Implementação)
builder.Services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
// builder.Services.AddScoped<IHabilidadeRepository, HabilidadeRepository>(); // Descomente se tiver criado este repo
builder.Services.AddScoped<IMatchService, MatchService>();

// 4. Configuração do HATEOAS (UriService)
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<UriService>(o =>
{
    var accessor = o.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriString());
    return new UriService(uri);
});

var app = builder.Build();

// 5. MIGRAÇÃO AUTOMÁTICA (O Segredo do DevOps)
// Isso vai criar o banco de dados na VM Linux ao iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Aplica as tabelas no PostgreSQL
        Console.WriteLine("INFO: Banco de dados migrado e atualizado com sucesso!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "ERRO: Falha ao aplicar migrações no banco de dados.");
    }
}

// Configuração do Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();