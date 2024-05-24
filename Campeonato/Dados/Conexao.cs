using Microsoft.Data.SqlClient;
using System;
namespace Campeonato.Dados
{
    public class Conexao
    {
        string ConexaoBD = "Data Source=127.0.0.1; Initial Catalog=Campeonato; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=True";

        public string PegarConexao()
        {
            return ConexaoBD;
        }
    }
}
