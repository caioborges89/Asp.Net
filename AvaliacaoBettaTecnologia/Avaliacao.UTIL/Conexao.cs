using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient ;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Avaliacao.UTIL
{
    /// <summary>
    /// Classe responsável pela conexão com o banco de dados
    /// </summary>
    public class Conexao:IDisposable 
    {
        private SqlConnection conn;

        /// <summary>
        /// Método responsável por abrir e fechar a conexão com o banco de dados.
        /// </summary>
        /// <param name="active">
        /// True - Abre a conexão com o Banco. |
        /// False - Fecha a conexão com o Banco.
        /// </param>
        public SqlConnection AbreFechaConexao(bool active)
        {
            try
            {
                //conn = new SqlConnection("Server=CAIO-PC;Database=Avaliacao;Trusted_Connection=True;");
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoSqlServer"].ConnectionString);
                if (active == true)
                { 
                    conn.Open();                     
                }
                else
                { 
                    conn.Close(); 
                }
                return conn;
            }
            catch(Exception ex)
            {
                throw ex; 
            }            
            
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
