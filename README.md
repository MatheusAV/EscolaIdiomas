# EscolaIdiomasAPI

![Badge](https://img.shields.io/badge/Status-Conclu%C3%ADdo-green)
![GitHub issues](https://img.shields.io/github/issues/SeuUsuario/EscolaIdiomasAPI)
![GitHub stars](https://img.shields.io/github/stars/SeuUsuario/EscolaIdiomasAPI)
![GitHub forks](https://img.shields.io/github/forks/SeuUsuario/EscolaIdiomasAPI)

## 📝 Descrição

A **EscolaIdiomasAPI** é uma API RESTful desenvolvida para gerenciar o cadastro de alunos, turmas e matrículas de uma escola de idiomas, permitindo operações completas de CRUD e validação de regras específicas, como capacidade máxima de turmas e unicidade de matrículas.

## 🚀 Funcionalidades

- **Gerenciamento de Alunos**: Criação, edição, exclusão e consulta de alunos, garantindo CPF único.
- **Gerenciamento de Turmas**: Cadastro de turmas com limite de alunos e controle de exclusão condicional.
- **Gerenciamento de Matrículas**: Associação de alunos a turmas com validação de duplicidade e capacidade máxima.
- **Documentação Swagger**: API documentada e acessível através do Swagger UI.

## 📋 Pré-requisitos

- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- [Node.js](https://nodejs.org/) e [Angular CLI](https://angular.io/cli) para o frontend

## 🛠️ Configuração do Projeto Backend

1. Clone o repositório:

   ```bash
   git clone https://github.com/SeuUsuario/EscolaIdiomasAPI.git
   cd EscolaIdiomasAPI
   ```

2. Instale as dependências do projeto:
   ```bash
   dotnet restore
   ```

3. Configure o banco de dados no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=SEU_SERVIDOR;Database=EscolaIdiomasDB;Trusted_Connection=True;"
   }
   ```

4. Execute as migrations para configurar o banco:
   ```bash
   dotnet ef database update --project EscolaIdiomas.Infrastructure
   ```

5. Execute a aplicação:
   ```bash
   dotnet run --project EscolaIdiomas.Api
   ```

6. Acesse a documentação Swagger em `http://localhost:5000/swagger`.

---

## 🛠️ Configuração do Projeto Frontend

1. Navegue até o diretório onde deseja criar o projeto Angular e execute:
   ```bash
   ng new escola-idiomas-frontend
   cd escola-idiomas-frontend
   ```

2. Instale as dependências necessárias:
   ```bash
   npm install bootstrap @angular/material @angular/forms
   ```

3. Configure o Bootstrap no arquivo `angular.json`:
   ```json
   "styles": [
     "src/styles.css",
     "node_modules/bootstrap/dist/css/bootstrap.min.css"
   ],
   "scripts": [
     "node_modules/bootstrap/dist/js/bootstrap.bundle.min.js"
   ]
   ```

4. Crie serviços para comunicação com a API no Angular:
   ```bash
   ng generate service services/aluno
   ng generate service services/turma
   ng generate service services/matricula
   ```

5. Configure os módulos e rotas no arquivo `app-routing.module.ts` para gerenciar os componentes de Alunos, Turmas e Matrículas.

6. Execute o frontend:
   ```bash
   ng serve
   ```

7. Acesse o frontend em `http://localhost:4200`.

---

## 📂 Estrutura do Projeto

```plaintext
EscolaIdiomasAPI/
├── EscolaIdiomas.Api/          # Camada de interface (Web API)
│   ├── Controllers/            # Controladores REST
│   ├── Program.cs              # Configuração principal da API
│   ├── Startup.cs              # Configuração de dependências e middlewares
├── EscolaIdiomas.Application/  # Camada de aplicação (Serviços e DTOs)
│   ├── Services/               # Serviços de aplicação
│   ├── Interfaces/             # Interfaces para os serviços de aplicação
│   ├── DTOs/                   # Data Transfer Objects
├── EscolaIdiomas.Domain/       # Camada de domínio (Entidades e Regras)
│   ├── Entities/               # Entidades principais do domínio
│   ├── Services/               # Serviços de domínio
│   ├── Interfaces/             # Contratos dos repositórios
│   ├── Exceptions/             # Exceções customizadas do domínio
├── EscolaIdiomas.Infrastructure/ # Camada de infraestrutura (Banco de dados)
│   ├── Data/                   # Configuração do contexto EF Core
│   ├── Repositories/           # Implementação dos repositórios
│   ├── Configurations/         # Configuração de mapeamento EF Core
│   ├── appsettings.json        # Configurações da aplicação
├── EscolaIdiomas.Tests/        # Projeto de testes automatizados
│   ├── Application/            # Testes dos serviços de aplicação
│   ├── Domain/                 # Testes dos serviços de domínio
│   ├── Infrastructure/         # Testes dos repositórios
└── README.md                   # Documentação do projeto
```

---

## 🧬 Endpoints

### 1. Alunos
- **Endpoint**: `POST /api/aluno`
- **Descrição**: Cria um novo aluno.
- **Body**:
  ```json
  {
    "Nome": "João Silva",
    "Cpf": "12345678901",
    "DataNascimento": "2000-01-01"
  }
  ```
- **Respostas**:
  - `200 OK`: Aluno criado com sucesso.
  - `400 Bad Request`: Dados inválidos.

- **Endpoint**: `GET /api/aluno/{cpf}`
- **Descrição**: Retorna as informações de um aluno pelo CPF.
- **Respostas**:
  - `200 OK`: Retorna o aluno.
  - `404 Not Found`: Aluno não encontrado.

---

### 2. Turmas
- **Endpoint**: `POST /api/turma`
- **Descrição**: Cria uma nova turma.
- **Body**:
  ```json
  {
    "Nome": "Inglês Básico",
    "CapacidadeMaxima": 5
  }
  ```
- **Respostas**:
  - `200 OK`: Turma criada com sucesso.
  - `400 Bad Request`: Dados inválidos.

---

### 3. Matrículas
- **Endpoint**: `POST /api/matricula`
- **Descrição**: Cria uma nova matrícula associando um aluno a uma turma.
- **Body**:
  ```json
  {
    "AlunoId": "id-do-aluno",
    "TurmaId": "id-da-turma"
  }
  ```
- **Respostas**:
  - `200 OK`: Matrícula criada com sucesso.
  - `400 Bad Request`: Violação de regra de negócio ou dados inválidos.

---

## 🧪 Testes

Os testes automatizados estão localizados em `EscolaIdiomas.Tests`.

Para executar os testes, use o comando:

```bash
dotnet test
```

---

## 📝 Tecnologias Utilizadas

- .NET 8.0 - Framework principal
- SQL Server - Banco de dados relacional
- Entity Framework Core - ORM para mapeamento de dados
- Swagger - Documentação interativa da API
- xUnit/Moq - Frameworks de teste para .NET
- Angular 18 - Framework para o frontend
- Bootstrap - Biblioteca de design responsivo
