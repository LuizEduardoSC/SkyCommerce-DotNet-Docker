# 🛒 SkyCommerce API

Bem-vindo ao repositório do **SkyCommerce API**, um backend de e-commerce robusto, escalável e focado em boas práticas de engenharia de software desenvolvido em **C# (.NET 8)**.

Este projeto foi construído para demonstrar proficiência em padrões arquiteturais avançados e desenvolvimento de APIs RESTful prontas para ambiente de produção.

## 🏗️ Arquitetura e Padrões

O sistema foi desenhado utilizando os princípios da **Clean Architecture** (Arquitetura Limpa), separando as responsabilidades em camadas independentes. Além disso, foram adotados conceitos de **Domain-Driven Design (DDD)** e **CQRS (Command Query Responsibility Segregation)**.

### Estrutura das Camadas:
- **`SkyCommerce.Domain`**: O coração do negócio. Contém as entidades (`Product`, `Category`, `Order`, etc.) as regras core da aplicação e interfaces de abstração (ex: `IRepository`). Não possui nenhuma dependência externa, garantindo alto desacoplamento.
- **`SkyCommerce.Application`**: A orquestradora da lógica. Contém os DTOs (Data Transfer Objects), configurações do AutoMapper e a estrutura base do MediatR.
- **`SkyCommerce.Infrastructure`**: Detalhes e implementações técnicas. Responsável pelo acesso a dados usando o **Entity Framework Core**, integração com o **PostgreSQL** e as implementações dos *Repositórios Genéricos*.
- **`SkyCommerce.Api`**: A porta de entrada HTTP (Presentation Layer). Contém os Controllers, as rotas e toda a configuração de serialização, middlewares e a interface do **Swagger**.

## 🚀 Funcionalidades Atuais

- **Gerenciamento de Categorias**: Criação (via DTOs limpos) e Listagem isolada.
- **Gerenciamento de Produtos**: Criação vinculada a Categorias com validação relacional e mapeamento avançado usando AutoMapper.
- **Isolamento de Domínio**: Configuração rígida de mapeamento, assegurando que o usuário final nunca recebe ou insere acidentalmente as entidades ricas em regras do banco de dados (ex: resolvendo o problema de "Over-Posting" ou falhas de validação cíclica).

## 🛠️ Tecnologias Utilizadas

- **[.NET 8 SDK]** - Framework robusto e de altíssimo desempenho.
- **[Entity Framework Core 8]** - ORM para tradução das entidades C# em tabelas relacionais.
- **[PostgreSQL]** - Banco de Dados relacional, estável e Open-Source.
- **[AutoMapper]** - Para injeção rápida e performática entre Objetos (Domain ↔ DTO).
- **[MediatR]** - Implementação de design Messaging/CQRS na camada de Aplicação.
- **[Docker & Docker Compose]** - Para uma arquitetura conteinerizada no setup.

---

## 🚦 Como Rodar e Instalar na Máquina

### 1. Pré-requisitos
Antes de começar, garanta que você tem o seguinte instalado na sua máquina:
- [**SDK do .NET 8**](https://dotnet.microsoft.com/download)
- [**Docker Desktop**](https://www.docker.com/products/docker-desktop) (para o contêiner do banco)
- Ferramenta Global do EF Core instalada. Se ainda não tiver, rode no terminal:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### 2. Passo a Passo de Execução

**Passo 1: Suba o Banco de Dados (PostgreSQL)**
Abra o terminal na raiz do projeto (onde fica o arquivo `docker-compose.yml`) e suba a infraestrutura via Docker:
```bash
docker-compose up -d
```
> Isso rodará o banco `skycommerce_db` silenciosamente em background na porta `5432`.

**Passo 2: Execute as Migrations (Update Database)**
Com o banco rodando, precisamos criar as tabelas baseadas no nosso código (`EF Core`):
```bash
dotnet ef database update --project src/SkyCommerce.Infrastructure --startup-project src/SkyCommerce.Api
```

**Passo 3: Rode a API**
Ainda no terminal da pasta raiz, inicialize a API:
```bash
dotnet run --project src/SkyCommerce.Api/SkyCommerce.Api.csproj
```

**Passo 4: Acesse a Interface Interativa (Swagger)**
Se o terminal não abrir o navegador, acesse o endereço fornecido nos logs (geralmente `http://localhost:5267/swagger` ou similar) para abrir a elegante documentação Swagger da API. Onde você já poderá testar a criação de `Categorias` e `Produtos`.

---
*Desenvolvido com ☕ e focado em manter padrões limpos, testáveis e prontos para escalar.*
