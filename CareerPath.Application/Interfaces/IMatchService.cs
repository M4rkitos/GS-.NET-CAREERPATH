using System.Collections.Generic;
using System.Threading.Tasks;
using CareerPath.Application.DTOs;
using CareerPath.Domain.Entities;

namespace CareerPath.Application.Interfaces
{
    public interface IMatchService
    {
        Task<Habilidade> CriarHabilidadeAsync(HabilidadeCreateDto dto);
        Task<Habilidade> BuscarHabilidadePorIdAsync(System.Guid id);
        Task<PagedResponse<Habilidade>> BuscarHabilidadesPaginadasAsync(HabilidadeSearchQuery query);
    }
}