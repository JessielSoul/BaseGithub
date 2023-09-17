using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Tienda;
using AccesoDatos.Tienda;

namespace Logica.Tienda
{
    public class ProductosLogica
    {
        private ProductosAccesoDatos _productosAcceso;

        public ProductosLogica()
        {
            _productosAcceso = new ProductosAccesoDatos();
        }
        public List<Productos> ObtenerProductos(string valor)
        {
            return _productosAcceso.ObtenerProductos(valor);
        }
        public void GuardarProducto(Productos nuevoProducto)
        {
            _productosAcceso.GuardarProducto(nuevoProducto);
        }
        public void EliminarProducto(int id)
        {
            _productosAcceso.EliminarProducto(id);
        }
        public void ActualizarProducto(Productos nuevoProducto)
        {
            _productosAcceso.ActualizarProducto(nuevoProducto);
        }
        public Tuple<bool, string> ValidarProducto(Productos nuevoProducto)
        {
            string mensaje = "";
            bool valida = true;

            if (nuevoProducto.Nombre == "")
            {
                mensaje = mensaje + "- El campo nombre es requerido \n";
                valida = false;
            }
            if (nuevoProducto.Descripcion == "")
            {
                mensaje = mensaje + "- El campo descripcion es requerido \n";
                valida = false;
            }
            if (nuevoProducto.Precio == 0)
            {
                mensaje = mensaje + "- El campo precio es requerido \n";
                valida = false;
            }

            var validar = new Tuple<bool, string>(valida, mensaje);
            return validar;
        }
    }
}
