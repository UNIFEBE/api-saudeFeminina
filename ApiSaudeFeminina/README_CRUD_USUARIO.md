# CRUD de Usuário - API Saúde Feminina

## ✅ Implementação Completa

O CRUD de usuário foi implementado seguindo a estrutura da tabela `usuario` do PostgreSQL conforme o script `src/sql/sql.sql`.

## 📁 Arquivos Criados

### **Domain (Entidades e Regras de Negócio)**
- ✅ `src/ApiSaudeFeminina.Domain/Entities/Usuario.cs` - Entidade com validações
- ✅ `src/ApiSaudeFeminina.Domain/Interfaces/IUsuarioRepository.cs` - Interface do repositório

### **Application (Lógica de Aplicação)**
- ✅ `src/ApiSaudeFeminina.Application/DTOs/UsuarioDto.cs` - 5 DTOs (Create, Update, UpdateSenha, UpdateDadosSaude, Response)
- ✅ `src/ApiSaudeFeminina.Application/Interfaces/IUsuarioService.cs` - Interface do serviço
- ✅ `src/ApiSaudeFeminina.Application/Services/UsuarioService.cs` - Lógica de negócio

### **Infrastructure (Persistência)**
- ✅ `src/ApiSaudeFeminina.Infrastructure/Repositories/UsuarioRepository.cs` - Acesso ao banco
- ✅ `src/ApiSaudeFeminina.Infrastructure/Configurations/UsuarioConfiguration.cs` - Mapeamento EF Core

### **API (Endpoints)**
- ✅ `src/ApiSaudeFeminina.API/Controllers/UsuariosController.cs` - 8 endpoints REST

### **Configuração**
- ✅ Atualizado `DependencyInjection.cs` (Application e Infrastructure)
- ✅ Atualizado `AppDbContext.cs` com DbSet de Usuários
- ✅ Configurado PostgreSQL no lugar de SQL Server
- ✅ Connection string configurada para PostgreSQL

### **Documentação e Testes**
- ✅ `CONFIGURACAO_BANCO.md` - Instruções completas de configuração
- ✅ `Usuarios.http` - Arquivo de testes HTTP com exemplos
- ✅ `.gitignore` - Proteção de senhas e arquivos sensíveis

## 🎯 Endpoints Disponíveis

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/usuarios` | Lista todos os usuários |
| GET | `/api/usuarios/{id}` | Busca usuário por ID |
| GET | `/api/usuarios/email/{email}` | Busca usuário por email |
| POST | `/api/usuarios` | Cria novo usuário |
| PUT | `/api/usuarios/{id}` | Atualiza dados básicos |
| PUT | `/api/usuarios/{id}/senha` | Altera senha do usuário |
| PUT | `/api/usuarios/{id}/dados-saude` | Atualiza dados de saúde |
| DELETE | `/api/usuarios/{id}` | Exclui usuário |

## 🗄️ Estrutura da Entidade Usuario

```csharp
public class Usuario
{
    // Identificação
    public Guid Id { get; }
    
    // Dados Pessoais
    public string Nome { get; }           // Max 150 chars
    public string Email { get; }          // Max 150 chars, único
    public string SenhaHash { get; }      // Max 255 chars
    public DateTime? DataNascimento { get; }
    public string? Telefone { get; }      // Max 20 chars
    public string? Cidade { get; }        // Max 100 chars
    public string? Estado { get; }        // 2 chars (UF)
    
    // Dados de Saúde
    public decimal? Peso { get; }         // kg (5,2)
    public decimal? Altura { get; }       // cm (5,2)
    public string? TipoSanguineo { get; } // Max 3 chars
    public string? FaseVida { get; }      // Max 50 chars
    
    // Auditoria
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; }
}
```

## 🔧 Configuração Rápida

### 1. **Configure a Connection String**

Edite `src/ApiSaudeFeminina.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=saude_feminina;Username=postgres;Password=SUA_SENHA"
  }
}
```

### 2. **Crie o Banco de Dados**

```sql
CREATE DATABASE saude_feminina;
```

### 3. **Execute o Script SQL**

Execute o arquivo `src/sql/sql.sql` no banco criado.

```bash
psql -U postgres -d saude_feminina -f src/sql/sql.sql
```

### 4. **Instale as Dependências**

```bash
cd src/ApiSaudeFeminina.Infrastructure
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 10.0.0
```

### 5. **Execute a API**

```bash
cd src/ApiSaudeFeminina.API
dotnet restore
dotnet build
dotnet run
```

### 6. **Acesse o Swagger**

Abra no navegador: `https://localhost:5001/swagger`

## 📝 Exemplo de Uso

### Criar Usuário

```bash
POST /api/usuarios
Content-Type: application/json

{
  "nome": "Maria Silva",
  "email": "maria@email.com",
  "senha": "Senha@123",
  "dataNascimento": "1990-05-15",
  "telefone": "(11) 98765-4321",
  "cidade": "São Paulo",
  "estado": "SP"
}
```

### Resposta

```json
{
  "id": "123e4567-e89b-12d3-a456-426614174000",
  "nome": "Maria Silva",
  "email": "maria@email.com",
  "dataNascimento": "1990-05-15",
  "telefone": "(11) 98765-4321",
  "cidade": "São Paulo",
  "estado": "SP",
  "peso": null,
  "altura": null,
  "tipoSanguineo": null,
  "faseVida": null,
  "createdAt": "2026-08-26T10:30:00Z",
  "updatedAt": null
}
```

### Atualizar Dados de Saúde

```bash
PUT /api/usuarios/{id}/dados-saude
Content-Type: application/json

{
  "peso": 65.5,
  "altura": 165,
  "tipoSanguineo": "A+",
  "faseVida": "Fase Adulta"
}
```

## 🔐 Segurança

### ⚠️ IMPORTANTE - Senha

O código atual usa **SHA256** apenas como exemplo. **Para produção, você DEVE substituir por:**

1. **BCrypt.Net-Next** (Recomendado)
```bash
dotnet add package BCrypt.Net-Next
```

2. **Argon2** (Mais seguro)
```bash
dotnet add package Konscious.Security.Cryptography.Argon2
```

### Protegendo Credenciais

**User Secrets** (desenvolvimento):
```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "sua-connection-string-aqui"
```

**Variáveis de Ambiente** (produção):
```bash
export ConnectionStrings__DefaultConnection="sua-connection-string"
```

## ✨ Funcionalidades

### Validações Implementadas
- ✅ Nome obrigatório (max 150 chars)
- ✅ Email obrigatório, único e válido (max 150 chars)
- ✅ Email convertido para lowercase automaticamente
- ✅ Estado deve ter 2 caracteres (UF)
- ✅ Estado convertido para uppercase automaticamente
- ✅ Peso e altura não podem ser negativos
- ✅ Tipo sanguíneo max 3 caracteres
- ✅ Fase da vida max 50 caracteres
- ✅ Verificação de email duplicado
- ✅ Verificação de senha atual ao alterar senha

### Operações Especiais
- ✅ Busca por email
- ✅ Atualização separada de senha
- ✅ Atualização separada de dados de saúde
- ✅ Timestamps automáticos (criado_em, atualizado_em)
- ✅ Soft delete preparado (pode adicionar IsDeleted no futuro)

## 🧪 Testando

### Com arquivo HTTP (VS Code / Visual Studio)

Use o arquivo `src/ApiSaudeFeminina.API/Usuarios.http` que contém todos os testes prontos.

### Com cURL

```bash
# Criar usuário
curl -X POST https://localhost:5001/api/usuarios \
  -H "Content-Type: application/json" \
  -d '{"nome":"Maria","email":"maria@email.com","senha":"Senha@123"}'

# Listar usuários
curl https://localhost:5001/api/usuarios

# Buscar por ID
curl https://localhost:5001/api/usuarios/{id}
```

### Com Swagger

Acesse `https://localhost:5001/swagger` para interface interativa completa.

## 📊 Mapeamento Banco x Entidade

| Coluna BD (snake_case) | Propriedade C# (PascalCase) | Tipo BD | Tipo C# |
|------------------------|----------------------------|---------|---------|
| id | Id | UUID | Guid |
| nome | Nome | VARCHAR(150) | string |
| email | Email | VARCHAR(150) | string |
| senha_hash | SenhaHash | VARCHAR(255) | string |
| data_nascimento | DataNascimento | DATE | DateTime? |
| telefone | Telefone | VARCHAR(20) | string? |
| cidade | Cidade | VARCHAR(100) | string? |
| estado | Estado | CHAR(2) | string? |
| peso | Peso | DECIMAL(5,2) | decimal? |
| altura | Altura | DECIMAL(5,2) | decimal? |
| tipo_sanguineo | TipoSanguineo | VARCHAR(3) | string? |
| fase_vida | FaseVida | VARCHAR(50) | string? |
| criado_em | CreatedAt | TIMESTAMP | DateTime |
| atualizado_em | UpdatedAt | TIMESTAMP | DateTime? |

## 🚀 Próximos Passos Sugeridos

1. **Implementar autenticação JWT**
2. **Adicionar BCrypt para hash de senhas**
3. **Criar endpoints para as outras tabelas** (ciclo_config, registro_diario, consulta, etc)
4. **Adicionar paginação no GetAll**
5. **Implementar filtros de busca**
6. **Adicionar logs estruturados (Serilog)**
7. **Implementar FluentValidation para DTOs**
8. **Adicionar testes unitários e de integração**
9. **Documentar com XML comments no Swagger**
10. **Implementar CORS para front-end**

## 📞 Suporte

Dúvidas ou problemas? Consulte:
- `CONFIGURACAO_BANCO.md` - Instruções de configuração detalhadas
- `Usuarios.http` - Exemplos práticos de uso
- Swagger UI - Documentação interativa
