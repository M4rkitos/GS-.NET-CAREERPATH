// Arquivo: IRepository.cs
// Localização: CareerPath.Application/Interfaces/IRepository.cs

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareerPath.Domain.Entities; // Depende apenas do Domínio

namespace CareerPath.Application.Interfaces
{
    // T é a Entidade que este repositório lida (ex: Profissional, Curso)
    public interface IRepository<T> where T : class 
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync(); // Para o Unit of Work
    }
}