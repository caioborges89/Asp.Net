using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacao.DTO
{
    public class ItensIndexDTO
    {
        public int Id { get; set; }

        public string Opcao { get; set; }

        public List<ItensIndexDTO> ListaItens() 
        {
            return new List<ItensIndexDTO>
            {
                new ItensIndexDTO{Id = 1, Opcao = "Cadastro de Cliente"},
                new ItensIndexDTO{Id = 2, Opcao = "Consulta de Cliente"},
                new ItensIndexDTO{Id = 3, Opcao = "Cadastro de Pedido"},
                new ItensIndexDTO{Id = 4, Opcao = "Consulta de Pedido"},
            };
        }

    }
}
