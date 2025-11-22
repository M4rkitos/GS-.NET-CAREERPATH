// Arquivo: UriService.cs
// Localização: CareerPath.WebAPI/Services/UriService.cs

using CareerPath.Application.DTOs;
using Microsoft.AspNetCore.WebUtilities;

namespace CareerPath.WebAPI.Services
{
    // Interface para injeção de dependência
    public interface IUriService
    {
        // Método que gera a URI para uma página específica
        Uri GetPaginatedSearchUri(string route, HabilidadeSearchQuery query, int pageNumber, int totalPages);
    }

    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPaginatedSearchUri(string route, HabilidadeSearchQuery query, int pageNumber, int totalPages)
        {
            // 1. Garante que o número da página não ultrapasse os limites
            if (pageNumber < 1) pageNumber = 1;
            if (pageNumber > totalPages && totalPages > 0) pageNumber = totalPages;
            
            // Constrói a URI base (ex: https://localhost:5001/api/habilidades/search)
            var uri = new Uri(string.Concat(_baseUri, route));
            
            // Cria um dicionário para os parâmetros da QueryString
            var parameters = new Dictionary<string, string?>
            {
                // Parâmetros de Paginação
                { nameof(HabilidadeSearchQuery.Pagina), pageNumber.ToString() },
                { nameof(HabilidadeSearchQuery.TamanhoPagina), query.TamanhoPagina.ToString() },
                
                // Parâmetros de Filtro e Ordenação (se existirem)
                { nameof(HabilidadeSearchQuery.NomeContem), query.NomeContem },
                { nameof(HabilidadeSearchQuery.OrdenarPor), query.OrdenarPor },
                { nameof(HabilidadeSearchQuery.NivelDemandaMin), query.NivelDemandaMin?.ToString() },
                { nameof(HabilidadeSearchQuery.Categoria), query.Categoria }
            };

            // Adiciona os parâmetros à URI, omitindo os valores nulos
            var modifiedUri = QueryHelpers.AddQueryString(uri.ToString(), parameters!);
            
            return new Uri(modifiedUri);
        }
    }
}