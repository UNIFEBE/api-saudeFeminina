# Configuração do Banco de Dados PostgreSQL

## 📝 Connection String

A connection string está configurada no arquivo `src/ApiSaudeFeminina.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=saude_feminina;Username=postgres;Password=sua_senha_aqui"
  }
}
```

## ⚙️ Como Configurar

### 1. **Edite a Connection String**

Ajuste os valores conforme seu ambiente PostgreSQL:

- **Host**: endereço do servidor PostgreSQL (padrão: `localhost`)
- **Port**: porta do PostgreSQL (padrão: `5432`)
- **Database**: nome do banco de dados (sugestão: `saude_feminina`)
- **Username**: usuário do PostgreSQL (padrão: `postgres`)
- **Password**: sua senha do PostgreSQL

### 2. **Crie o Banco de Dados**

Execute no PostgreSQL (pgAdmin, DBeaver ou terminal):

```sql
CREATE DATABASE saude_feminina;
```

### 3. **Execute o Script SQL**

Execute o arquivo `src/sql/sql.sql` no banco criado para criar todas as tabelas.

```bash
psql -U postgres -d saude_feminina -f src/sql/sql.sql
```

Ou abra o arquivo no pgAdmin/DBeaver e execute.

## 🔐 Segurança

**IMPORTANTE**: Nunca comite senhas no Git!

### Opção 1: User Secrets (Recomendado para desenvolvimento)

```bash
cd src/ApiSaudeFeminina.API
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=saude_feminina;Username=postgres;Password=SUA_SENHA_REAL"
```

### Opção 2: Variáveis de Ambiente (Produção)

No servidor de produção, configure a variável:

```bash
export ConnectionStrings__DefaultConnection="Host=seu-servidor;Port=5432;Database=saude_feminina;Username=usuario;Password=senha"
```

### Opção 3: appsettings.Development.json (Local apenas)

Crie/edite o arquivo `src/ApiSaudeFeminina.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=saude_feminina;Username=postgres;Password=SUA_SENHA_REAL"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

**Depois adicione ao .gitignore:**

```
**/appsettings.Development.json
```

## 🚀 Executando a API

```bash
cd src/ApiSaudeFeminina.API
dotnet restore
dotnet build
dotnet run
```

A API estará disponível em:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger: `https://localhost:5001/swagger`

## 📚 Endpoints Disponíveis

### Usuários (`/api/usuarios`)

- **GET** `/api/usuarios` - Listar todos os usuários
- **GET** `/api/usuarios/{id}` - Buscar usuário por ID
- **GET** `/api/usuarios/email/{email}` - Buscar usuário por email
- **POST** `/api/usuarios` - Criar novo usuário
- **PUT** `/api/usuarios/{id}` - Atualizar dados do usuário
- **PUT** `/api/usuarios/{id}/senha` - Alterar senha
- **PUT** `/api/usuarios/{id}/dados-saude` - Atualizar dados de saúde
- **DELETE** `/api/usuarios/{id}` - Excluir usuário

## 🧪 Testando

### Criar Usuário

```bash
curl -X POST https://localhost:5001/api/usuarios \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Maria Silva",
    "email": "maria@email.com",
    "senha": "senha123",
    "dataNascimento": "1990-05-15",
    "telefone": "(11) 98765-4321",
    "cidade": "São Paulo",
    "estado": "SP"
  }'
```

### Buscar Todos os Usuários

```bash
curl https://localhost:5001/api/usuarios
```

### Atualizar Dados de Saúde

```bash
curl -X PUT https://localhost:5001/api/usuarios/{id}/dados-saude \
  -H "Content-Type: application/json" \
  -d '{
    "peso": 65.5,
    "altura": 165,
    "tipoSanguineo": "A+",
    "faseVida": "Fase Adulta"
  }'
```

## 📦 Pacotes NuGet Instalados

- `Npgsql.EntityFrameworkCore.PostgreSQL` v10.0.0 - Driver PostgreSQL para EF Core
- `Microsoft.EntityFrameworkCore` v10.0.11
- `Microsoft.EntityFrameworkCore.Design` v10.0.11

## ⚠️ Notas Importantes

1. **Sem Migrations**: O projeto está configurado para usar um banco existente, sem Entity Framework Migrations.

2. **Tabelas já criadas**: Execute o script SQL antes de rodar a API.

3. **Segurança da Senha**: O código atual usa SHA256 como exemplo. **Em produção, substitua por BCrypt ou Argon2**!

4. **Validações**: A entidade Usuario já possui validações básicas (tamanho de campos, formato de email, etc).
