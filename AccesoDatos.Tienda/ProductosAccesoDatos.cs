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
                    IDProducto = Convert.ToInt32(renglon["idproducto"]),
                    Nombre = renglon["nombre"].ToString(),
                    Descripcion = renglon["descripcion"].ToString(),
                    Precio = Convert.ToDecimal(renglon["precio"]),
                };
                listaProductos.Add(product);
            }
            return listaProductos;
        }
        public void GuardarProducto(Productos nuevoProducto)
        {
            string consulta = string.Format("Insert into productos values(null, '{0}', '{1}', '{2}')",
                nuevoProducto.Nombre, nuevoProducto.Descripcion, nuevoProducto.Precio);
            conexion.EjecutarConsulta(consulta);
        }
        public void ActualizarProducto(Productos nuevoProducto)
        {
            string consulta = string.Format("update productos set nombre = '{0}', descripcion = '{1}', " +
                "precio = '{2}' where idproducto = {3}",
                nuevoProducto.Nombre, nuevoProducto.Descripcion, nuevoProducto.Precio, nuevoProducto.IDProducto);
            conexion.EjecutarConsulta(consulta);
        }
        public void EliminarProducto(int id)
        {
            string consulta = string.Format("delete from productos where idproducto = {0}", id);
            conexion.EjecutarConsulta(consulta);
        }

    }
}
