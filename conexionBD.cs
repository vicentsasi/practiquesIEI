﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
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
            try
            {
                if (conn == null)
                {
                   await Conectar();
                }
                using (MySqlCommand command = new MySqlCommand(
                  $"INSERT INTO centro_educativo (nombre, tipo, direccion, codigo_postal, longitud, latitud, telefono, descripcion) VALUES ('{centro.nombre}', '{centro.tipo}', '{centro.direccion}', '{centro.cod_postal}', '{centro.longitud}', '{centro.latitud}', '{centro.telefono}','{centro.descripcion}')", conn))
                {
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Datos insertados correctamente.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al ejecutar el comando: {e.Message}");
            }
        }
    }
}
