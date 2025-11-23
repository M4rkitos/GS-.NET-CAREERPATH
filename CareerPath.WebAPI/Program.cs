using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
// Usings das camadas
using CareerPath.Infrastructure.Data;
using CareerPath.Infrastructure.Repositories;
using CareerPath.Application.Interfaces;
using CareerPath.Application.Services;
using CareerPath.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Adicionar Serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. CONFIGURAÇÃO DO BANCO DE DADOS (HARDCODED PARA GARANTIR)
// Conecta direto na VM Linux (10.0.0.4)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=10.0.0.4;Port=5432;Database=CareerPathDB;Username=postgres;Password=Abc123456789!;"));

// 3. Injeção de Dependência
builder.Services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
builder.Services.AddScoped<IMatchService, MatchService>();

// 4. Configuração do HATEOAS (Corrigida)
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<UriService>(o =>
{
    var accessor = o.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    // Fix: Usando ToUriComponent()
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

var app = builder.Build();

// 5. MIGRAÇÃO AUTOMÁTICA (Cria o banco no Linux ao rodar)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Aplica as tabelas
        Console.WriteLine("INFO: SUCESSO! Banco de dados migrado e conectado no Linux!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        // Se der erro, ele vai mostrar o motivo detalhado no console
        logger.LogError(ex, "ERRO CRÍTICO: Falha ao conectar no PostgreSQL.");
    }
}

// Configuração do Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();