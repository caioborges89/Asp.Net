using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avaliacao.DAL;
using Avaliacao.DTO;
using Avaliacao.UTIL;

namespace Avaliacao.BLL
{
    public class PedidoBLL
    {
        private PedidoDAL pedidoDAL = new PedidoDAL();
        private SqlHelper sqlHelper = new SqlHelper();

        public List<PedidoDTO> selectPedido(PedidoDTO pedidoDTO)
        {
            return pedidoDAL.selectPedido(pedidoDTO);
        }

        public int inserPedido(PedidoDTO pedidoDTO)
        {
            return pedidoDAL.insertPedido(pedidoDTO);
        }

        public int updatePedido(PedidoDTO pedidoDTO)
        {
            return pedidoDAL.updatePedido(pedidoDTO);
        }

        public int deletePedido(PedidoDTO pedidoDTO)
        {
            return pedidoDAL.deletePedido(pedidoDTO);
        }

        public Int64 maxIdPedido()
        {
            return sqlHelper.selectMaxId("Pedido", "Id");
        }
    }
}
