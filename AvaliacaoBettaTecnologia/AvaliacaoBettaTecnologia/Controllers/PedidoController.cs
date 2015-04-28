using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Avaliacao.BLL;
using Avaliacao.DTO;
using Avaliacao.Enum;

namespace Avaliacao.WEB.Controllers
{
    public class PedidoController : Controller
    {
        private PedidoBLL pedidoBLL;
        private PedidoDTO pedidoDTO;
        private List<PedidoDTO> listaPedido;

        private ClienteBLL clienteBLL;
        private ClienteDTO clienteDTO;
        private List<ClienteDTO> listaCliente;

#region "Instancias de Objetos"

        /// <summary>
        /// Método que cria novas instancias dos objetos referentes ao pedido.
        /// </summary>
        private void instanciaObjetosPedido()
        {
            pedidoBLL = new PedidoBLL();
            pedidoDTO = new PedidoDTO();
            listaPedido = new List<PedidoDTO>();
        }

        /// <summary>
        /// Método que cria novas instancias dos objetos referentes ao cliente.
        /// </summary>
        private void instanciaObjetosCliente()
        {
            clienteBLL = new ClienteBLL();
            clienteDTO = new ClienteDTO();
            listaCliente = new List<ClienteDTO>();
        }

#endregion
        
        public ActionResult Index()
        {
            instanciaObjetosPedido();

            listaPedido = pedidoBLL.selectPedido(pedidoDTO);

            return View(listaPedido);
        }

        public ActionResult Cadastrar()
        {
            instanciaObjetosCliente();
            instanciaObjetosPedido();
            listaCliente = clienteBLL.selectCliente(clienteDTO);

            pedidoDTO.Data = DateTime.Now;

            ViewBag.Cliente = new SelectList
            (
                listaCliente,
                "Id",
                "Nome"
            );

            return View(pedidoDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(PedidoDTO pedidoDTO)
        {
            try
            {
                if(ModelState.IsValid)                
                {
                    instanciaObjetosPedido();

                    pedidoDTO.Id = pedidoBLL.maxIdPedido();
                    pedidoBLL.inserPedido(pedidoDTO);
                    return RedirectToAction("Index");
                }

                return View(pedidoDTO);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Detalhes(Int64 id)
        {
            instanciaObjetosPedido();
            pedidoDTO.Id = id;
            listaPedido = pedidoBLL.selectPedido(pedidoDTO);

            if (listaPedido == null)
            {
                return HttpNotFound();
            }
            else
            {                
                return View(listaPedido[0]);
            }

        }

        public ActionResult Editar(Int64 id)
        {
            instanciaObjetosPedido();
            pedidoDTO.Id = id;
            listaPedido = pedidoBLL.selectPedido(pedidoDTO);

            if (listaPedido == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(listaPedido[0]);
            }
                        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(PedidoDTO pedidoDTO)
        {
            try
            {
               if(ModelState.IsValid)
               {
                   instanciaObjetosPedido();
                   pedidoBLL.updatePedido(pedidoDTO);
                   return RedirectToAction("Index");
               }
                return View(pedidoDTO);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Excluir(Int64 id)
        {
            instanciaObjetosPedido();
            pedidoDTO.Id = id;
            listaPedido = pedidoBLL.selectPedido(pedidoDTO);

            if (listaPedido == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(listaPedido[0]);
            }
        }

        
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirDefinitivo(Int64 id)
        {
            try
            {
                instanciaObjetosPedido();
                pedidoDTO.Id = id;
                pedidoBLL.deletePedido(pedidoDTO);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost,ActionName("Index")]
        public ActionResult Consultar(PedidoDTO pedidoDTO)
        {
            instanciaObjetosPedido();
            listaPedido = pedidoBLL.selectPedido(pedidoDTO);
            return View(listaPedido);
        }
    }
}
