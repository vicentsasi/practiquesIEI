using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace practiquesIEI
{
    public class conexionBD
    {
        private static MySqlConnection conn;
        public static async void Conectar()
        {
            Console.WriteLine("Conectando");
            if (conn != null)
            {
                return;
            }
            try
            {
                string connectionString = "server=172.23.186.115;port=3306;user=Administrador;password=root;database=centrosbd;";
                conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();
                Console.WriteLine("Conectado");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString() + "Error");
            }
        }
    }
}
