// Arquivo: IHabilidadeRepository.cs
// Localização: CareerPath.Application/Interfaces/IHabilidadeRepository.cs

using System.Threading.Tasks;
using CareerPath.Domain.Entities;
using CareerPath.Application.DTOs; // Necessário para usar PagedResponse e HabilidadeSearchQuery

namespace CareerPath.Application.Interfaces
{
    // A HabilidadeRepository herda o CRUD genérico (IRepository<Habilidade>)
    public interface IHabilidadeRepository : IRepository<Habilidade>
    {
        // NOVO MÉTODO PARA O REQUISITO DE BUSCA, FILTROS E PAGINAÇÃO
        Task<PagedResponse<Habilidade>> SearchAsync(HabilidadeSearchQuery query);

        // Se necessário: métodos específicos para Habilidade (ex: buscar as mais demandadas)
        Task<IEnumerable<Habilidade>> GetHabilidadesMaisDemandadas(int limit);
    }
}