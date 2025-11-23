# 💼 CareerPath - Plataforma de Match de Habilidades Futuras

## 💡 Visão Geral do Projeto

O **CareerPath** é uma Web API desenvolvida em **ASP.NET Core 8** que atua como um motor de recomendação para o mercado de trabalho. A solução cruza o perfil do profissional (profissão atual e experiência) com um catálogo de **Habilidades Futuras** e **Cursos** de alta demanda, sugerindo caminhos de *upskilling* personalizados.

### 🎯 Objetivos
* Mitigar a lacuna de habilidades (*skills gap*) no mercado de trabalho.
* Fornecer uma API robusta para integração com Front-ends Web e Mobile.
* Demonstrar uma arquitetura distribuída, escalável e aderente às melhores práticas de mercado.

---

## 🏗️ Arquitetura e Design

O projeto segue rigorosamente os princípios da **Clean Architecture** e **Domain-Driven Design (DDD)** para garantir a separação de responsabilidades, testabilidade e manutenção.

### Estrutura de Camadas
* **`CareerPath.Domain`**: O núcleo do sistema. Contém as Entidades (`Profissional`, `Habilidade`, `Curso`), Invariantes e Regras de Negócio. Não possui dependências de outras camadas.
* **`CareerPath.Application`**: Camada de orquestração. Contém os Serviços (`MatchService`), DTOs, Interfaces e Lógica de Aplicação.
* **`CareerPath.Infrastructure`**: Camada de dados e implementação. Gerencia o acesso ao banco via **Entity Framework Core**, Mapeamentos e Repositórios.
* **`CareerPath.WebAPI`**: Camada de entrada. Contém os Controllers RESTful, Configuração de Injeção de Dependência (DI) e Swagger.

---

## ⚙️ Tecnologias e Ferramentas

* **Framework:** .NET 8 SDK
* **Banco de Dados:** PostgreSQL 16
* **ORM:** Entity Framework Core (Npgsql)
* **Documentação:** Swagger / OpenAPI
* **Padrões:** Repository Pattern, HATEOAS, ProblemDetails (RFC 7807)

---

## 🚀 Como Rodar o Projeto

### Pré-requisitos
1. Ter o **.NET 8 SDK** instalado.
2. Ter acesso a uma instância **PostgreSQL** (Local ou em VM/Cloud).

### Configuração da Conexão
O projeto está configurado para aplicar migrações automaticamente ao iniciar. Você deve configurar a string de conexão no arquivo `appsettings.json` dentro da pasta `CareerPath.WebAPI`.

Exemplo de configuração (`appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=SEU_IP_OU_LOCALHOST;Port=5432;Database=CareerPathDB;Username=postgres;Password=SUA_SENHA;"
  }
}

Execução (Passo a Passo)
Abra o terminal na pasta raiz da solução e execute:

Bash

# 1. Restaurar dependências e compilar
dotnet restore

# 2. Navegar para a pasta da API
cd CareerPath.WebAPI

# 3. Iniciar a aplicação
# (Este comando criará o Banco de Dados automaticamente via Migrations)
dotnet run

A API estará disponível em: http://localhost:5000 (ou porta configurada). A documentação Swagger estará em: http://localhost:5000/swagger


🌐 Rotas e Endpoints PrincipaisA API implementa o padrão HATEOAS no endpoint de busca, fornecendo links de navegação para paginação.
Método,Rota,Descrição
POST,/api/habilidades,CRUD: Cria uma nova Habilidade no catálogo.
GET,/api/habilidades/{id},CRUD: Busca detalhes de uma Habilidade.
GET,/api/habilidades/search,"BUSCA AVANÇADA: Retorna lista paginada com suporte a filtros (Nome, NivelDemanda) e ordenação."
POST,/api/profissionais,Cria um perfil profissional para teste de match.

🔑 Qualidade e Compliance
Tratamento de Erros: Implementação global de tratamento de exceções utilizando o padrão ProblemDetails, garantindo respostas de erro consistentes e informativas.

Validação de Domínio: As entidades de domínio protegem suas invariantes (ex: valores negativos, campos obrigatórios) diretamente no construtor, garantindo a integridade dos dados.






Feito por:

Nomes: Marcos Vinicius | Jonas Ikimio | Daniel Kendi
RMs: 560475 | 560560 | 553043
