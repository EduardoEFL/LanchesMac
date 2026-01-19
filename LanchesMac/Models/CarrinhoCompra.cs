using LanchesMac.context;
using LanchesMac.Migrations;

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
                }
            }
           
            return quantidadeLocal;



        }


        private CarrinhoCompraItem ObterCarrinhoCompraItem(Lanche lanche)
        {
            return _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                  s.CarrinhoCompraId == CarrinhoCompraId);

        }

    }
}

