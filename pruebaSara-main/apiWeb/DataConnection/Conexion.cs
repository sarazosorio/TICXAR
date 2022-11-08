using Microsoft.Data.SqlClient;
namespace apiWeb.DataConnection
{
    public class Conexion
    {
        //Se crea la conexion con la bd
        SqlConnection Connection = new SqlConnection(@"server=localhost; database=PruebaSara;Trusted_Connection= True;" +
            "MultipleActiveResultSets= True;TrustServerCertificate= False;Encrypt= False");
        public SqlConnection getcn
        {
            get { return Connection; }
        }
    }
}
