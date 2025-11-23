using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
// Usings das camadas do projeto
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
// Se você tiver outros repositórios, adicione-os aqui
builder.Services.AddScoped<IMatchService, MatchService>();

// 4. Configuração do HATEOAS (UriService)
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<UriService>(o =>
{
    var accessor = o.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    // CORREÇÃO APLICADA AQUI: .ToUriComponent()
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

var app = builder.Build();

// 5. MIGRAÇÃO AUTOMÁTICA AO INICIAR
// Tenta conectar ao PostgreSQL na VM Linux e criar o banco
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Aplica as tabelas
        Console.WriteLine("INFO: Banco de dados migrado e conectado com sucesso!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "ERRO CRÍTICO: Falha ao conectar ou migrar o banco de dados.");
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