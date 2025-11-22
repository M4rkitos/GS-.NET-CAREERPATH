// Localização: CareerPath.WebAPI/Program.cs (Bloco builder.Services)

// 1. Adiciona o serviço para acessar o contexto HTTP de forma segura.
builder.Services.AddHttpContextAccessor(); 

// 2. Registra o IUriService. Usamos AddSingleton neste caso.
builder.Services.AddSingleton<IUriService>(o =>
{
    // Obtém o contexto HTTP
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext!.Request;
    
    // Constrói a URI base (ex: https://localhost:5001/)
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriString());
    
    // Retorna a implementação (UriService)
    return new UriService(uri);
});