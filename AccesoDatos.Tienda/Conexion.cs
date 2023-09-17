using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AccesoDatos.Tienda
{
    public class Conexion
    {
        private MySqlConnection _conn;

        public Conexion(string servidor, string usuario, string password, string database, uint puerto)
        {
            MySqlConnectionStringBuilder cadenaConexion = new MySqlConnectionStringBuilder();
            cadenaConexion.Server = servidor;
            cadenaConexion.UserID = usuario;
            cadenaConexion.Password = password;
            cadenaConexion.Database = database;
            cadenaConexion.Port = puerto;
            _conn = new MySqlConnection(cadenaConexion.ToString());
        }
        public void EjecutarConsulta(string consulta)
        {
            try
            {
                _conn.Open();
                using (MySqlCommand comando = new MySqlCommand(consulta, _conn))
                {
                    comando.ExecuteNonQuery();
                    Console.WriteLine("Consulta ejecutada correctamente");
                }
                _conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar la consulta: ", ex.Message);
            }
        }
        //DataTable funciona con una tabla, DataSet varias tablas señalando su posición
        public DataTable ObtenerDatos(string consulta)
        {
            DataTable tabla = new DataTable();
            try
            {
                _conn.Open();
                using (MySqlCommand comando = new MySqlCommand(consulta, _conn))
                {
                    using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                        Console.WriteLine("Consulta ejecutada correctamente");
                    }
                }
                _conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar la consulta: ", ex.Message);
            }
            return tabla;
        }
    }
}
