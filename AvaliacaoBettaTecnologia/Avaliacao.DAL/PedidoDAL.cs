using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Avaliacao.UTIL;
using Avaliacao.DTO;

namespace Avaliacao.DAL
{
    public class PedidoDAL
    {
        private Conexao conexao;
        private SqlConnection conn;
        private SqlDataReader reader;

        /// <summary>
        /// Método responsável por limpar todos os objetos da classe e fechar
        /// a conexão com o Banco de Dados.
        /// </summary>
        private void limpaObjetos()
        {
            conexao.AbreFechaConexao(false);
            reader = null;
            GC.Collect();
        }

        public List<PedidoDTO> selectPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();

                List<PedidoDTO> listaPedido = new List<PedidoDTO>();

                StringBuilder sbCampos = new StringBuilder();
                sbCampos.Append("" +
                                "PE.Id             , " +
                                "CL.Nome           , " +
                                "CL.Telefone       , " +
                                "CL.Email          , " +
                                "CL.Removido       , " +
                                "CL.Id_TipoCliente , " +
                                "PE.Data           , " +
                                "PE.Descricao      , " +
                                "PE.Valor            " +
                "");

                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("SELECT " + sbCampos.ToString() + " FROM Pedido PE   " +
                                   "INNER JOIN Cliente CL ON CL.Id = PE.Id_Cliente      " +
                                   "WHERE NOT ISNULL(PE.Id, 0) = 0                      " +                                   
                "");

                /*-----------------------------------------------------------------------*/
                /* Critérios */

                if (pedidoDTO.Cliente != null)
                {
                    sbStringSQL.Append("AND CL.Nome LIKE @Nome ");
                }

                if (pedidoDTO.Id != 0)
                {
                    sbStringSQL.Append("AND PE.Id = @Id ");
                }
                                
                if (pedidoDTO.DataInicial != DateTime.MinValue)
                {
                    if (pedidoDTO.DataFinal != DateTime.MinValue)
                    {
                        sbStringSQL.Append("AND PE.Data BETWEEN @DataInicial AND @DataFinal ");
                    }
                }

                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/
                /* Valores passados por parâmetros */

                if (pedidoDTO.Cliente != null)
                {
                    cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value =  pedidoDTO.Cliente.Nome + "%";
                }

                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt).Value = pedidoDTO.Id;

                if (pedidoDTO.DataInicial != DateTime.MinValue)
                {
                    cmd.Parameters.Add("@DataInicial", System.Data.SqlDbType.DateTime).Value = pedidoDTO.DataInicial;                    
                }

                if (pedidoDTO.DataFinal != DateTime.MinValue)
                {
                    cmd.Parameters.Add("@DataFinal", System.Data.SqlDbType.DateTime).Value = pedidoDTO.DataFinal;
                }              
                
                /*-----------------------------------------------------------------------*/

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PedidoDTO pedido;
                    pedido = montaRegistroPedido(reader);
                    listaPedido.Add(pedido);
                }

                return listaPedido;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message.ToString());
            }
            finally
            {
                limpaObjetos();
            }

        }

        private PedidoDTO montaRegistroPedido(SqlDataReader reader)
        {
            PedidoDTO resultado = new PedidoDTO();

            if ((reader["Id"] != null))
            {
                resultado.Id = Convert.ToInt64(reader["Id"]);
            }

            if ((reader["Nome"] != null))
            {
                resultado.Cliente = new ClienteDTO();
                resultado.Cliente.Nome = reader["Nome"].ToString();                
            }

            if ((reader["Data"] != null))
            {
                resultado.Data = Convert.ToDateTime(reader["Data"]);
            }

            if ((reader["Descricao"] != null))
            {
                resultado.Descricao = reader["Descricao"].ToString();
            }

            if ((reader["Telefone"] != null))
            {
                resultado.Valor = Convert.ToDecimal(reader["Telefone"]);
            }

            if ((reader["Id_TipoCliente"] != null))
            {
                resultado.Cliente.IdTipoCliente = Convert.ToInt16(reader["Id_TipoCliente"]);
            }

            if ((reader["Descricao"] != null))
            {
                resultado.Descricao = reader["Descricao"].ToString();
            }

            if ((reader["Valor"] != null))
            {
                resultado.Valor = Convert.ToDecimal(reader["Valor"]);
            }

            return resultado;
        }

        public int insertPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();

                StringBuilder sbCampos = new StringBuilder();
                sbCampos.Append("" +
                                "Id         , " +
                                "Id_Cliente , " +
                                "Data       , " +
                                "Descricao  , " +
                                "Valor        " +                                
                "");

                StringBuilder sbValores = new StringBuilder();
                sbValores.Append("" +
                                "@Id         , " +
                                "@Id_Cliente , " +
                                "@Data       , " +
                                "@Descricao  , " +
                                "@Valor        " +
                "");


                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("INSERT INTO Pedido(" + sbCampos.ToString() + ") " +
                                   "VALUES(" + sbValores.ToString() + ") " +                                   
                "");

                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/
                /* Valores passados por parâmetros */

                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt).Value = pedidoDTO.Id;
                cmd.Parameters.Add("@Id_Cliente", System.Data.SqlDbType.BigInt).Value = pedidoDTO.Cliente.Id;
                cmd.Parameters.Add("@Data", System.Data.SqlDbType.DateTime).Value = pedidoDTO.Data;
                cmd.Parameters.Add("@Descricao", System.Data.SqlDbType.NVarChar).Value = pedidoDTO.Descricao;
                cmd.Parameters.Add("@Valor", System.Data.SqlDbType.Decimal).Value = pedidoDTO.Valor;

                /*-----------------------------------------------------------------------*/

                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message.ToString());
            }
            finally
            {
                limpaObjetos();
            }

        }

        public int updatePedido(PedidoDTO pedidoDTO)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();

                StringBuilder sbCamposValores = new StringBuilder();
                sbCamposValores.Append("" +
                                "Descricao  = @Descricao, " +
                                "Valor      = @Valor      " +
                "");
                
                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("UPDATE Pedido SET " +
                                   sbCamposValores.ToString() + " " +
                                   "WHERE Id = @Id " +
                "");

                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/
                /* Valores passados por parâmetros */
                
                cmd.Parameters.Add("@Descricao", System.Data.SqlDbType.NVarChar).Value = pedidoDTO.Descricao;
                cmd.Parameters.Add("@Valor", System.Data.SqlDbType.Decimal).Value = pedidoDTO.Valor;
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt).Value = pedidoDTO.Id;

                /*-----------------------------------------------------------------------*/

                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message.ToString());
            }
            finally
            {
                limpaObjetos();
            }

        }

        public int deletePedido(PedidoDTO pedidoDTO)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();
                
                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("DELETE FROM Pedido WHERE Id = @Id ");

                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/
                /* Valores passados por parâmetros */

                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt).Value = pedidoDTO.Id;
             
                /*-----------------------------------------------------------------------*/

                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message.ToString());
            }
            finally
            {
                limpaObjetos();
            }

        }

    }
}
