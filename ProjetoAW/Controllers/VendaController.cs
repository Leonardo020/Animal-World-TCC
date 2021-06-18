using ProjetoAW.Repositorio;
using System.Web.Mvc;

namespace ProjetoAW.Controllers
{
    public class VendaController : Controller
    {
        actionsEntrega acEntrega = new actionsEntrega();
        actionsPedido acPedido = new actionsPedido();
        // GET: Venda
        public ActionResult DadosEntrega()
        {
            var entrega = acEntrega.consultaEntrega();
            return View(entrega);
        }

        public ActionResult adicionarItemCarrinho()
        {
            return View();
        }

        public ActionResult MeuCarrinho()
        {
            return View();
        }

        public ActionResult DadosPedido()
        {
            return View();
        }

        public ActionResult ListaPedido()
        {
            var pedidos = acPedido.consultaPedido();
            return View(pedidos);
        }
    }
}