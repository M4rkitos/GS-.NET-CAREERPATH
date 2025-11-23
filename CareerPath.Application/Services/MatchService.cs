using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerPath.Application.DTOs;
using CareerPath.Application.Interfaces;
using CareerPath.Domain.Entities;

namespace CareerPath.Application.Services
{
    public class MatchService : IMatchService
    {
        public async Task<Habilidade> CriarHabilidadeAsync(HabilidadeCreateDto dto)
        {
            // Simulação
            return new Habilidade(dto.Nome, dto.Descricao, dto.NivelDemandaGlobal);
        }

        public async Task<Habilidade> BuscarHabilidadePorIdAsync(Guid id)
        {
            return new Habilidade("Teste", "Descricao", 10);
        }

        public async Task<PagedResponse<Habilidade>> BuscarHabilidadesPaginadasAsync(HabilidadeSearchQuery query)
        {
            return new PagedResponse<Habilidade>
            {
                PaginaAtual = query.Pagina,
                TamanhoPagina = query.TamanhoPagina,
                TotalRegistros = 1,
                TotalPaginas = 1,
                Dados = new List<Habilidade> { new Habilidade("Java", "Backend", 10) }
            };
        }
    }
}