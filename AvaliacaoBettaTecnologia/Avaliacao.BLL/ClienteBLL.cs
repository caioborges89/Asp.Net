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
    public class ClienteBLL
    {
        private ClienteDAL clienteDAL = new ClienteDAL();
        private SqlHelper sqlHelper = new SqlHelper();

        public List<ClienteDTO> selectCliente(ClienteDTO clienteDTO)
        {
            return clienteDAL.selectCliente(clienteDTO);
        }

        public int insertCliente(ClienteDTO clienteDTO)
        {
            return clienteDAL.insertCliente(clienteDTO);
        }

        public int updateCliente(ClienteDTO clienteDTO)
        {
            return clienteDAL.updateCliente(clienteDTO);
        }

        public int inativaCliente(ClienteDTO clienteDTO)
        {
            return clienteDAL.inativaCliente(clienteDTO);
        }

        public Int64 maxIdCliente()
        {
            return sqlHelper.selectMaxId("Cliente", "Id");
        }
    }
}
