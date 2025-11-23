using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
// CORREÇÃO DOS USINGS:
using CareerPath.Application.DTOs;
using CareerPath.Application.Interfaces; // Interface está aqui
using CareerPath.WebAPI.Services; 

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
            var habilidade = await _matchService.CriarHabilidadeAsync(dto);
            return Ok(habilidade);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search([FromQuery] HabilidadeSearchQuery query)
        {
            var resultados = await _matchService.BuscarHabilidadesPaginadasAsync(query);
            return Ok(resultados);
        }
    }
}