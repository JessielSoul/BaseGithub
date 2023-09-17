using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Tienda;

namespace AccesoDatos.Tienda
{
    public class ProductosAccesoDatos
    {
        Conexion conexion;

        public ProductosAccesoDatos()
        {
            conexion = new Conexion("localhost", "root", "", "tienda", 3306);
        }

        public List<Productos> ObtenerProductos(string valor)
        {
            var listaProductos = new List<Productos>();
            var dt = new DataTable();
            dt = conexion.ObtenerDatos("Select * from Productos");
            //foreach = Por cada renglon en la tabla
            foreach (DataRow renglon in dt.Rows)
            {
                var product = new Productos
                {
                    IDProducto = Convert.ToInt32(renglon["IDProducto"]),
                    Nombre = renglon["Nombre"].ToString(),
                    Descripcion = renglon["Descripcion"].ToString(),
                    Precio = Convert.ToDecimal(renglon["Precio"]),
                };
                listaProductos.Add(product);
            }
            return listaProductos;
        }
        public void GuardarProducto(Productos nuevoProducto)
        {
            string consulta = string.Format("Insert into Productos values(null, '{0}', '{1}', '{2}')",
                nuevoProducto.Nombre, nuevoProducto.Descripcion, nuevoProducto.Precio);
            conexion.EjecutarConsulta(consulta);
        }
        public void ActualizarProducto(Productos nuevoProducto)
        {
            string consulta = string.Format("Update Productos set Nombre = '{0}', Descripcion = '{1}', " +
                "Precio = '{2}' where IDProducto = {3}",
                nuevoProducto.Nombre, nuevoProducto.Descripcion, nuevoProducto.Precio, nuevoProducto.IDProducto);
            conexion.EjecutarConsulta(consulta);
        }
        public void EliminarProducto(int id)
        {
            string consulta = string.Format("Delete from Productos where IDProducto = {0}", id);
            conexion.EjecutarConsulta(consulta);
        }
    }
}
