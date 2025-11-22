// Arquivo: RegraDeNegocioException.cs
// Localização: CareerPath.Domain/Exceptions/RegraDeNegocioException.cs

using System;

namespace CareerPath.Domain.Exceptions // O namespace reflete o caminho
{
    // A classe herda de System.Exception
    public class RegraDeNegocioException : Exception 
    {
        // Construtor padrão que permite passar uma mensagem de erro
        public RegraDeNegocioException(string message) : base(message) 
        {
        }
        
        // Opcional: Construtor que permite passar a mensagem e a exceção interna
        public RegraDeNegocioException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}