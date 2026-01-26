using LanchesMac.Models;
using LanchesMac.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components
{
    public class CarrinhoCompraResumo :ViewComponent
    {
        private readonly CarrinhoCompra _carrinhocompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhocompra)
        {
            _carrinhocompra = carrinhocompra;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _carrinhocompra.GetCarrinhoCompraItens();

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhocompra,
                CarrinhoCompraTotal = _carrinhocompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }
    }
}
