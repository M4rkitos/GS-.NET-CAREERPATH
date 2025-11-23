using CareerPath.Application.Interfaces;
using CareerPath.Domain.Entities;
using CareerPath.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerPath.Infrastructure.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfissionalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Profissional?> GetByEmailAsync(string email)
        {
            return await _context.Profissionais
                                 .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task AddAsync(Profissional entity)
        {
            await _context.Profissionais.AddAsync(entity);
        }

        public async Task<Profissional?> GetByIdAsync(Guid id)
        {
            return await _context.Profissionais.FindAsync(id);
        }

        public async Task<IEnumerable<Profissional>> GetAllAsync()
        {
            return await _context.Profissionais.ToListAsync();
        }

        public void Update(Profissional entity)
        {
            _context.Profissionais.Update(entity);
        }

        public void Delete(Profissional entity)
        {
            _context.Profissionais.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Método exigido pela interface que estava faltando
        public async Task<IEnumerable<Profissional>> GetByProfissaoAndExperiencia(string profissao, int minExperiencia)
        {
            return await _context.Profissionais
                .Where(p => p.ProfissaoAtual == profissao && p.AnosExperiencia >= minExperiencia)
                .ToListAsync();
        }
    }
}