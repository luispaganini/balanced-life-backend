# Projeto C# com Arquitetura Limpa (Clean Architecture)

Este projeto é uma aplicação C# que utiliza a arquitetura limpa (Clean Architecture) com as seguintes camadas:

- **API**: Camada responsável pela interface com o cliente (endpoints, controllers, etc.).
- **Application**: Camada de regras de negócio e lógica de aplicação.
- **Domain**: Camada que contém as entidades e interfaces do domínio.
- **Infra.IoC**: Camada para configuração de Injeção de Dependência.
- **Infra.Data**: Camada responsável pela implementação de acesso a dados.

## Requisitos

Antes de começar, certifique-se de ter o seguinte instalado em seu sistema:

- [.NET SDK](https://dotnet.microsoft.com/download) (versão 6.0 ou superior)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (ou [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions))
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (opcional, para gerenciamento do banco de dados)

## Instalação

1. Clone este repositório para sua máquina local:

   ```bash
   git clone https://github.com/seu-usuario/seu-projeto.git
   ```

2. Acesse o diretório do projeto:

   ```bash
   cd seu-projeto
   ```

3. Restaure as dependências do projeto:

   ```bash
   dotnet restore
   ```

## Configuração do Banco de Dados

1. **Crie o Banco de Dados SQL Server:**

   - Abra o [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) e conecte-se ao seu servidor SQL Server.
   - No painel "Object Explorer", clique com o botão direito em "Databases" e selecione "New Database...".
   - Dê um nome ao banco de dados e clique em "OK".

2. **Configuração da String de Conexão:**

   - No arquivo `appsettings.json` da camada **API**, configure a string de conexão com o banco de dados. Substitua `YOUR_SERVER`, `YOUR_DATABASE`, `YOUR_USERNAME` e `YOUR_PASSWORD` pelos valores correspondentes:

     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;"
       }
     }
     ```

3. **Atualize o Banco de Dados:**

   - Insira as tabelas, colunas e informações do banco de dados referente com o arquivo ".sql".

## Executar a Aplicação

Para rodar a aplicação, utilize o seguinte comando a partir do diretório da camada **API**:

```bash
dotnet run
```

A aplicação será iniciada e estará disponível no endereço configurado em `launchSettings.json` ou `appsettings.json`.

## Estrutura de Pastas

```bash
├── API                # Camada de API (Controllers, Configurações, etc.)
│   ├── Controllers    # Controllers da API
│   └── Startup.cs     # Configurações da aplicação
├── Application        # Camada de Aplicação (Serviços, Regras de Negócio, etc.)
│   ├── Services       # Serviços da aplicação
│   └── DTOs           # Data Transfer Objects
├── Domain             # Camada de Domínio (Entidades, Interfaces, etc.)
│   ├── Entities       # Entidades de domínio
│   └── Interfaces     # Interfaces de repositórios e serviços
├── Infra.IoC          # Camada de Injeção de Dependência (Configuração de IoC)
│   └── DependencyInjection.cs # Configurações de injeção de dependência
├── Infra.Data         # Camada de Acesso a Dados (Contexto do EF, Repositórios, etc.)
│   ├── DataContext.cs # Contexto do banco de dados
│   └── Repositories   # Implementação dos repositórios
├── appsettings.json   # Configurações da aplicação (incluindo strings de conexão)
├── .gitignore         # Arquivos e pastas a serem ignorados pelo Git
└── seu-projeto.sln    # Solução do projeto
```

## Scripts Disponíveis

- `run`: Executa a aplicação.

## Contribuição

Sinta-se à vontade para abrir uma issue ou pull request para sugestões de melhorias ou correções.

---

Siga este guia para configurar e executar sua aplicação com Clean Architecture em C#. Se precisar de ajuda adicional, consulte a documentação oficial do .NET e dos pacotes utilizados.
