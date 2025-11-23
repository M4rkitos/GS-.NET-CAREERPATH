using System;
using System.Collections.Generic;

namespace CareerPath.Domain.Entities
{
    public class Habilidade
    {
        public Habilidade() { } // Construtor vazio para EF Core

        public Habilidade(string nome, string descricao, int nivelDemanda)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            NivelDemandaGlobal = nivelDemanda;
            DataCriacao = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int NivelDemandaGlobal { get; private set; }
        public DateTime DataCriacao { get; private set; }

        // Relacionamento
        public virtual ICollection<Curso> Cursos { get; set; }
    }
}