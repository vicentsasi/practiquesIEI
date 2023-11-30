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
                                            $"WHERE nombre = @nombre AND " +
                                                  $"tipo = @tipo AND " +
                                                  $"direccion = @direccion AND " +
                                                  $"codigo_postal = @cod_postal AND " +
                                                  $"longitud = @longitud AND " +
                                                  $"latitud = @latitud AND " +
                                                  $"telefono = @telefono AND " +
                                                  $"descripcion = @descripcion";
                using (MySqlCommand commandExistencia = new MySqlCommand(consultaExistencia, conn))
                {
                    commandExistencia.Parameters.AddWithValue("@nombre", centro.nombre);
                    commandExistencia.Parameters.AddWithValue("@tipo", centro.tipo);
                    commandExistencia.Parameters.AddWithValue("@direccion", centro.direccion);
                    commandExistencia.Parameters.AddWithValue("@cod_postal", centro.cod_postal);
                    commandExistencia.Parameters.AddWithValue("@longitud", centro.longitud);
                    commandExistencia.Parameters.AddWithValue("@latitud", centro.latitud);
                    commandExistencia.Parameters.AddWithValue("@telefono", centro.telefono);
                    commandExistencia.Parameters.AddWithValue("@descripcion", centro.descripcion);
                    int cantidadExistente = Convert.ToInt32(commandExistencia.ExecuteScalar());
                    if (cantidadExistente > 0)
                    {
                        Console.WriteLine($"El centro {centro.nombre} ya existe en la base de datos.");
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(
                         $"INSERT INTO centro_educativo (nombre, tipo, direccion, codigo_postal, longitud, latitud, telefono, descripcion, cod_localidad) VALUES (@nombre, @tipo, @direccion, @cod_postal, @longitud, @latitud, @telefono,@descripcion,@loc_codigo)", conn))
                        {
                            command.Parameters.AddWithValue("@nombre", centro.nombre);
                            command.Parameters.AddWithValue("@tipo", centro.tipo);
                            command.Parameters.AddWithValue("@direccion", centro.direccion);
                            command.Parameters.AddWithValue("@cod_postal", centro.cod_postal);
                            command.Parameters.AddWithValue("@longitud", centro.longitud);
                            command.Parameters.AddWithValue("@latitud", centro.latitud);
                            command.Parameters.AddWithValue("@telefono", centro.telefono);
                            command.Parameters.AddWithValue("@descripcion", centro.descripcion);
                            command.Parameters.AddWithValue("@loc_codigo", centro.loc_codigo);
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
                                            $"WHERE loc_nombre = @nombre AND " +
                                                  $"loc_codigo = @codigo ";
                using (MySqlCommand commandExistencia = new MySqlCommand(consultaExistencia, conn))
                {
                    commandExistencia.Parameters.AddWithValue("@nombre", loc.nombre);
                    commandExistencia.Parameters.AddWithValue("@codigo", loc.codigo);
                    int cantidadExistente = Convert.ToInt32(commandExistencia.ExecuteScalar());
                    if (cantidadExistente > 0)
                    {
                        Console.WriteLine($"La localidad {loc.nombre} ya existe en la base de datos.");
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
                                            $"WHERE prov_nombre = @nombre AND " +
                                                  $"prov_codigo = @codigo";
                using (MySqlCommand commandExistencia = new MySqlCommand(consultaExistencia, conn))
                {
                    commandExistencia.Parameters.AddWithValue("@nombre", prov.nombre);
                    commandExistencia.Parameters.AddWithValue("@codigo", prov.codigo);
                    int cantidadExistente = Convert.ToInt32(commandExistencia.ExecuteScalar());
                    if (cantidadExistente > 0)
                    {
                        Console.WriteLine($"La Provincia {prov.nombre} ya existe en la base de datos.");
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(
                        $"INSERT INTO provincia(prov_codigo, prov_nombre) VALUES (@codigo,@nombre)", conn))
                        {
                            command.Parameters.AddWithValue("@nombre", prov.nombre);
                            command.Parameters.AddWithValue("@codigo", prov.codigo);
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
