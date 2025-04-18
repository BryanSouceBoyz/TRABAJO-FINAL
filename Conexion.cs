using Microsoft.Data.SqlClient;


namespace SolMedBasicConsole
{
    internal class Connection
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadena = "Server=BRYAN\\SQLEXPRESS;Database=SolMedBasicDB;Integrated Security=True;TrustServerCertificate=True";
            return new SqlConnection(cadena);
        }
    }
}