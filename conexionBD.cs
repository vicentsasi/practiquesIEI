using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using practiquesIEI.Entities;

namespace practiquesIEI
{
    public class ConexionBD
    {
        private static MySqlConnection conn;
        public static async Task Conectar()
        {
            Console.WriteLine("Conectando");
            if (conn != null)
            {
                return;
            }
            try
            {
                string connectionString = "" +
                     "server=172.23.186.115;" +
                    "port=3306;" +
                    "user=Administrador;" +
                    "password=root;" +
                    "database=centrosbd;";
                conn = new MySqlConnection(connectionString);
                await conn.OpenAsync();
                Console.WriteLine("Conectado");
                await BorrarCentros();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        public static async Task<string> insertCentro(centro_educativo centro, string logs) {
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consultaExistencia = $"SELECT COUNT(*) " +
                                            $"FROM centro_educativo " +
                                            $"WHERE nombre = @nombre AND " +
                                                  $"direccion = @direccion AND " +
                                                  $"codigo_postal = @cod_postal AND " +
                                                  $"longitud = @longitud AND " +
                                                  $"latitud = @latitud AND " +
                                                  $"telefono = @telefono ";
                using (MySqlCommand commandExistencia = new MySqlCommand(consultaExistencia, conn))
                {
                    commandExistencia.Parameters.AddWithValue("@nombre", centro.nombre);
                    commandExistencia.Parameters.AddWithValue("@direccion", centro.direccion);
                    commandExistencia.Parameters.AddWithValue("@cod_postal", centro.cod_postal);
                    commandExistencia.Parameters.AddWithValue("@longitud", centro.longitud);
                    commandExistencia.Parameters.AddWithValue("@latitud", centro.latitud);
                    commandExistencia.Parameters.AddWithValue("@telefono", centro.telefono);
                    int cantidadExistente = Convert.ToInt32(commandExistencia.ExecuteScalar());
                    if (cantidadExistente > 0)
                    {
                        logs += $"El centro {centro.nombre} ya existe en la base de datos.\r\n";
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(
                         $"INSERT INTO centro_educativo (nombre, tipo, direccion, codigo_postal, longitud, latitud, telefono, descripcion, cod_localidad) VALUES (@nombre, @tipo, @direccion, @cod_postal, @longitud, @latitud, @telefono,@descripcion,@loc_codigo)", conn))
                        {
                            command.Parameters.AddWithValue("@nombre", centro.nombre);
                            command.Parameters.AddWithValue("@tipo", centro.tipo.ToString());
                            command.Parameters.AddWithValue("@direccion", centro.direccion);
                            command.Parameters.AddWithValue("@cod_postal", centro.cod_postal);
                            command.Parameters.AddWithValue("@longitud", centro.longitud);
                            command.Parameters.AddWithValue("@latitud", centro.latitud);
                            command.Parameters.AddWithValue("@telefono", centro.telefono);
                            command.Parameters.AddWithValue("@descripcion", centro.descripcion);
                            command.Parameters.AddWithValue("@loc_codigo", centro.loc_codigo);
                            command.ExecuteNonQuery();
                            logs += "Centro insertado correctamente.\r\n";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logs +=$"Error al ejecutar el comando: {e.Message}\r\n";
            }
            return logs;
        }
        public static async void insertLocalidad(localidad loc, string logs)
        {
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consultaExistencia = $"SELECT COUNT(*) " +
                                            $"FROM localidad " +
                                            $"WHERE loc_nombre = @nombre AND " +
                                                  $"loc_codigo = @codigo ";
                using (MySqlCommand commandExistencia = new MySqlCommand(consultaExistencia, conn))
                {
                    commandExistencia.Parameters.AddWithValue("@nombre", loc.nombre);
                    commandExistencia.Parameters.AddWithValue("@codigo", loc.codigo);
                    int cantidadExistente = Convert.ToInt32(commandExistencia.ExecuteScalar());
                    if (cantidadExistente > 0)
                    {
                        logs += $"La localidad {loc.nombre} ya existe en la base de datos.\n";
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(
                        $"INSERT INTO localidad(loc_codigo, loc_nombre, prov_nombre) VALUES (@codigo,@nombre,@provnombre)", conn))
                        {
                            command.Parameters.AddWithValue("@nombre", loc.nombre);
                            command.Parameters.AddWithValue("@codigo", loc.codigo);
                            command.Parameters.AddWithValue("@provnombre", loc.prov_nombre);
                            command.ExecuteNonQuery();
                            logs += "Localidad insertada correctamente.\n";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logs += $"Error al ejecutar el comando: {e.Message}\n";
            }
        }
        public static async void insertProvincia(provincia prov, string logs)
        {
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consultaExistencia = $"SELECT COUNT(*) " +
                                            $"FROM provincia " +
                                            $"WHERE prov_nombre = @nombre AND " +
                                                  $"prov_codigo = @codigo";
                using (MySqlCommand commandExistencia = new MySqlCommand(consultaExistencia, conn))
                {
                    commandExistencia.Parameters.AddWithValue("@nombre", prov.nombre);
                    commandExistencia.Parameters.AddWithValue("@codigo", prov.codigo);
                    int cantidadExistente = Convert.ToInt32(commandExistencia.ExecuteScalar());
                    if (cantidadExistente > 0)
                    {
                        logs += $"La Provincia {prov.nombre} ya existe en la base de datos.\n";
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(
                        $"INSERT INTO provincia(prov_codigo, prov_nombre) VALUES (@codigo,@nombre)", conn))
                        {
                            command.Parameters.AddWithValue("@nombre", prov.nombre);
                            command.Parameters.AddWithValue("@codigo", prov.codigo);
                            command.ExecuteNonQuery();
                            logs +="Provincia insertada correctamente.\n";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logs += $"Error al ejecutar el comando: {e.Message}\n";
            }
        }
        public static async Task BorrarCentros()
        {
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                using (MySqlCommand command1 = new MySqlCommand(
                    $"DELETE FROM centro_educativo", conn))
                {
                    await command1.ExecuteNonQueryAsync();
                    Console.WriteLine("Datos de centros borrados correctamente.");
                }
                using (MySqlCommand command2 = new MySqlCommand(
                    $"DELETE FROM localidad", conn))
                {
                    await command2.ExecuteNonQueryAsync();
                    Console.WriteLine("Datos de localidad borrados correctamente.");
                }
                using (MySqlCommand command3 = new MySqlCommand(
                    $"DELETE FROM provincia", conn))
                {
                    await command3.ExecuteNonQueryAsync();
                    Console.WriteLine( "Datos  de provincia borrados correctamente. \n");
                }
            }
            catch (Exception e)
            {
               Console.WriteLine($"Error al ejecutar el comando: {e.Message} \n")  ;
            }
        }
        public static async Task<List<centro_educativo>> FindCentrosByLocalidad(string loc) {

            List<centro_educativo> listCenters = new List<centro_educativo>();

            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consulta = $"SELECT * " +
                                  $"FROM centro_educativo c " +
                                  $"JOIN localidad l ON l.loc_codigo = c.cod_localidad " +
                                  $"WHERE l.loc_nombre = @nombre_loc";
                using (MySqlCommand command = new MySqlCommand(consulta, conn))
                {
                    command.Parameters.AddWithValue("@nombre_loc", loc);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Verifica si hay filas en el resultado
                        if (reader.HasRows)
                        {
                            // Itera a través de las filas
                            while (reader.Read())
                            {
                                centro_educativo centro = new centro_educativo();
                                centro.nombre = reader["nombre"].ToString();
                                centro.latitud = reader["latitud"].ToString().Replace(',', '.');
                                centro.longitud = reader["longitud"].ToString().Replace(',', '.');
                                //centro.tipo = reader["tipo"].ToString();
                                centro.cod_postal = reader["codigo_postal"].ToString();
                                centro.telefono = int.Parse(reader["telefono"].ToString());
                                centro.descripcion = reader["descripcion"].ToString();
                                centro.direccion = reader["direccion"].ToString();
                                listCenters.Add(centro);
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message} \n");
            }
            return listCenters;
        }
        public static async Task<List<centro_educativo>> FindCentrosByTipo(string tipo)
        {

            List<centro_educativo> listCenters = new List<centro_educativo>();

            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consulta = $"SELECT * " +
                                  $"FROM centro_educativo c " +
                                  $"WHERE c.tipo = @tipo";
                using (MySqlCommand command = new MySqlCommand(consulta, conn))
                {
                    command.Parameters.AddWithValue("@tipo", tipo);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Verifica si hay filas en el resultado
                        if (reader.HasRows)
                        {
                            // Itera a través de las filas
                            while (reader.Read())
                            {
                                centro_educativo centro = new centro_educativo();
                                centro.nombre = reader["nombre"].ToString();
                                centro.latitud = reader["latitud"].ToString().Replace(',', '.');
                                centro.longitud = reader["longitud"].ToString().Replace(',', '.');
                                //centro.tipo = reader["tipo"].ToString();
                                centro.cod_postal = reader["codigo_postal"].ToString();
                                centro.telefono = int.Parse(reader["telefono"].ToString());
                                centro.descripcion = reader["descripcion"].ToString();
                                centro.direccion = reader["direccion"].ToString();
                                listCenters.Add(centro);
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message} \n");
            }
            return listCenters;
        }
        public static async Task<List<centro_educativo>> FindCentrosByProvincia(string prov)
        {

            List<centro_educativo> listCenters = new List<centro_educativo>();

            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consulta = $"SELECT * " +
                                  $"FROM centro_educativo c " +
                                  $"JOIN localidad l ON l.loc_codigo = c.cod_localidad " +
                                  $"JOIN provincia p ON l.prov_nombre = p.prov_nombre " +
                                  $"WHERE p.prov_nombre = @nombre_prov";
                using (MySqlCommand command = new MySqlCommand(consulta, conn))
                {
                    command.Parameters.AddWithValue("@nombre_prov", prov);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Verifica si hay filas en el resultado
                        if (reader.HasRows)
                        {
                            // Itera a través de las filas
                            while (reader.Read())
                            {
                                centro_educativo centro = new centro_educativo();
                                centro.nombre = reader["nombre"].ToString();
                                centro.latitud = reader["latitud"].ToString().Replace(',', '.');
                                centro.longitud = reader["longitud"].ToString().Replace(',', '.');
                                //centro.tipo = reader["tipo"].ToString();
                                centro.cod_postal = reader["codigo_postal"].ToString();
                                centro.telefono = int.Parse(reader["telefono"].ToString());
                                centro.descripcion = reader["descripcion"].ToString();
                                centro.direccion = reader["direccion"].ToString();
                                listCenters.Add(centro);
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message} \n");
            }
            return listCenters;
        }
        public static async Task<List<centro_educativo>> FindCentrosByCod_Postal(string cp)
        {

            List<centro_educativo> listCenters = new List<centro_educativo>();

            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consulta = $"SELECT * " +
                                  $"FROM centro_educativo c " +
                                  $"WHERE c.codigo_postal = @cod_postal";
                using (MySqlCommand command = new MySqlCommand(consulta, conn))
                {
                    command.Parameters.AddWithValue("@cod_postal", cp);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Verifica si hay filas en el resultado
                        if (reader.HasRows)
                        {
                            // Itera a través de las filas
                            while (reader.Read())
                            {
                                centro_educativo centro = new centro_educativo();
                                centro.nombre = reader["nombre"].ToString();
                                centro.latitud = reader["latitud"].ToString().Replace(',', '.');
                                centro.longitud = reader["longitud"].ToString().Replace(',', '.');
                                //centro.tipo = reader["tipo"].ToString();
                                centro.cod_postal = reader["codigo_postal"].ToString();
                                centro.telefono = int.Parse(reader["telefono"].ToString());
                                centro.descripcion = reader["descripcion"].ToString();
                                centro.direccion = reader["direccion"].ToString();
                                listCenters.Add(centro);
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message} \n");
            }
            return listCenters;
        }
        public static async Task<List<centro_educativo>> getAllCentros()
        {

            List<centro_educativo> listCenters = new List<centro_educativo>();

            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consulta = $"SELECT * " +
                                  $"FROM centro_educativo c ";
                using (MySqlCommand command = new MySqlCommand(consulta, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Verifica si hay filas en el resultado
                        if (reader.HasRows)
                        {
                            // Itera a través de las filas
                            while (reader.Read())
                            {
                                centro_educativo centro = new centro_educativo();
                                centro.nombre = reader["nombre"].ToString();
                                centro.latitud = reader["latitud"].ToString().Replace(',', '.');
                                centro.longitud = reader["longitud"].ToString().Replace(',', '.');
                                //centro.tipo = reader["tipo"].ToString();
                                centro.cod_postal = reader["codigo_postal"].ToString();
                                centro.telefono = int.Parse(reader["telefono"].ToString());
                                centro.descripcion = reader["descripcion"].ToString();
                                centro.direccion = reader["direccion"].ToString();
                                listCenters.Add(centro);
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message} \n");
            }
            return listCenters;
        }
        public static List<centro_educativo> buscarCentros(string Localidad, int codPos, string provincia, string  tipo) {

            List <centro_educativo> listaCentros = new List<centro_educativo>();
            if (conn.State == ConnectionState.Closed)
            {
                conn.OpenAsync();
            }
            try
            {
                string consultaExistencia = $"SELECT * " +
                                            $"FROM centro_educativo C, localidad L " +
                                            $"WHERE C.loc_codigo = L.loc_codigo ";

                if (Localidad != "" || codPos != 0 || provincia != "" || tipo != "") {
                    if (Localidad != "") { consultaExistencia += $" AND L.loc_nombre = '{Localidad}' "; }
                    if (codPos != 0) { consultaExistencia += $" AND C.codigo_postal = '{codPos.ToString()}' "; }
                    if (provincia != "") { consultaExistencia += $" AND L.prov_nombre = {provincia} "; }
                    if (tipo != "") { consultaExistencia += $" AND C.tipo = {tipo} "; }
                }
                MySqlCommand command1 = new MySqlCommand(consultaExistencia);

                using (MySqlDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        centro_educativo centro = new centro_educativo();
                        {
                            centro.nombre = reader["nombre"].ToString();
                            centro.tipo = (tipo_centro)Enum.Parse(typeof(tipo_centro), reader["tipo"].ToString());
                            centro.longitud = reader["longitud"].ToString();
                            centro.latitud = reader["latitud"].ToString();
                            centro.telefono = Convert.ToInt32(reader["telefono"]);
                            centro.descripcion = reader["descripcion"].ToString();
                            centro.direccion = reader["direccion"].ToString();
                            centro.cod_postal = reader["codigo_postal"].ToString();
                            centro.loc_codigo = reader["loc_codigo"].ToString();
                        };

                        listaCentros.Add(centro);
                    }
                }
                return listaCentros;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al conectar con la BD: {e.Message}");
            }
            return listaCentros;
        }
    }
}
