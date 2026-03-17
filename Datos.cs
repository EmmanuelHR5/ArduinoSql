using System;
using MySqlConnector;

namespace ProgramablesAvalonia
{
    internal class Datos
    {
        private string cadenaConexion =
            "Server=127.0.0.1;Port=3306;Database=programables;Uid=emmanuel;Pwd=cloud123.*;";

        public MySqlConnection? Conexion()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar: " + ex.Message);
                return null;
            }
        }

        public bool ejecutar(string consulta)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(consulta, Conexion());
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex){return false;}

            
        }
    }
}