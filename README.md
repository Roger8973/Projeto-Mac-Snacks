# 🍔 Mac Snacks (LanchesMac)

Aplicação web em **ASP.NET Core MVC (.NET 6)** que simula uma loja online de lanches: catálogo por categoria, carrinho de compras, checkout de pedidos e autenticação com perfis de acesso (membro e administrador).

---

## ✨ Funcionalidades

- **Página inicial** com carrossel e vitrine dos lanches marcados como preferidos.
- **Catálogo de lanches** com listagem geral, filtro por categoria (`/Snack/List/{categoria}`) e página de detalhes.
- **Carrinho de compras** persistido no banco e vinculado à sessão do usuário (`CartId` em sessão): adicionar, remover e ver o resumo com total.
- **Checkout** com formulário de entrega validado (nome, endereço, CEP etc.), cálculo do total de itens e do valor e gravação do pedido com seus detalhes (`Order` / `OrderDetail`).
- **Autenticação e cadastro** com ASP.NET Core Identity — é preciso estar logado para adicionar itens ao carrinho.
- **Perfis de acesso** (`Member` e `Admin`) criados automaticamente na inicialização, com política de autorização `Admin`.
- **Área administrativa** (`/Admin`) restrita ao perfil Admin.
- **Página de contato** e Tag Helper customizado de e-mail.
- **View Components**: menu de categorias e resumo do carrinho no layout.

## 🛠️ Tecnologias

| Camada | Tecnologia |
|---|---|
| Framework | ASP.NET Core MVC — .NET 6 |
| ORM | Entity Framework Core 6 (Code First + Migrations) |
| Banco de dados | SQL Server (Express/LocalDB) |
| Autenticação | ASP.NET Core Identity |
| Front-end | Razor Views, Bootstrap 5, jQuery, jQuery Validation |

## 🧱 Arquitetura

O projeto segue o padrão **MVC** com **Repository Pattern** e injeção de dependência nativa:

```
LanchesMac/
├── Areas/Admin/        # Área administrativa (restrita ao perfil Admin)
├── Components/         # View Components (CategoryMenu, ShoppingCartSummary)
├── Context/            # AppDbContext (IdentityDbContext + DbSets)
├── Controllers/        # Home, Snack, CartPurchase, Order, Account, Contact
├── Migrations/         # Migrations do EF Core (inclui carga inicial de dados)
├── Models/             # Snack, Category, CartPurchase, CartPurchaseItem, Order, OrderDetail
├── Repositories/       # Repositórios e interfaces (Snacks, Categories, Orders)
├── Services/           # Seed de perfis e usuários iniciais
├── TagHelpers/         # EmailTagHelper
├── ViewModels/         # ViewModels das telas
├── Views/              # Razor Views
└── wwwroot/            # Arquivos estáticos (css, js, imagens, libs)
```

**Modelo de dados**

- `Category` 1 — N `Snack`
- `CartPurchaseItem` → `Snack` (itens do carrinho, agrupados por `CartId`)
- `Order` 1 — N `OrderDetail` → `Snack`
- Tabelas do Identity (`AspNetUsers`, `AspNetRoles`, …)

## 🚀 Como executar

### Pré-requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- SQL Server (Express ou LocalDB)
- Ferramenta do EF Core: `dotnet tool install --global dotnet-ef`

### Passo a passo

1. **Clone o repositório**

   ```bash
   git clone https://github.com/Roger8973/Projeto-Mac-Snacks.git
   cd Projeto-Mac-Snacks/LanchesMac
   ```

2. **Configure a connection string** em `appsettings.json` apontando para a sua instância do SQL Server. Exemplo com LocalDB:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SnackDataBase;Trusted_Connection=True;"
   }
   ```

3. **Crie o banco e aplique as migrations** (isso também popula categorias e lanches):

   ```bash
   dotnet ef database update
   ```

4. **Execute a aplicação**

   ```bash
   dotnet run
   ```

   Acesse `https://localhost:7198` (ou `http://localhost:5198`).

### Dados iniciais

As migrations carregam as categorias **Normal** e **Natural** e alguns lanches de exemplo (Cheese Salada, Misto Quente, Cheese Burger, Lanche Natural Peito de Peru…).

Na inicialização, a aplicação cria os perfis e os usuários abaixo, caso ainda não existam:

| Usuário | Senha | Perfil |
|---|---|---|
| `usuario@localhost` | `Numsey#2022` | Member |
| `admin@localhost` | `Numsey#2022` | Admin |

> ⚠️ Essas credenciais são apenas para ambiente de desenvolvimento.

## 🗺️ Principais rotas

| Rota | Descrição |
|---|---|
| `/` | Página inicial |
| `/Snack/List` | Todos os lanches |
| `/Snack/List/{categoria}` | Lanches de uma categoria |
| `/Snack/Details?snackId={id}` | Detalhes de um lanche |
| `/CartPurchase` | Carrinho de compras |
| `/Order/Checkout` | Finalização do pedido |
| `/Account/Login` · `/Account/Register` | Login e cadastro |
| `/Admin` | Área administrativa (perfil Admin) |
| `/Contact` | Fale conosco |

## 📌 Próximos passos

- [ ] CRUD de lanches, categorias e pedidos na área administrativa
- [ ] Exigir autenticação também no checkout
- [ ] Busca de lanches por nome
- [ ] Testes automatizados
- [ ] Migrar para .NET 8 (LTS) — o .NET 6 está fora de suporte

## 👤 Autor

**Roger Fraga Messina** — [GitHub](https://github.com/Roger8973)
