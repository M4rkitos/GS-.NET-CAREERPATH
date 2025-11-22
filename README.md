# 💼 CareerPath - Plataforma de Match de Habilidades Futuras

## 💡 Visão Geral do Projeto

O **CareerPath** é uma Web API desenvolvida em **ASP.NET Core** que atua como um motor de recomendação. A solução visa mitigar o desafio da **Adaptação de Carreiras** no mercado de trabalho, cruzando o perfil do profissional (profissão e experiência) com um catálogo de **Habilidades Futuras** e **Cursos** de alta demanda. O objetivo é fornecer um caminho claro para o *upskilling* e aumentar a relevância profissional dos usuários.

---

## 🏗️ Decisões Arquiteturais

O projeto foi construído utilizando a **Clean Architecture** e o **Domain-Driven Design (DDD)** para garantir alta manutenibilidade e testabilidade.

* **Domain (`CareerPath.Domain`):** Contém as **Entidades** (`Profissional`, `Habilidade`, `Curso`), suas **Invariantes** e as **Exceções de Regra de Negócio**.
* **Application (`CareerPath.Application`):** Contém os **DTOs**, as **Interfaces de Repositório** e os **Serviços de Aplicação** (`IMatchService`).
* **Infrastructure (`CareerPath.Infrastructure`):** Implementa o **EF Core** (`ApplicationDbContext`), as **Migrations** e os **Repositórios Concretos**.
* **WebAPI (`CareerPath.WebAPI`):** Camada de Apresentação (**Controllers** e **HATEOAS**).

### Qualidade e Conformidade
* **Invariantes de Domínio:** As entidades garantem a validade de seu estado através de validações no construtor.
* **Tratamento de Erros (ProblemDetails):** Exceções de Domínio são capturadas por um filtro global e convertidas em respostas padronizadas **HTTP 400 Bad Request** no formato **ProblemDetails** (RFC 7807).

---

## ⚙️ Pré-requisitos e Execução

### Pré-requisitos e Variáveis de Ambiente
É necessário ter o **.NET 8 SDK** instalado. A String de Conexão com o banco de dados deve ser configurada no arquivo `CareerPath.WebAPI/appsettings.json` na chave `DefaultConnection`.

### Instruções de Execução
As instruções devem ser executadas via terminal na **raiz da solução**.

1.  **Restaurar Pacotes:**
    ```bash
    dotnet restore
    ```

2.  **Aplicar Migrations (Criação do Banco de Dados):**
    * Este comando cria o banco e as tabelas, cumprindo o requisito de **Migrations aplicadas**:
    ```bash
    dotnet ef database update --project CareerPath.Infrastructure --startup-project CareerPath.WebAPI
    ```

3.  **Executar a API Web:**
    ```bash
    dotnet run --project CareerPath.WebAPI
    ```

---

## 🌐 Rotas e Exemplos de Uso

A documentação interativa completa (com exemplos) está disponível no **Swagger UI** (acessível em `/swagger` após a execução).

| Método | Rota | Descrição |
| :--- | :--- | :--- |
| **POST** | `/api/habilidades` | **CRUD:** Cria uma nova Habilidade. |
| **GET** | `/api/habilidades/{id}` | **CRUD:** Busca Habilidade por ID. |
| **GET** | `/api/habilidades/search` | **BUSCA AVANÇADA** com Paginação, Filtros, Ordenação e HATEOAS. |

### Exemplo de Uso (Busca e HATEOAS)
O endpoint `/search` implementa filtros (`NomeContem`, `NivelDemandaMin`), Paginação e Ordenação.

**Exemplo de Requisição (via cURL):**
```bash
curl -X 'GET' 'https://localhost:[PORTA]/api/habilidades/search?Pagina=2&TamanhoPagina=10&NivelDemandaMin=7&OrdenarPor=DemandaDesc'


Exemplo da Resposta JSON (com HATEOAS): A resposta incluirá links de navegação autodescritivos:

JSON

{
    // ... dados de metadados
    "dados": [...],
    "proximaPaginaUri": "https://localhost:[PORTA]/api/habilidades/search?Pagina=3&...",
    "paginaAnteriorUri": "https://localhost:[PORTA]/api/habilidades/search?Pagina=1&..."
}



Feito por:

Nomes: Marcos Vinicius | Jonas Ikimio | Daniel Kendi
RMs: 560475 | 560560 | 553043
