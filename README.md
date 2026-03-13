# LanchesMac
 
 ## Quick Start (Docker + SQL Server)
```bash
git clone <URL_DO_SEU_REPOSITORIO>
cd LanchesMac
cd docker
copy .env.example .env
# edite o arquivo .env e defina MSSQL_SA_PASSWORD
docker compose up -d
cd ..\LanchesMac
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet run
```
 
Acesse:
 - `https://localhost:7139`
 - `http://localhost:5139`
 
## Proposta do projeto
O **LanchesMac** é uma aplicação web (ASP.NET Core MVC) de uma lanchonete, com foco em demonstrar um fluxo completo de:
 
- **Catálogo de lanches** (listagem, detalhes e busca)
- **Filtro por categoria**
- **Carrinho de compras** (adicionar/remover itens)
- **Checkout / Pedido**
- **Autenticação** com **ASP.NET Core Identity** (login/registro) para ações protegidas
 
O objetivo é servir como base de estudo/portfólio para conceitos de MVC, repositórios, Entity Framework Core, SQL Server, sessões e Identity.
 
## Stack / Tecnologias
- **.NET 6** (`net6.0`)
- **ASP.NET Core MVC**
- **Entity Framework Core 6 (SqlServer)**
- **ASP.NET Core Identity**
- **SQL Server** (local via Docker Compose ou instância local)
 
## Estrutura do repositório
- `LanchesMac.sln`: Solution
- `LanchesMac/`: projeto web
  - `Program.cs` / `Startup.cs`: bootstrap e pipeline
  - `Context/AppDbContext.cs`: `DbContext` (inclui tabelas do domínio e Identity)
  - `Migrations/`: migrations do EF Core (inclui inserts SQL para popular categorias e lanches)
  - `Controllers/`, `Models/`, `Views/`: MVC
  - `appsettings.json`: conexão padrão (Windows/SQLExpress)
  - `appsettings.Development.json`: conexão para SQL Server na porta `1433` (Docker)
- `docker/`: arquivos para subir o SQL Server via Docker Compose
 
## Pré-requisitos
- **.NET SDK 6.0** instalado
- **Git**
- **SQL Server** (uma das opções)
  - **Opção A (recomendado): Docker + Docker Compose**
  - **Opção B: SQL Server local / SQLExpress**
 
> No Windows, você pode usar Visual Studio 2022 ou apenas o `dotnet` via terminal.
 
## Como clonar o projeto
```bash
git clone <URL_DO_SEU_REPOSITORIO>
cd LanchesMac
```
 
## Executar via Visual Studio
 - Abra o arquivo `LanchesMac.sln`.
 - Selecione o projeto `LanchesMac` como **Startup Project**.
 - Garanta que o banco está configurado (Docker ou SQL Server local).
 - Execute (F5).

## Banco de dados (SQL Server com Docker Compose)
Foi adicionada a pasta `docker/` com um `docker-compose.yml` para subir o SQL Server localmente.
 
### 1) Configurar senha do SA
Entre na pasta `docker/` e crie um arquivo `.env` baseado no exemplo:
 
```bash
cd docker
copy .env.example .env
```
 
Edite o arquivo `.env` e defina `MSSQL_SA_PASSWORD`.
 
**Regras comuns do SQL Server**: mínimo 8 caracteres, com maiúscula, minúscula, número e caractere especial.
 
### 2) Subir o container
Execute dentro da pasta `docker/`:
 
```bash
docker compose up -d
```
 
O SQL Server ficará exposto em:
- `localhost,1433`
 
Para ver logs:
```bash
docker compose logs -f
```
 
Para parar:
```bash
docker compose down
```
 
## Configuração da Connection String
O projeto usa a connection string `DefaultConnection`.
 
- Em `LanchesMac/appsettings.Development.json` já existe uma connection string apontando para:
  - `Server=localhost,1433;Database=LanchesDatabase;User Id=sa;Password=<SUA_SENHA_FORTE_AQUI>;TrustServerCertificate=True;`
 
Ajuste a senha para a **mesma senha** definida em `docker/.env`.
 
> Alternativa: você pode colocar a connection string em **User Secrets** ou via variável de ambiente, mas atualmente o projeto lê do `appsettings*.json`.

## Banco de dados (sem Docker / SQL Server local)
 Se você já possui um SQL Server local (ex.: **SQLExpress**), você pode usar a connection string do `LanchesMac/appsettings.json` (Integrated Security).
 
 - Ajuste o servidor e instância conforme sua máquina.
 - Garanta que o banco `LanchesDatabase` exista (ou rode as migrations para criar).

## Criar/Atualizar o banco (Migrations)
Este projeto possui migrations em `LanchesMac/Migrations/`.
 
Entre na pasta do projeto (onde está o `LanchesMac.csproj`) e execute:
 
```bash
cd LanchesMac
dotnet restore
dotnet ef database update
```
 
Isso irá:
- Criar o banco `LanchesDatabase` (se não existir)
- Criar as tabelas do domínio e do Identity
- Popular dados iniciais via migrations (ex.: categorias e lanches) quando aplicável
 
> Se o comando `dotnet ef` não estiver disponível, instale a ferramenta:
```bash
dotnet tool install --global dotnet-ef
```
 
## Executar a aplicação
Entre na pasta do projeto e execute:
 
```bash
cd LanchesMac
dotnet run
```
 
URLs (conforme `Properties/launchSettings.json`):
- `https://localhost:7139`
- `http://localhost:5139`
 
## Reset do banco (desenvolvimento)
 - Se estiver usando **Docker**, para remover o container:
   - `cd docker` e depois `docker compose down`
 - Para remover também o volume (apaga os dados persistidos):
   - `cd docker` e depois `docker compose down -v`
 
 Depois disso, suba novamente o container e rode `dotnet ef database update`.

## Funcionalidades principais (rotas)
- **Home**: `/` (lista de lanches preferidos)
- **Listagem de lanches**: `/Lanche/List` e `/Lanche/List?categoria=Normal`
- **Detalhes**: `/Lanche/Details?lancheId=1`
- **Busca**: `/Lanche/Search?searchString=cheese`
- **Carrinho**: `/CarrinhoCompra/Index`
- **Checkout** (requer login): `/Pedido/Checkout`
- **Login/Registro**:
  - `/Account/Login`
  - `/Account/Register`
 
## Observações importantes
- **Ações protegidas**: adicionar/remover do carrinho e checkout exigem usuário autenticado (`[Authorize]`).
- **Sessão**: carrinho usa sessão (`services.AddSession()` e `app.UseSession()`).
 
## Troubleshooting
- **Erro de login no SQL Server (sa)**
  - Confirme que a senha do `docker/.env` é a mesma do `appsettings.Development.json`.
  - Confirme que o container está rodando e a porta `1433` está livre.
 
- **Certificado/SSL**
  - A connection string já usa `TrustServerCertificate=True;` para desenvolvimento.
 
- **`dotnet ef` não encontrado**
  - Instale `dotnet-ef` globalmente:
    - `dotnet tool install --global dotnet-ef`
 
- **Banco não cria / migrations não aplicam**
  - Rode novamente:
    - `cd LanchesMac` e depois `dotnet ef database update`
