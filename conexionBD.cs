using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace practiquesIEI
{
    internal class conexionBD
    {
        static async System.Threading.Tasks.Task Conectar()
        {
            string connectionString = "Server=collectify-server-mysql.mysql.database.azure.com;Port=3306;Database=IEI-Server;User Id=Administrador;Password=root;";
            MySqlConnection conn = null;

            try
            {
                // Crea una nueva conexión utilizando la cadena de conexión proporcionada
                conn = new MySqlConnection(connectionString);

                // Abre la conexión
                await conn.OpenAsync();

                Console.WriteLine("Conectado");
            }
            catch (Exception e)
            {
                // Manejo de excepciones en caso de error al abrir la conexión
                Console.WriteLine($"{e.Message}\nError");
            }
            finally
            {
                // Asegúrate de cerrar la conexión, incluso si hay un error
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    Console.WriteLine("Conexión cerrada");
                }
            }
}
