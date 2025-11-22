// Arquivo: Program.cs (Camada WebAPI)

// ... código anterior

var app = builder.Build();

// Bloco de Aplicação de Migrações (SOLUÇÃO DE BYPASS DO DOTNET EF)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Obtém o contexto do banco de dados
        var context = services.GetRequiredService<ApplicationDbContext>(); 
        
        // Aplica a migração pendente, criando o banco de dados e as tabelas
        context.Database.Migrate(); 

        Console.WriteLine("INFO: Migrações aplicadas com sucesso!");
    }
    catch (Exception ex)
    {
        // Se houver erro de conexão (o que não deve ocorrer mais), o erro será logado
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "ERRO: Ocorreu um erro durante a migração do banco de dados.");
    }
}
// Fim do Bloco de Migrações

// ... código do pipeline (app.UseSwagger, app.UseHttpsRedirection, etc.)