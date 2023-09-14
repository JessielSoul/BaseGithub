using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica.Tienda;
using AccesoDatos;
using Entidades.Tienda;
using System.Diagnostics.Eventing.Reader;

namespace Presentacion.Tienda
{
    public partial class Frm_Tienda : Form
    {
        private ProductosLogica _productologica;
        private string banderaGuardar = "";
        private int id = 0;
        public Frm_Tienda()
        {
            InitializeComponent();
            _productologica = new ProductosLogica();
        }
        private void Principal_Load(object sender, EventArgs e)
        {
            ControlarBotones(true, false, false, true, true);
            ControlarCuadros(true);
            LlenarProductos("");
        }
        private void LlenarProductos(string valor)
        {
            dtg_Tienda.DataSource = _productologica.ObtenerProductos(valor);
        }
        private void GuardarProductos()
        {
            Productos nuevoProducto = new Productos();
            nuevoProducto.IDProducto = 0;
            nuevoProducto.Nombre = txt_nombre.Text;
            nuevoProducto.Descripcion = txt_descripcion.Text;
            nuevoProducto.Precio = decimal.Parse(txt_precio.Text);

            var validar = _productologica.ValidarProducto(nuevoProducto);
            if (validar.Item1)
            {
                _productologica.GuardarProducto(nuevoProducto);
                LlenarProductos("");
                LimpiarCuadros();
                ControlarBotones(true, false, false, true, true);
                ControlarCuadros(true);
            }
            else
            {
                MessageBox.Show(validar.Item2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ControlarBotones(Boolean nuevo, Boolean guardar, Boolean cancelar, Boolean eliminar, Boolean cerrar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnCancelar.Enabled = cancelar;
            btnEliminar.Enabled = eliminar;
            btnCerrar.Enabled = cerrar;
        }
        private void ControlarCuadros(Boolean estado)
        {
            txt_nombre.Enabled = estado;
            txt_descripcion.Enabled = estado;
            txt_precio.Enabled = estado;
            txt_buscar.Enabled = estado;
        }
        private void LimpiarCuadros()
        {
            txt_nombre.Text = "";
            txt_descripcion.Text = "";
            txt_precio.Text = "";
        }
        private void ActualizarCategoria()
        {

            Productos nuevoProducto = new Productos();
            nuevoProducto.IDProducto= id;
            nuevoProducto.Nombre = txt_nombre.Text;
            nuevoProducto.Descripcion = txt_descripcion.Text;
            nuevoProducto.Precio = decimal.Parse(txt_precio.Text);

            var validar = _productologica.ValidarProducto(nuevoProducto);
            if (validar.Item1)
            {
                _productologica.ActualizarProducto(nuevoProducto);
                LlenarProductos("");
                LimpiarCuadros();
                ControlarBotones(true, false, false, true, true);
                ControlarCuadros(true);
            }
            else
            {
                MessageBox.Show(validar.Item2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            LlenarProductos(txt_buscar.Text);
        }
        public void Eliminar()
        {
            if (MessageBox.Show("¿Desea eliminar la categoria seleccionada", "Eliminar categoria?",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var id = dtg_Tienda.CurrentRow.Cells["id"].Value.ToString();
                _productologica.EliminarProducto(int.Parse(id));
            }
        }

        private void dtgCategoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ControlarBotones(false, true, true, false, false);
            ControlarCuadros(true);
            txt_nombre.Focus();

            txt_nombre.Text = dtg_Tienda.CurrentRow.Cells["nombre"].Value.ToString();
            txt_descripcion.Text = dtg_Tienda.CurrentRow.Cells["descripcion"].Value.ToString();
            txt_precio.Text = dtg_Tienda.CurrentRow.Cells["precio"].Value.ToString();
            id = int.Parse(dtg_Tienda.CurrentRow.Cells["idproducto"].Value.ToString());
            banderaGuardar = "actualizar";
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ControlarBotones(false, true, true, false, false);
            ControlarCuadros(true);
            txt_nombre.Focus();
            banderaGuardar = "guardar";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (banderaGuardar == "guardar")
            {
                GuardarProductos();
                LlenarProductos("");
            }
            else if (banderaGuardar == "actualizar")
            {
                ActualizarCategoria();
                LlenarProductos("");
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            ControlarBotones(true, false, false, true, true);
            ControlarCuadros(false);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            LlenarProductos("");
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            LlenarProductos(txt_buscar.Text);
        }
    }
}
