// Arquivo: HabilidadesController.cs
// Localização: CareerPath.WebAPI/Controllers/HabilidadesController.cs

// ... (using statements)
using CareerPath.WebAPI.Services; // Importa o seu UriService

[ApiController]
[Route("api/habilidades")]
public class HabilidadesController : ControllerBase
{
    private readonly IMatchService _matchService; 
    private readonly IUriService _uriService; // <--- A dependência que você injetou

    // Construtor: O framework injetará o MatchService e o UriService (graças ao Program.cs)
    public HabilidadesController(IMatchService matchService, IUriService uriService)
    {
        _matchService = matchService;
        _uriService = uriService; 
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search([FromQuery] HabilidadeSearchQuery query)
    {
        // 1. Executa a busca (retorna PagedResponse)
        var resultadosPaginados = await _matchService.BuscarHabilidadesPaginadasAsync(query);
        
        // Define a rota (ex: /api/habilidades/search)
        var route = Request.Path.Value!; 

        // 2. Lógica HATEOAS (Geração dos Links)
        int totalPages = resultadosPaginados.TotalPaginas;
        int currentPage = resultadosPaginados.PaginaAtual;
        
        // Geração dos links usando o serviço:
        resultadosPaginados.PrimeiraPaginaUri = _uriService.GetPaginatedSearchUri(route, query, 1, totalPages).ToString();
        resultadosPaginados.UltimaPaginaUri = _uriService.GetPaginatedSearchUri(route, query, totalPages, totalPages).ToString();

        resultadosPaginados.ProximaPaginaUri = currentPage < totalPages ? 
            _uriService.GetPaginatedSearchUri(route, query, currentPage + 1, totalPages).ToString() 
            : null; 
        
        resultadosPaginados.PaginaAnteriorUri = currentPage > 1 ? 
            _uriService.GetPaginatedSearchUri(route, query, currentPage - 1, totalPages).ToString() 
            : null; 
        
        // 3. Retorna o resultado COM os links HATEOAS
        return Ok(resultadosPaginados);
    }
}