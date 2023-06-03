using Microsoft.Data.SqlClient;

namespace FIAP.Api.Data
{
    public  class ContextDB
    {
        private readonly string _connectionString;
        public ContextDB()
        {
            _connectionString = "Server=MEUPC\\COMETA;Initial Catalog=DesafioFIAP;User ID=sa;Password=@dmin123;TrustServerCertificate=True";
        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
