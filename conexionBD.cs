using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        public static async void insertCentro(centro_educativo centro) {
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
      

                string consultaExistencia = $"SELECT COUNT(*) " +
                                            $"FROM centro_educativo " +
                                            $"WHERE nombre = '{centro.nombre}' AND " +
                                                  $"tipo = '{centro.tipo}' AND " +
                                                  $"direccion = '{centro.direccion}' AND " +
                                                  $"codigo_postal = '{centro.cod_postal}' AND " +
                                                  $"longitud = '{centro.longitud}' AND " +
                                                  $"latitud = '{centro.latitud}' AND " +
                                                  $"telefono = '{centro.telefono}' AND " +
                                                  $"descripcion = '{centro.descripcion}'";
                using (MySqlCommand commandExistencia = new MySqlCommand(consultaExistencia, conn))
                {
                    int cantidadExistente = Convert.ToInt32(commandExistencia.ExecuteScalar());
                    if (cantidadExistente > 0)
                    {
                        Console.WriteLine($"El centro {centro.nombre} ya existe en la base de datos.");
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(
                         $"INSERT INTO centro_educativo (nombre, tipo, direccion, codigo_postal, longitud, latitud, telefono, descripcion, cod_localidad) VALUES ('{centro.nombre}', '{centro.tipo}', '{centro.direccion}', '{centro.cod_postal}', '{centro.longitud}', '{centro.latitud}', '{centro.telefono}','{centro.descripcion}','{centro.loc_codigo}')", conn))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("Centro insertado correctamente.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message}");
            }
        }
        public static async void insertLocalidad(localidad loc)
        {
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consultaExistencia = $"SELECT COUNT(*) " +
                                            $"FROM localidad " +
                                            $"WHERE loc_nombre = '{loc.nombre}' AND " +
                                                  $"loc_codigo = '{loc.codigo}'";
                using (MySqlCommand commandExistencia = new MySqlCommand(consultaExistencia, conn))
                {
                    int cantidadExistente = Convert.ToInt32(commandExistencia.ExecuteScalar());
                    if (cantidadExistente > 0)
                    {
                        Console.WriteLine($"La localidad {loc.nombre} ya existe en la base de datos.");
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(
                        $"INSERT INTO localidad(loc_codigo, loc_nombre, prov_nombre) VALUES ('{loc.codigo}','{loc.nombre}','{loc.prov_nombre}')", conn))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("Localidad insertada correctamente.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message}");
            }
        }
        public static async void insertProvincia(provincia prov)
        {
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
            try
            {
                string consultaExistencia = $"SELECT COUNT(*) " +
                                            $"FROM provincia " +
                                            $"WHERE prov_nombre = '{prov.nombre}' AND " +
                                                  $"prov_codigo = '{prov.codigo}'";
                using (MySqlCommand commandExistencia = new MySqlCommand(consultaExistencia, conn))
                {
                    int cantidadExistente = Convert.ToInt32(commandExistencia.ExecuteScalar());
                    if (cantidadExistente > 0)
                    {
                        Console.WriteLine($"La Provincia {prov.nombre} ya existe en la base de datos.");
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(
                        $"INSERT INTO provincia(prov_codigo, prov_nombre) VALUES ('{prov.codigo}','{prov.nombre}')", conn))
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("Provincia insertada correctamente.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message}");
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
                    Console.WriteLine("Datos  de provincia borrados correctamente.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message}");
            }
        }

    }
}
