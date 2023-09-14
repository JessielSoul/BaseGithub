using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Tienda
{
    public class Productos
    {
        private int _IDProducto;
        private string _Nombre;
        private string _Descripcion;
        private decimal _Precio;

        public int IDProducto { get => _IDProducto; set => _IDProducto = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public decimal Precio { get => _Precio; set => _Precio = value; }
    }
}
