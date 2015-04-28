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
    public class ClienteController : Controller
    {
        //
        // GET: /Cliente/

        private ClienteDTO clienteDTO = new ClienteDTO();
        private ClienteBLL clienteBLL = new ClienteBLL();
        private List<ClienteDTO> listaCliente = new List<ClienteDTO>();

        private void tratarDados(List<ClienteDTO> lista = null)
        {
            if(lista == null)
            {
                listaCliente = clienteBLL.selectCliente(clienteDTO);
            }  
          
            for(int i =0; i <listaCliente.Count; i++)
            {
                if(listaCliente[i].IdTipoCliente == 1) /* Quando for 1 será Pessoa Fisica */
                {
                    listaCliente[i].FisicaJuridica = "Fisica";
                }
                else if (listaCliente[i].IdTipoCliente == 2) /* Quando for 2 será Pessoa Juridica */
                {
                    listaCliente[i].FisicaJuridica = "Juridica";
                }

            }
        }

        private void preencheDropDownList(int idTipoCliente = 0)
        {
            Array valorTipoPessoa = System.Enum.GetValues(typeof(TipoCliente));
            Array nomeTipoPessoa = System.Enum.GetNames(typeof(TipoCliente));
            
            ViewBag.TipoCliente = new SelectList
                    (
                    nomeTipoPessoa
                    );

            if (idTipoCliente == 1)
                listaCliente[0].TipoCliente = TipoCliente.Fisica;
            else if (idTipoCliente == 2)
                listaCliente[0].TipoCliente = TipoCliente.Juridica;
            
        }

        public ActionResult Index(ClienteDTO clienteDTO)
        {
            tratarDados();
            preencheDropDownList();
            return View(listaCliente);
        }

        public ActionResult Cadastrar()
        {
            preencheDropDownList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(ClienteDTO clienteDTO)
        {
            if(ModelState.IsValid)
            {
                //tratarDados();
                if (clienteDTO.TipoCliente.ToString() == "Fisica")
                {
                    clienteDTO.IdTipoCliente = 1;
                }
                else if (clienteDTO.TipoCliente.ToString() == "Juridica")
                {
                    clienteDTO.IdTipoCliente = 2;
                }
                else
                {
                    clienteDTO.IdTipoCliente = 0;
                }

                if (clienteDTO.Telefone != null)
                {
                    clienteDTO.Telefone = clienteDTO.Telefone.Replace("(", "");
                    clienteDTO.Telefone = clienteDTO.Telefone.Replace(")", "");
                    clienteDTO.Telefone = clienteDTO.Telefone.Replace("-", "");
                }  

                clienteDTO.Id = clienteBLL.maxIdCliente();
                clienteBLL.insertCliente(clienteDTO);
                return RedirectToAction("Index");
            }
            return View(clienteDTO);
        }

        public ActionResult Editar(Int64 id)
        {
            clienteDTO = new ClienteDTO();
            clienteDTO.Id = id;
            listaCliente = clienteBLL.selectCliente(clienteDTO);

            preencheDropDownList(listaCliente[0].IdTipoCliente);

            if(listaCliente == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(listaCliente[0]);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ClienteDTO clienteDTO)
        {
            if (ModelState.IsValid)
            {
                if (clienteDTO.TipoCliente.ToString() == "Fisica")
                    clienteDTO.IdTipoCliente = 1;
                else if (clienteDTO.TipoCliente.ToString() == "Juridica")
                    clienteDTO.IdTipoCliente = 2;
                
                if(clienteDTO.Telefone != null)
                {
                    clienteDTO.Telefone = clienteDTO.Telefone.Replace("(", "");
                    clienteDTO.Telefone = clienteDTO.Telefone.Replace(")", "");
                    clienteDTO.Telefone = clienteDTO.Telefone.Replace("-", "");
                }               

                clienteBLL.updateCliente(clienteDTO);
                return RedirectToAction("Index");
            }
            return View(clienteDTO);
        }

        public ActionResult Detalhes(Int64 id)
        {
            clienteDTO = new ClienteDTO();
            clienteDTO.Id = id;
            listaCliente = clienteBLL.selectCliente(clienteDTO);

            if (listaCliente == null)
            {
                return HttpNotFound();
            }
            else
            {
                tratarDados();
                return View(listaCliente[0]);
            }
        }

        public ActionResult Excluir(Int64 id)
        {
            clienteDTO = new ClienteDTO();
            clienteDTO.Id = id;
            listaCliente = clienteBLL.selectCliente(clienteDTO);

            if (listaCliente == null)
            {
                return HttpNotFound();
            }
            else
            {
                tratarDados();
                return View(listaCliente[0]);
            }
        }

        [HttpPost,ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult InativaCliente(Int64 id)
        {
            clienteDTO = new ClienteDTO();
            clienteDTO.Id = id;
            clienteDTO.Removido = true;
            clienteBLL.inativaCliente(clienteDTO);
            return RedirectToAction("Index");
        }

        [HttpPost,ActionName("Index")]
        public ActionResult Consultar(ClienteDTO clienteDTO)
        {
            preencheDropDownList();

            if (clienteDTO.TipoCliente.ToString() == "Fisica")
            {
                clienteDTO.IdTipoCliente = 1;
            }                
            else if(clienteDTO.TipoCliente.ToString() == "Juridica")
            {
                clienteDTO.IdTipoCliente = 2;
            }                
            else
            {
                clienteDTO.IdTipoCliente = 0;
            }                
                        
            listaCliente = clienteBLL.selectCliente(clienteDTO);
            tratarDados(listaCliente);
            return View(listaCliente);
        }
    }
}
