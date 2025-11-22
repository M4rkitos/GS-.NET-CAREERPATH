// Arquivo: PagedResponse.cs (Genérico)
// Localização: CareerPath.Application/DTOs/PagedResponse.cs

using System.Collections.Generic;

namespace CareerPath.Application.DTOs
{
    public class PagedResponse<T> where T : class
    {
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalRegistros { get; set; }
        public IEnumerable<T> Dados { get; set; } = new List<T>();
        
        // Propriedades para HATEOAS
        public string? ProximaPaginaUri { get; set; }
        public string? PaginaAnteriorUri { get; set; }
        public string? PrimeiraPaginaUri { get; set; }
        public string? UltimaPaginaUri { get; set; }
    }
}