using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
// Importa as camadas que conectamos no .csproj
using CareerPath.Application.DTOs;
using CareerPath.Application.Services;
using CareerPath.WebAPI.Services; // Para o UriService

namespace CareerPath.WebAPI.Controllers
{
    [ApiController]
    [Route("api/habilidades")]
    public class HabilidadesController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly UriService _uriService;

        public HabilidadesController(IMatchService matchService, UriService uriService)
        {
            _matchService = matchService;
            _uriService = uriService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HabilidadeCreateDto dto)
        {
            // Simulação de criação para o teste
            return Ok(new { Message = "Habilidade Criada", Data = dto });
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] HabilidadeSearchQuery query)
        {
            var resultadosPaginados = await _matchService.BuscarHabilidadesPaginadasAsync(query);
            
            var route = Request.Path.Value!;
            
            // Lógica HATEOAS simplificada para o teste
            resultadosPaginados.PrimeiraPaginaUri = _uriService.GetPaginatedSearchUri(route, query, 1, resultadosPaginados.TotalPaginas).ToString();
            
            return Ok(resultadosPaginados);
        }
    }
}