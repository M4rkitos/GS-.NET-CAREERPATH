// Arquivo: IProfissionalRepository.cs
// Localização: CareerPath.Application/Interfaces/IProfissionalRepository.cs

using System.Threading.Tasks;
using CareerPath.Domain.Entities;

namespace CareerPath.Application.Interfaces
{
    // ProfissionalRepository herda o CRUD genérico e adiciona métodos específicos
    public interface IProfissionalRepository : IRepository<Profissional>
    {
        Task<Profissional?> GetByEmailAsync(string email);
        
        // Exemplo de um método de consulta complexa para o Match
        Task<IEnumerable<Profissional>> GetByProfissaoAndExperiencia(string profissao, int minExperiencia);
    }
}