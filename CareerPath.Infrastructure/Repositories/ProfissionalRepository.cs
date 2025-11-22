// Arquivo: ProfissionalRepository.cs
// Localização: CareerPath.Infrastructure/Repositories/ProfissionalRepository.cs

using CareerPath.Application.Interfaces;
using CareerPath.Domain.Entities;
using CareerPath.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerPath.Infrastructure.Repositories
{
    // ProfissionalRepository implementa a interface específica
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfissionalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementação do método específico
        public async Task<Profissional?> GetByEmailAsync(string email)
        {
            return await _context.Profissionais
                                 .FirstOrDefaultAsync(p => p.Email == email);
        }

        // Implementação dos métodos herdados do IRepository (exemplo: Add)
        public async Task AddAsync(Profissional profissional)
        {
            await _context.Profissionais.AddAsync(profissional);
        }
        
        // ... (Implementar os demais métodos do CRUD e SaveChangesAsync)
    }
}