using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CartPurchase
    {
        private readonly AppDbContext _context;

        public CartPurchase(AppDbContext context)
        {
            _context = context;
        }

        public string CartPurchaseId { get; set; }
        public List<CartPurchaseItem> CartPurchaseItems { get; set; }
        public static CartPurchase GetCartPurchase(IServiceProvider services)
        {
            //define uma sessão
            //obtem um serviço do tipo do contexto 
            //obtem ou gera o Id do carrinho
            //atribui o id do carrinho na Sessão
            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
          
            var context = services.GetService<AppDbContext>();      
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();       
            session.SetString("CartId", cartId);
       
            return new CartPurchase(context)
            {
                CartPurchaseId = cartId
            };
        }

        public void AddToCart(Snack snack)
        {
            var cartPurchaseItem = _context.CartPurchaseItems.SingleOrDefault(
                     s => s.Snack.SnackId == snack.SnackId &&
                     s.CartPurchaseId == CartPurchaseId);

            if (cartPurchaseItem == null)
            {
                cartPurchaseItem = new CartPurchaseItem
                {
                    CartPurchaseId = CartPurchaseId,
                    Snack = snack,
                    Amount = 1
                };
                _context.CartPurchaseItems.Add(cartPurchaseItem);
            }
            else
            {
                cartPurchaseItem.Amount++;
            }
            _context.SaveChanges();
        }

        public int RemoveFromCart(Snack snack)
        {
            var cartPurchaseItem = _context.CartPurchaseItems.SingleOrDefault(
                     s => s.Snack.SnackId == snack.SnackId &&
                     s.CartPurchaseId == CartPurchaseId);

            var localQuantity = 0;

            if (cartPurchaseItem != null)
            {
                if (cartPurchaseItem.Amount > 1)
                {
                    cartPurchaseItem.Amount--;
                    localQuantity = cartPurchaseItem.Amount;
                }
                else
                {
                    _context.CartPurchaseItems.Remove(cartPurchaseItem);
                }
            }
            _context.SaveChanges();
            return localQuantity;
        }

        public List<CartPurchaseItem> GetCartPurchaseItems()
        {
            return CartPurchaseItems ??
                   (CartPurchaseItems =
                       _context.CartPurchaseItems.Where(c => c.CartPurchaseId == CartPurchaseId)
                           .Include(s => s.Snack)
                           .ToList());
        }

        public void CleanCart()
        {
            var cartItems = _context.CartPurchaseItems
                                 .Where(carrinho => carrinho.CartPurchaseId == CartPurchaseId);

            _context.CartPurchaseItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public decimal TotalShoppingCart()
        {
            var total = _context.CartPurchaseItems.Where(c => c.CartPurchaseId == CartPurchaseId)
                .Select(c => c.Snack.Price * c.Amount).Sum();
            return total;
        }
    }
}

