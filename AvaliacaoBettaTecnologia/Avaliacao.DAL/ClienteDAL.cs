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
    
    public class ClienteDAL    
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

        /// <summary>
        /// Faz o select no banco de dados buscando as informações referente a tabela Cliente.
        /// </summary>
        /// <param name="clienteDTO">
        /// Objeto do tipo ClienteDTO
        /// </param>
        /// <returns>
        /// Lista do tipo ClienteDTO
        /// </returns>
        public List<ClienteDTO> selectCliente(ClienteDTO clienteDTO)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();

                List<ClienteDTO> listaCliente = new List<ClienteDTO>();

                StringBuilder sbCampos = new StringBuilder();
                sbCampos.Append("" +
                                "CL.Id             , " +
                                "CL.Nome           , " +
                                "CL.Telefone       , " +
                                "CL.Email          , " +
                                "CL.Removido       , " +
                                "CL.Id_TipoCliente   " +
                "");

                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("SELECT " + sbCampos.ToString() + " FROM Cliente CL " +
                                   "WHERE NOT ISNULL(CL.Id, 0) = 0 " +
                                   "AND Removido = 0 " +
                "");

                /*-----------------------------------------------------------------------*/
                /* Critérios */

                if (clienteDTO.Id != 0)
                {
                    sbStringSQL.Append("AND CL.Id = @Id ");
                }
                                
                if (clienteDTO.Nome != null)
                {
                    sbStringSQL.Append("AND CL.Nome LIKE @Nome ");
                }

                if (clienteDTO.IdTipoCliente != 0)
                {
                    sbStringSQL.Append("AND CL.Id_TipoCliente = @Id_TipoCliente ");
                }
                
                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/
                /* Valores passados por parâmetros */

                if (clienteDTO.Id != 0)
                {
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt).Value = clienteDTO.Id;
                }
                if (clienteDTO.Nome != null)
                {
                    cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value = clienteDTO.Nome + "%";
                }
                if (clienteDTO.IdTipoCliente != 0)
                {
                    cmd.Parameters.Add("@Id_TipoCliente", System.Data.SqlDbType.TinyInt).Value = clienteDTO.IdTipoCliente;
                }                

                /*-----------------------------------------------------------------------*/

                reader = cmd.ExecuteReader();
 
                while(reader.Read())
                {
                    ClienteDTO cliente;
                    cliente = montaRegistroCliente(reader);
                    listaCliente.Add(cliente);
                }

                return listaCliente;
            }
            catch(Exception ex)
            {
                throw new Exception("Erro: " + ex.Message.ToString());                
            }
            finally
            {
                limpaObjetos();                
            }

        }

        private ClienteDTO montaRegistroCliente(SqlDataReader reader)
        {
            ClienteDTO resultado = new ClienteDTO();

            if((reader["Id"] != null))
            {
                resultado.Id = Convert.ToInt64(reader["Id"]);
            }

            if ((reader["Id_TipoCliente"] != null))
            {
                resultado.IdTipoCliente = Convert.ToInt32(reader["Id_TipoCliente"]);
            }

            if ((reader["Nome"] != null))
            {
                resultado.Nome = reader["Nome"].ToString();
            }

            if ((reader["Removido"] != null))
            {
                resultado.Removido = Convert.ToBoolean(reader["Removido"]);
            }

            if ((reader["Telefone"] != null))
            {
                resultado.Telefone = reader["Telefone"].ToString();
            }

            if ((reader["Email"] != null))
            {
                resultado.Email = reader["Email"].ToString();
            }

            return resultado;
        }


        public int insertCliente(ClienteDTO clienteDTO)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();

                StringBuilder sbCampos = new StringBuilder();
                sbCampos.Append("" +
                                "Id             , " +
                                "Nome           , " +
                                "Telefone       , " +
                                "Email          , " +
                                "Removido       , " +
                                "Id_TipoCliente   " +
                "");

                StringBuilder sbValores = new StringBuilder();
                sbValores.Append("" +
                                "@Id             , " +
                                "@Nome           , " +
                                "@Telefone       , " +
                                "@Email          , " +
                                "@Removido       , " +
                                "@Id_TipoCliente   " +
                "");

                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("INSERT INTO Cliente(" + sbCampos.ToString() + ") " +
                                   "VALUES(" + sbValores.ToString() + ") "  +
                "");

                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/
                /* Valores passados por parâmetros */
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt).Value = clienteDTO.Id;
                cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value = clienteDTO.Nome;
                cmd.Parameters.Add("@Telefone", System.Data.SqlDbType.NVarChar).Value = clienteDTO.Telefone;
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = clienteDTO.Email;
                cmd.Parameters.Add("@Removido", System.Data.SqlDbType.Bit).Value = clienteDTO.Removido;
                cmd.Parameters.Add("@Id_TipoCliente", System.Data.SqlDbType.TinyInt).Value = clienteDTO.IdTipoCliente;
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

        public int updateCliente(ClienteDTO clienteDTO)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();

                StringBuilder sbCamposValores = new StringBuilder();
                sbCamposValores.Append("" +
                                       "Nome            = @Nome           , " +
                                       "Telefone        = @Telefone       , " +
                                       "Email           = @Email          , " +
                                       "Id_TipoCliente  = @Id_TipoCliente   " +
                "");

                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("UPDATE Cliente SET " +
                                   sbCamposValores.ToString() + " " +
                                   "WHERE Id = @Id " +
                "");

                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/
                /* Valores passados por parâmetros */
                
                cmd.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar).Value = clienteDTO.Nome;
                cmd.Parameters.Add("@Telefone", System.Data.SqlDbType.NVarChar).Value = clienteDTO.Telefone;
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = clienteDTO.Email;                
                cmd.Parameters.Add("@Id_TipoCliente", System.Data.SqlDbType.TinyInt).Value = clienteDTO.IdTipoCliente;
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt).Value = clienteDTO.Id;

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

        public int inativaCliente(ClienteDTO clienteDTO)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();

                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("UPDATE Cliente SET Removido = @Removido WHERE Id = @Id ");

                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/
                /* Valores passados por parâmetros */
                cmd.Parameters.Add("@Removido", System.Data.SqlDbType.Bit).Value = clienteDTO.Removido;
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt).Value = clienteDTO.Id;
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
        
        public int deleteCliente(ClienteDTO clienteDTO)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();

                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("DELETE FROM Cliente WHERE Id = @Id ");

                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/
                /* Valores passados por parâmetros */
                cmd.Parameters.Add("@Id", System.Data.SqlDbType.BigInt).Value = clienteDTO.Id;                
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
