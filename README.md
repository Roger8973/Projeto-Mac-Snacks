# 🍔 Mac Snacks (LanchesMac)

A web application built with **ASP.NET Core MVC (.NET 6)** that simulates an online snack shop: a catalog organized by category, a shopping cart, order checkout, and authentication with role-based access (member and administrator).

---

## ✨ Features

- **Home page** with a carousel and a showcase of snacks flagged as favorites.
- **Snack catalog** with a full listing, filtering by category (`/Snack/List/{category}`), and a details page.
- **Shopping cart** stored in the database and tied to the user's session (`CartId` kept in session): add items, remove items, and view a summary with the total.
- **Checkout** with a validated delivery form (name, address, ZIP code, etc.), calculation of total items and total price, and persistence of the order with its line items (`Order` / `OrderDetail`).
- **Authentication and registration** with ASP.NET Core Identity. Users must be logged in to add items to the cart.
- **Access roles** (`Member` and `Admin`) created automatically at startup, with an `Admin` authorization policy.
- **Admin area** (`/Admin`) restricted to the Admin role.
- **Contact page** and a custom e-mail Tag Helper.
- **View Components**: category menu and cart summary in the layout.

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC — .NET 6 |
| ORM | Entity Framework Core 6 (Code First + Migrations) |
| Database | SQL Server (Express/LocalDB) |
| Authentication | ASP.NET Core Identity |
| Front-end | Razor Views, Bootstrap 5, jQuery, jQuery Validation |

## 🧱 Architecture

The project follows the **MVC** pattern with the **Repository Pattern** and built-in dependency injection:

```
LanchesMac/
├── Areas/Admin/        # Admin area (restricted to the Admin role)
├── Components/         # View Components (CategoryMenu, ShoppingCartSummary)
├── Context/            # AppDbContext (IdentityDbContext + DbSets)
├── Controllers/        # Home, Snack, CartPurchase, Order, Account, Contact
├── Migrations/         # EF Core migrations (including initial data seeding)
├── Models/             # Snack, Category, CartPurchase, CartPurchaseItem, Order, OrderDetail
├── Repositories/       # Repositories and interfaces (Snacks, Categories, Orders)
├── Services/           # Seeding of initial roles and users
├── TagHelpers/         # EmailTagHelper
├── ViewModels/         # Screen ViewModels
├── Views/              # Razor Views
└── wwwroot/            # Static files (css, js, images, libs)
```

**Data model**

- `Category` 1 — N `Snack`
- `CartPurchaseItem` → `Snack` (cart items, grouped by `CartId`)
- `Order` 1 — N `OrderDetail` → `Snack`
- Identity tables (`AspNetUsers`, `AspNetRoles`, …)

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- SQL Server (Express or LocalDB)
- EF Core CLI tool: `dotnet tool install --global dotnet-ef`

### Steps

1. **Clone the repository**

   ```bash
   git clone https://github.com/Roger8973/Projeto-Mac-Snacks.git
   cd Projeto-Mac-Snacks/LanchesMac
   ```

2. **Set the connection string** in `appsettings.json` to point to your SQL Server instance. Example using LocalDB:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SnackDataBase;Trusted_Connection=True;"
   }
   ```

3. **Create the database and apply the migrations** (this also seeds categories and snacks):

   ```bash
   dotnet ef database update
   ```

4. **Run the application**

   ```bash
   dotnet run
   ```

   Open `https://localhost:7198` (or `http://localhost:5198`).

### Seed data

The migrations load the **Normal** and **Natural** categories along with some sample snacks (Cheese Salada, Misto Quente, Cheese Burger, Lanche Natural Peito de Peru…).

At startup, the application creates the following roles and users if they don't exist yet:

| User | Password | Role |
|---|---|---|
| `usuario@localhost` | `Numsey#2022` | Member |
| `admin@localhost` | `Numsey#2022` | Admin |

> ⚠️ These credentials are intended for development environments only.

## 🗺️ Main Routes

| Route | Description |
|---|---|
| `/` | Home page |
| `/Snack/List` | All snacks |
| `/Snack/List/{category}` | Snacks in a category |
| `/Snack/Details?snackId={id}` | Snack details |
| `/CartPurchase` | Shopping cart |
| `/Order/Checkout` | Order checkout |
| `/Account/Login` · `/Account/Register` | Login and registration |
| `/Admin` | Admin area (Admin role) |
| `/Contact` | Contact us |

## 📌 Roadmap

- [ ] CRUD for snacks, categories, and orders in the admin area
- [ ] Require authentication for checkout as well
- [ ] Search snacks by name
- [ ] Automated tests
- [ ] Upgrade to .NET 8 (LTS) — .NET 6 is out of support

## 👤 Author

**Roger Fraga Messina** — [GitHub](https://github.com/Roger8973)
