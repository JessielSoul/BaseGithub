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
            dtgTienda.DataSource = _productologica.ObtenerProductos(valor);
        }
        private void GuardarProductos()
        {
            Productos nuevoProducto = new Productos();
            nuevoProducto.IDProducto = 0;
            nuevoProducto.Nombre = txtNombre.Text;
            nuevoProducto.Descripcion = txtDescripcion.Text;
            nuevoProducto.Precio = decimal.Parse(txtPrecio.Text);

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
            txtNombre.Enabled = estado;
            txtDescripcion.Enabled = estado;
            txtPrecio.Enabled = estado;
            txtBuscar.Enabled = estado;
        }
        private void LimpiarCuadros()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
        }
        private void ActualizarCategoria()
        {

            Productos nuevoProducto = new Productos();
            nuevoProducto.IDProducto= id;
            nuevoProducto.Nombre = txtNombre.Text;
            nuevoProducto.Descripcion = txtDescripcion.Text;
            nuevoProducto.Precio = decimal.Parse(txtPrecio.Text);

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
            LlenarProductos(txtBuscar.Text);
        }
        public void Eliminar()
        {
            if (MessageBox.Show("¿Desea eliminar la categoria seleccionada", "Eliminar categoria?",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var id = dtgTienda.CurrentRow.Cells["id"].Value.ToString();
                _productologica.EliminarProducto(int.Parse(id));
            }
        }
        private void dtgCategoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ControlarBotones(false, true, true, false, false);
            ControlarCuadros(true);
            txtNombre.Focus();

            txtNombre.Text = dtgTienda.CurrentRow.Cells["Nombre"].Value.ToString();
            txtDescripcion.Text = dtgTienda.CurrentRow.Cells["Descripcion"].Value.ToString();
            txtPrecio.Text = dtgTienda.CurrentRow.Cells["Precio"].Value.ToString();
            id = int.Parse(dtgTienda.CurrentRow.Cells["IDProducto"].Value.ToString());
            banderaGuardar = "Actualizar";
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ControlarBotones(false, true, true, false, false);
            ControlarCuadros(true);
            txtNombre.Focus();
            banderaGuardar = "Guardar";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (banderaGuardar == "Guardar")
            {
                GuardarProductos();
                LlenarProductos("");
            }
            else if (banderaGuardar == "Actualizar")
            {
                ActualizarCategoria();
                LlenarProductos("");
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ControlarBotones(true, false, false, true, true);
            ControlarCuadros(false);
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
            LlenarProductos("");
        }
    }
}
