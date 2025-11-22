// Arquivo: HabilidadeSearchQuery.cs
// Localização: CareerPath.Application/DTOs/HabilidadeSearchQuery.cs

using System.ComponentModel.DataAnnotations;

namespace CareerPath.Application.DTOs
{
    public class HabilidadeSearchQuery
    {
        // --- Filtros (Opcionais) ---
        public string? NomeContem { get; set; } // Filtra habilidades por parte do nome
        public int? NivelDemandaMin { get; set; } // Filtra habilidades por demanda mínima (1 a 10)
        public string? Categoria { get; set; } // Filtra por uma categoria de habilidade (ex: 'Cloud', 'Data Science')

        // --- Paginação (Obrigatórios, mas com defaults) ---
        [Range(1, int.MaxValue, ErrorMessage = "A página deve ser maior que zero.")]
        public int Pagina { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "O tamanho da página deve ser entre 1 e 100.")]
        public int TamanhoPagina { get; set; } = 10;

        // --- Ordenação (Opcional) ---
        // Ex: "NomeAsc", "NomeDesc", "DemandaDesc"
        public string? OrdenarPor { get; set; }
    }
}