// Arquivo: Curso.cs
// Localização: CareerPath.Domain/Entities/Curso.cs

using System;

namespace CareerPath.Domain.Entities
{
    public class Curso
    {
        // Construtor para aplicar as Invariantes
        public Curso(string titulo, string instituicao, string linkAcesso, int cargaHoraria, Guid habilidadeId)
        {
            // Invariante 1: Título não pode ser nulo ou vazio
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("O título do curso é obrigatório.");
            }
            // Invariante 2: Link de acesso é obrigatório
            if (string.IsNullOrWhiteSpace(linkAcesso))
            {
                throw new ArgumentException("O link de acesso ao curso é obrigatório.");
            }
            // Invariante 3: Carga horária deve ser positiva
            if (cargaHoraria <= 0)
            {
                throw new ArgumentException("A carga horária do curso deve ser maior que zero.");
            }

            this.Id = Guid.NewGuid();
            this.Titulo = titulo;
            this.Instituicao = instituicao;
            this.LinkAcesso = linkAcesso;
            this.CargaHoraria = cargaHoraria;
            this.HabilidadeId = habilidadeId; // Associa a qual habilidade ele pertence
        }
        
        // Propriedades
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Instituicao { get; private set; }
        public string LinkAcesso { get; private set; }
        public int CargaHoraria { get; private set; }
        
        // Relacionamento com Habilidade (Chave Estrangeira e Objeto de Navegação)
        public Guid HabilidadeId { get; private set; }
        public virtual Habilidade Habilidade { get; set; }

        // Método de atualização (exemplo)
        public void AtualizarLink(string novoLink)
        {
            if (string.IsNullOrWhiteSpace(novoLink))
            {
                 throw new ArgumentException("O novo link não pode ser vazio.");
            }
            this.LinkAcesso = novoLink;
        }
    }
}