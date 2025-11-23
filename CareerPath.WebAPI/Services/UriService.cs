using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using CareerPath.Application.DTOs; // <--- O IMPORTANTE QUE FALTAVA

namespace CareerPath.WebAPI.Services
{
    public class UriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPaginatedSearchUri(string route, HabilidadeSearchQuery query, int pageNumber, int totalPages)
        {
            var uri = new Uri(string.Concat(_baseUri, route));
            // Lógica simplificada para o teste de conexão
            return uri;
        }
    }
}