using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Avaliacao.UTIL
{
    public class SqlHelper
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

        public Int64 selectMaxId(string tabela, string campo)
        {
            try
            {
                conexao = new Conexao();
                conn = new SqlConnection();

                StringBuilder sbStringSQL = new StringBuilder();
                sbStringSQL.Append("SELECT MAX(" + campo + ") AS MaxId FROM " + tabela + " ");


                /*-----------------------------------------------------------------------*/

                conn = conexao.AbreFechaConexao(true);
                SqlCommand cmd = new SqlCommand(sbStringSQL.ToString(), conn);

                /*-----------------------------------------------------------------------*/

                Int64 maxId = Convert.ToInt64(cmd.ExecuteScalar());

                if(maxId != null)
                {
                    maxId++;
                }
                else
                {
                    maxId = 1;
                }

                return maxId; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                limpaObjetos();
            }

        }
    }
}
