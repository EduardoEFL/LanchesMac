using LanchesMac.context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {

        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessão
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um serviço do tipo do nosso contexto 
            var context = services.GetService<AppDbContext>();

            //obtem ou gera o id do carrinho
            string carrinhoid = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //atribui o id do carrinho na sessão
            session.SetString("CarrinhoId", carrinhoid);

            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoid
            };

        }
        public void AdicionarAoCarrinho(Lanche lanche)
        {
            CarrinhoCompraItem carrinhoCompraItem = ObterCarrinhoCompraItem(lanche);
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }

            _context.SaveChanges();
        }
        public int RemoveDoCarrinho(Lanche lanche)
        {
            CarrinhoCompraItem carrinhoCompraItem = ObterCarrinhoCompraItem(lanche);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                    _context.SaveChanges();
                    quantidadeLocal = _context.CarrinhoCompraItens.Count();
                }
            }

            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItems ??
                   (CarrinhoCompraItems =
                   _context.CarrinhoCompraItens
                   .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                   .Include(s => s.Lanche)
                   .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                   .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens
                 .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                 .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
            return total;
        }

        private CarrinhoCompraItem ObterCarrinhoCompraItem(Lanche lanche)
        {
            return _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                  s.CarrinhoCompraId == CarrinhoCompraId);

        }

    }
}

